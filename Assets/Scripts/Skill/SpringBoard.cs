using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringBoard : MonoBehaviour
{
    //public GameObject SBoard;
    public float jumpForce = 10f; // 设置跳板提供的向上力

    private void OnCollisionEnter(Collision collision)
    {
        // 检查碰撞对象是否有刚体组件
        Rigidbody rb = collision.collider.GetComponent<Rigidbody>();

        // 如果有刚体组件，给予向上的力
        if (rb != null)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
