using Unity.Netcode;
using UnityEngine;

public class PlayerInit : NetworkBehaviour
{
    public static PlayerInit Instance;
    public Transform Player { get; private set; }

    private void Start()
    {
        Instance = this;
        GameManager.Instance.OnStartGame.AddListener(OnStartGame);
    }

    private void OnStartGame()
    {
        PlayerInfo playerInfo = LobbyController.Instance.AllPlayerInfos[OwnerClientId];
        Player = transform.GetChild(playerInfo.gender);
        Player.gameObject.SetActive(true);
        PlayerSync playerSync = GetComponent<PlayerSync>();
        playerSync.SetTarget(Player);
        playerSync.enabled = true;
        if (IsLocalPlayer)
        {
            CameraController.Instance.SetFollowTarget(Player);
        }
    }

    public void SetPlayerSpawn(Vector3 position)
    {
        Player.position = position;
    }
}
