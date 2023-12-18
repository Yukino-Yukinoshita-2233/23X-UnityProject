using UnityEngine;
using Unity.Netcode;

public class Win : NetworkBehaviour
{
    private new AudioSource audio;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            if (other.GetComponent<NetworkObject>().IsOwner && IsServer)
            {

            }
            else
            {
                UpdateWinServerRpc();
            }
        }
    }

    [ClientRpc]
    private void UpdateWinClientRpc()
    {

    }

    [ServerRpc]
    private void UpdateWinServerRpc()
    {

    }


}
