using Cinemachine.Examples;
using Unity.Netcode;
using UnityEngine;

public class PlayerInit : NetworkBehaviour
{
    private void Start()
    {
        GameManager.Instance.OnStartGame.AddListener(OnStartGame);
    }

    private void OnStartGame()
    {
        PlayerInfo playerInfo = LobbyController.Instance.AllPlayerInfos[OwnerClientId];
        var Player = transform.GetChild(playerInfo.gender);
        Player.gameObject.SetActive(true);
        PlayerSync playerSync = GetComponent<PlayerSync>();
        playerSync.SetTarget(Player);
        playerSync.enabled = true;
        if (IsLocalPlayer)
        {
            Player.GetComponent<PlayerMove>().enabled = true;
            CameraController.Instance.SetFollowTarget(Player);
        }

        Player.position = MazeGenerator.Instance.Spawnpositon.Value;
    }
}
