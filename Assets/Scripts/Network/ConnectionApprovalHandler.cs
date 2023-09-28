using Unity.Netcode;
using UnityEngine;
using Unity.Game.Constants;
using System.Text;

public class ConnectionApprovalHandler : NetworkBehaviour
{
    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            NetworkManager.ConnectionApprovalCallback = ApprovalCheck;
        }
    }

    private void ApprovalCheck(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response)
    {
        // 客户端请求数据
        var payload = request.Payload;
        // 服务端数据处理
        response.PlayerPrefabHash = uint.Parse(Encoding.UTF8.GetString(payload));
        response.Approved = true;
    }
}
