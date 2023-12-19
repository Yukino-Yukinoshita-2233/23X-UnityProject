using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBulletsSkill : MonoBehaviour
{
    [SerializeField]
    GameObject PlayerPosition;
    //GameObject EnergyBulletsPrefab;
    // ����һ�����б��������������ӵ���Ԥ����
    public GameObject bulletPrefab;
    // ����һ�����б���������ָ���ӵ�������λ��
    public Transform spawnPoint;
    // ����һ��˽�б����������洢��ǰ���ӵ�
    private GameObject currentBullet;
    // ����һ��˽�б����������洢�ӵ��Ĵ�С�仯���ٶ�
    private float scaleSpeed = 0.1f;
    // ����һ��˽�б����������洢�ӵ��ķ�����ٶ�
    private float shootSpeed = 20f;

    IEnumerator Wait(float i)
    {

        yield return new
        WaitForSeconds(i);

    }
    void Start()
    {
        //bulletPrefab = Resources.Load("/PlayerSkill/bulletPrefab");
        if(GameObject.FindWithTag("Player") != null)
        {
            PlayerPosition = GameObject.FindWithTag("Player");
            Debug.Log("Player");
        }
        spawnPoint.position = PlayerPosition.GetComponent<Transform>().position + new Vector3(0.15f, 1, 1f);



    }
    // �� Update �����м����ҵ�����
    void Update()
    {
        // �����Ұ���������
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Player Fire");
            // ����һ���ӵ���λ��Ϊ����λ�ã���תΪ��ҵ���ת
            currentBullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.transform.rotation * Quaternion.Euler(90,0,0));
            // ��ȡ�ӵ��Ľű����
            EnergyBullets bullet = currentBullet.GetComponent<EnergyBullets>();
            // �����ӵ����ٶ�Ϊ 0��������ʱ����
            bullet.speed = 0;
            currentBullet.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            // �����ӵ��ĳ�ʼ��СΪ��ǰ�Ĵ�С
            bullet.scale = currentBullet.transform.localScale;
        }

        // �����ҳ�����ס������
        if (Input.GetMouseButton(0))
        {        
            //StartCoroutine(Wait(0.5f));


            // �����ǰ���ӵ�
            if (currentBullet != null)
            {
                // ��ȡ�ӵ��Ľű����
                EnergyBullets bullet = currentBullet.GetComponent<EnergyBullets>();
                if (currentBullet.transform.localScale.x <= 0.5f)
                {
                // ���ӵ��Ĵ�С�����ӣ����Դ�С�仯���ٶȺ�ʱ����
                currentBullet.transform.localScale += Vector3.one * scaleSpeed * Time.deltaTime;
                // �����ӵ��ĳ�ʼ��СΪ��ǰ�Ĵ�С
                bullet.scale = currentBullet.transform.localScale;
                    if (shootSpeed >= 1)
                    {
                        shootSpeed -= Time.deltaTime *5;

                    }

                }
            }
        }
        // �������ɿ�������
        if (Input.GetMouseButtonUp(0))
        {
            // �����ǰ���ӵ�
            if (currentBullet != null)
            {
                // ��ȡ�ӵ��Ľű����
                EnergyBullets bullet = currentBullet.GetComponent<EnergyBullets>();
                // �����ӵ����ٶ�Ϊ������ٶ�
                Debug.Log(shootSpeed);
                bullet.speed = shootSpeed;
                // ����ǰ���ӵ��ÿ�
                currentBullet = null;
                shootSpeed = 20f;
            }
        }
    }




}
