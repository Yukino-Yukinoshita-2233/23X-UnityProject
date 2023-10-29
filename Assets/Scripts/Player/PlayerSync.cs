using Unity.Netcode;
using UnityEngine;

public class PlayerSync : NetworkBehaviour
{
    private NetworkVariable<Vector3> _syncPos = new();
    private NetworkVariable<Quaternion> _syncRota = new();
    private Transform _syncTransform;

    public void SetTarget(Transform player)
    {
        _syncTransform = player;
    }

    private void Update()
    {
        if (IsLocalPlayer)
        {
            UploadTransform();
        }
    }

    private void FixedUpdate()
    {
        if (!IsLocalPlayer)
        {
            SyncTransform();
        }
    }

    private void SyncTransform()
    {
        _syncTransform.position = _syncPos.Value;
        _syncTransform.rotation = _syncRota.Value;
    }

    private void UploadTransform()
    {
        if (IsServer)
        {
            _syncPos.Value = _syncTransform.position;
            _syncRota.Value = _syncTransform.rotation;
        }
        else
        {
            UploadTransformServerRpc(_syncTransform.position, _syncTransform.rotation);
        }
    }

    [ServerRpc]
    private void UploadTransformServerRpc(Vector3 position, Quaternion rotation)
    {
        _syncPos.Value = position;
        _syncRota.Value = rotation;
    }
}
