using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringBoard : MonoBehaviour
{
    //public GameObject SBoard;
    public float jumpForce = 10f; // ���������ṩ��������

    private void OnCollisionEnter(Collision collision)
    {
        // �����ײ�����Ƿ��и������
        Rigidbody rb = collision.collider.GetComponent<Rigidbody>();

        // ����и���������������ϵ���
        if (rb != null)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
