using Cinemachine;
using Unity.Netcode;
using UnityEngine;

public class CameraController : NetworkBehaviour
{
    public static CameraController Instance;
    public CinemachineVirtualCamera _cam;
    public Transform PlayerSpawn;
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        Instance = this;
    }

    private void Start()
    {
        if (_cam == null)
        {
            _cam = transform.GetChild(0).GetComponent<CinemachineVirtualCamera>();
        }
    }

    public void SetFollowTarget(Transform player)
    {
        _cam.Follow = player;
    }

    public Vector3 GetSpawnPosition()
    {
        return PlayerSpawn.position + new Vector3(-0.5f, 0, 0.5f);
    }
}
