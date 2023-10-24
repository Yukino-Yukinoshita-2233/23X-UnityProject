using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public struct PlayerInfo : INetworkSerializable
{
    public ulong id;
    public bool isReady;

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref id);
        serializer.SerializeValue(ref isReady);
    }
}

public class LobbyController : NetworkBehaviour
{
    private GameObject _content;
    private GameObject _cell;
    private Button _startBtn;
    private Toggle _isReady;
    private Dictionary<ulong, PlayerListCell> _cellList = new();
    private Dictionary<ulong, PlayerInfo> _allPlayerInfo = new();

    public override void OnNetworkSpawn()
    {
        if (IsHost)
        {
            NetworkManager.OnClientConnectedCallback += OnClientConnectdCallback;
        }

        GetAllComponent();
        HandleEvent();

        PlayerInfo playerInfo = new()
        {
            id = NetworkManager.LocalClientId,
            isReady = false
        };
        AddPlayer(playerInfo);
    }

    private void OnClientConnectdCallback(ulong obj)
    {
        PlayerInfo playerInfo = new()
        {
            id = obj,
            isReady = false
        };
        AddPlayer(playerInfo);
        UpdateAllPlayerInfos();
    }

    private void UpdateAllPlayerInfos()
    {
        foreach (var item in _allPlayerInfo)
        {
            UpdatePlayerInfoClientRpc(item.Value);
        }
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
            _cellList[item.Key].SetReady(item.Value.isReady);
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
        _isReady = GameObject.Find("Canvas/Ready").GetComponent<Toggle>();
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
        _cellList[playerInfo.id].SetReady(playerInfo.isReady);
        UpdateAllPlayerInfos();
    }

    private void OnStartBtnClick()
    {
        throw new NotImplementedException();
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
