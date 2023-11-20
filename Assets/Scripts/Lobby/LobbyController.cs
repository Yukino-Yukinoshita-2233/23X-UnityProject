using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public struct PlayerInfo : INetworkSerializable
{
    public ulong id;
    public bool isReady;
    public int gender;

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref id);
        serializer.SerializeValue(ref isReady);
        serializer.SerializeValue(ref gender);
    }
}

public class LobbyController : NetworkBehaviour
{
    public static LobbyController Instance;
    public Dictionary<ulong, PlayerInfo> AllPlayerInfos { get { return _allPlayerInfo; } }
    private GameObject _content;
    private GameObject _cell;
    private Button _startBtn;
    private Toggle _isReady;
    private Toggle _male;
    private Toggle _female;
    private Dictionary<ulong, PlayerListCell> _cellList = new();
    private Dictionary<ulong, PlayerInfo> _allPlayerInfo = new();

    public override void OnNetworkSpawn()
    {
        Instance = this;
        if (IsHost)
        {
            NetworkManager.OnClientConnectedCallback += OnClientConnectdCallback;
        }

        GetAllComponent();
        HandleEvent();

        PlayerInfo playerInfo = new()
        {
            id = NetworkManager.LocalClientId,
            isReady = false,
            gender = 0

        };
        AddPlayer(playerInfo);

        base.OnNetworkSpawn();
    }

    private void OnClientConnectdCallback(ulong obj)
    {
        PlayerInfo playerInfo = new()
        {
            id = obj,
            isReady = false,
            gender = 0
        };
        AddPlayer(playerInfo);
        UpdateAllPlayerInfos();
    }

    /// <summary>
    /// 服务端自己调用的方法
    /// </summary>
    private void UpdateAllPlayerInfos()
    {
        bool toStart = true;
        foreach (var item in _allPlayerInfo)
        {
            if (!item.Value.isReady)
            {
                toStart = false;
            }
            UpdatePlayerInfoClientRpc(item.Value);
        }

        // 开始游戏
        _startBtn.gameObject.SetActive(toStart);

    }

    [ClientRpc]
    private void UpdatePlayerInfoClientRpc(PlayerInfo playerInfo)
    {
        if (!IsServer)
        {
            if (_allPlayerInfo.ContainsKey(playerInfo.id))
            {
                _allPlayerInfo[playerInfo.id] = playerInfo;
            }
            else
            {
                AddPlayer(playerInfo);
            }
            UpdatePlayerCells();
        }
    }

    private void UpdatePlayerCells()
    {
        foreach (var item in _allPlayerInfo)
        {
            _cellList[item.Key].UpdateInfo(item.Value);
        }
    }

    private void UpdatePlayerInfo(ulong id, bool isReady)
    {
        PlayerInfo info = _allPlayerInfo[id];
        info.isReady = isReady;
        _allPlayerInfo[id] = info;
    }

    void GetAllComponent()
    {
        _content = GameObject.Find("Canvas/List/Viewport/Content");
        _cell = _content.transform.GetChild(0).gameObject;
        _startBtn = GameObject.Find("Canvas/Start").GetComponent<Button>();
        _startBtn.gameObject.SetActive(false);
        _isReady = GameObject.Find("Canvas/Ready").GetComponent<Toggle>();

        _male = GameObject.Find("Canvas/Gender/Male").GetComponent<Toggle>();
        _female = GameObject.Find("Canvas/Gender/Female").GetComponent<Toggle>();

        _male.onValueChanged.AddListener(OnMaleChanged);
        _female.onValueChanged.AddListener(OnFemaleChanged);

    }

    private void OnFemaleChanged(bool arg0)
    {
        if (arg0)
        {
            PlayerInfo playerInfo = _allPlayerInfo[NetworkManager.LocalClientId];
            playerInfo.gender = 1;
            _allPlayerInfo[NetworkManager.LocalClientId] = playerInfo;
            _cellList[NetworkManager.LocalClientId].UpdateInfo(playerInfo);

            if (IsServer)
            {
                UpdateAllPlayerInfos();
            }
            else
            {
                UpdateAllPlayerInfosServerRpc(_allPlayerInfo[NetworkManager.LocalClientId]);
            }

            ChoosePlayer.Instance.SwitchPlayer(1);
        }
    }

    private void OnMaleChanged(bool arg0)
    {
        if (arg0)
        {
            PlayerInfo playerInfo = _allPlayerInfo[NetworkManager.LocalClientId];
            playerInfo.gender = 0;
            _allPlayerInfo[NetworkManager.LocalClientId] = playerInfo;
            _cellList[NetworkManager.LocalClientId].UpdateInfo(playerInfo);

            if (IsServer)
            {
                UpdateAllPlayerInfos();
            }
            else
            {
                UpdateAllPlayerInfosServerRpc(_allPlayerInfo[NetworkManager.LocalClientId]);
            }

            ChoosePlayer.Instance.SwitchPlayer(0);
        }
    }

    void HandleEvent()
    {
        if (_startBtn != null)
        {
            _startBtn.onClick.AddListener(OnStartBtnClick);
        }

        if (_isReady != null)
        {
            _isReady.onValueChanged.AddListener(OnReadyToggleChanged);
        }
    }

    private void OnReadyToggleChanged(bool arg0)
    {
        _cellList[NetworkManager.LocalClientId].SetReady(arg0);
        UpdatePlayerInfo(NetworkManager.LocalClientId, arg0);
        if (IsServer)
        {
            UpdateAllPlayerInfos();
        }
        else
        {
            UpdateAllPlayerInfosServerRpc(_allPlayerInfo[NetworkManager.LocalClientId]);
        }

    }

    [ServerRpc(RequireOwnership = false)]
    private void UpdateAllPlayerInfosServerRpc(PlayerInfo playerInfo)
    {
        _allPlayerInfo[playerInfo.id] = playerInfo;
        _cellList[playerInfo.id].UpdateInfo(playerInfo);
        UpdateAllPlayerInfos();
    }

    private void OnStartBtnClick()
    {
        NetworkManager.Singleton.SceneManager.LoadScene("Maze", LoadSceneMode.Single);
        // NetworkManager.Singleton.SceneManager.LoadScene("Maze001", LoadSceneMode.Single);
    }

    private void AddPlayer(PlayerInfo playerInfo)
    {
        _allPlayerInfo.Add(playerInfo.id, playerInfo);
        GameObject clone = Instantiate(_cell);
        clone.transform.SetParent(_content.transform, false);
        PlayerListCell cell = clone.GetComponent<PlayerListCell>();
        _cellList.Add(playerInfo.id, cell);
        cell.Initial(playerInfo);
        clone.SetActive(true);
    }
}
