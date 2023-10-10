using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using TMPro;
using Unity.Netcode.Transports.UTP;
using Unity.Game.Constants;
using System.Text;

public class CanvasManager : MonoBehaviour
{
    public GameObject Player_Boy;
    public GameObject Player_Girl;
    TMP_InputField _ip;
    TMP_InputField _port;
    GameObject _player_boy;
    GameObject _player_girl;
    uint _player_hash;
    void Start()
    {
        Button createBtn = GameObject.Find("CreateRoom").GetComponent<Button>();
        Button joinBtn = GameObject.Find("JoinRoom").GetComponent<Button>();
        _ip = GameObject.Find("IP").GetComponent<TMP_InputField>();
        _port = GameObject.Find("Port").GetComponent<TMP_InputField>();
        TMP_Dropdown _player = GameObject.Find("ChooseCharacter").GetComponent<TMP_Dropdown>();
        createBtn.onClick.AddListener(OnCreateBtnClick);
        joinBtn.onClick.AddListener(OnJoinBtnClick);

        _player.onValueChanged.AddListener(OnPlayerValueChanged);
        // DropDown添加Options
        foreach (var option in GameConstants.ChooseCharacter)
        {
            var newOption = new TMP_Dropdown.OptionData(option);
            _player.options.Add(newOption);
            _player.RefreshShownValue();
        }

        // 获取玩家
        foreach (var player in GameObject.FindGameObjectsWithTag("Player"))
        {
            switch (player.name)
            {
                case "Player_Boy":
                    _player_boy = player.gameObject;
                    break;
                case "Player_Girl":
                    _player_girl = player.gameObject;
                    break;
            }
        }
    }

    private void OnCreateBtnClick()
    {
        // 联机申请
        // NetworkManager.Singleton.ConnectionApprovalCallback = ApprovalCheck;
        // 启动服务端
        NetworkManager.Singleton.StartHost();
        // 进入大厅
        GameManager.Instance.LoadScene("Lobby");
    }

    private void OnJoinBtnClick()
    {
        var transport = NetworkManager.Singleton.NetworkConfig.NetworkTransport as UnityTransport;
        if (ushort.TryParse(_port.text, out ushort port))
        {
            // 设置客户端连接
            transport.SetConnectionData(_ip.text, port);
            // 客户端的角色选择
            NetworkManager.Singleton.NetworkConfig.ConnectionData = Encoding.UTF8.GetBytes(_player_hash.ToString());
            NetworkManager.Singleton.StartClient();
        }
        else
        {
            Debug.LogError("Invaild port");
        }
    }

    private void OnPlayerValueChanged(int value)
    {
        switch (value)
        {
            case 0:
                _player_boy.SetActive(true);
                _player_girl.SetActive(false);
                // _player_hash = _player_boy.GetComponent<NetworkObject>().PrefabIdHash;
                NetworkManager.Singleton.NetworkConfig.PlayerPrefab = Player_Boy;
                _player_hash = Player_Boy.GetComponent<NetworkObject>().PrefabIdHash;
                break;
            case 1:
                _player_boy.SetActive(false);
                _player_girl.SetActive(true);
                // _player_hash = _player_girl.GetComponent<NetworkObject>().PrefabIdHash;
                NetworkManager.Singleton.NetworkConfig.PlayerPrefab = Player_Girl;
                _player_hash = Player_Girl.GetComponent<NetworkObject>().PrefabIdHash;
                break;
        }
    }
}
