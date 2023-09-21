using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class StartManager : MonoBehaviour
{
    void Start()
    {
        Button createBtn = GameObject.Find("CreateRoom").GetComponent<Button>();
        Button joinBtn = GameObject.Find("JoinRoom").GetComponent<Button>();
        createBtn.onClick.AddListener(OnCreateBtnClick);
        joinBtn.onClick.AddListener(OnJoinBtnClick);
    }

    private void OnCreateBtnClick()
    {
        NetworkManager.Singleton.StartHost();
    }

    private void OnJoinBtnClick()
    {
        NetworkManager.Singleton.StartClient();
    }
}
