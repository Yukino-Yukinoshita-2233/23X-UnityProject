using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBullets : MonoBehaviour
{
    [SerializeField]
    float forwardForce;
    float upForce;
    float Speed;
    float BulletsSize;
    Vector3 forwardVector;

    // 定义一个公有变量，用来存储子弹的速度
    public float speed;
    // 定义一个公有变量，用来存储子弹的初始大小
    public Vector3 scale;
    private void Start()
    {
        StartCoroutine((IEnumerator)DestroyTime(10));
    }
    // 在 Awake 函数中初始化变量
    void Awake()
    {
        // 设置子弹的大小为初始大小
        transform.localScale = scale;
    }

    // 在 Update 函数中更新子弹的运动
    void Update()
    {
        // 让子弹沿着自身的 z 轴方向前进，乘以速度和时间间隔
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
    IEnumerable DestroyTime(int S)
    {
        while (S > 0)
        {
            S--;
            yield return new WaitForSeconds(1);
        }
        Destroy(gameObject);

    }

    private void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
    }
}
