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
        switch (Encoding.UTF8.GetString(payload))
        {
            // 设置玩家角色实体
            case "0":
                response.PlayerPrefabHash = null;
                break;
            case "1":
                response.PlayerPrefabHash = null;
                break;
        }
        response.Approved = true;
    }
}
