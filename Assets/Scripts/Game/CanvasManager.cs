using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using TMPro;
using Unity.Netcode.Transports.UTP;
using Unity.Game.Constants;
using System.Text;

public class CanvasManager : MonoBehaviour
{
    public int Playerchoice { get; set; } = 0;
    TMP_InputField _ip;
    TMP_InputField _port;
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

        foreach (var option in GameConstants.ChooseCharacter)
        {
            var newOption = new TMP_Dropdown.OptionData(option);
            _player.options.Add(newOption);
            _player.RefreshShownValue();
        }
    }

    private void OnCreateBtnClick()
    {
        // 启动服务端
        NetworkManager.Singleton.StartHost();
    }

    private void OnJoinBtnClick()
    {
        var transport = NetworkManager.Singleton.NetworkConfig.NetworkTransport as UnityTransport;
        if (ushort.TryParse(_port.text, out ushort port))
        {
            // 设置客户端连接
            transport.SetConnectionData(_ip.text, port);
            // 客户端的角色选择
            NetworkManager.Singleton.NetworkConfig.ConnectionData = Encoding.UTF8.GetBytes(Playerchoice.ToString());
            NetworkManager.Singleton.StartClient();
        }
        else
        {
            Debug.LogError("Invaild port");
        }
    }

    private void OnPlayerValueChanged(int value)
    {
        Playerchoice = value;
    }
}
