using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using TMPro;
using Unity.Netcode.Transports.UTP;
using Unity.Game.Constants;
using System.Text;
using UnityEngine.SceneManagement;

public class CanvasManager : NetworkBehaviour
{
    TMP_InputField _ip;
    TMP_InputField _port;
    void Start()
    {
        Button createBtn = GameObject.Find("CreateRoom").GetComponent<Button>();
        Button joinBtn = GameObject.Find("JoinRoom").GetComponent<Button>();
        _ip = GameObject.Find("IP").GetComponent<TMP_InputField>();
        _port = GameObject.Find("Port").GetComponent<TMP_InputField>();

        // 监听事件
        createBtn.onClick.AddListener(OnCreateBtnClick);
        joinBtn.onClick.AddListener(OnJoinBtnClick);

    }

    private void OnCreateBtnClick()
    {
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
            NetworkManager.Singleton.StartClient();
        }
        else
        {
            Debug.LogError("Invaild port");
        }
    }
}
