using System;
using System.Collections;
using System.Collections.Generic;
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
        Transform player = transform.GetChild(playerInfo.gender);
        player.gameObject.SetActive(true);
        if (IsLocalPlayer)
        {
            CameraController.Instance.SetFollowTarget(player);
        }
    }
}
