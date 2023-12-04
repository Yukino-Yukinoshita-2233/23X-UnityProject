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

    // ����һ�����б����������洢�ӵ����ٶ�
    public float speed;
    // ����һ�����б����������洢�ӵ��ĳ�ʼ��С
    public Vector3 scale;
    private void Start()
    {
        StartCoroutine((IEnumerator)DestroyTime(10));
    }
    // �� Awake �����г�ʼ������
    void Awake()
    {
        // �����ӵ��Ĵ�СΪ��ʼ��С
        transform.localScale = scale;
    }

    // �� Update �����и����ӵ����˶�
    void Update()
    {
        // ���ӵ���������� z �᷽��ǰ���������ٶȺ�ʱ����
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
