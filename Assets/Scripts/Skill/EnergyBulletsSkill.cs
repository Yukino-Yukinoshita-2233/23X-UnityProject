using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBulletsSkill : MonoBehaviour
{
    [SerializeField]
    GameObject PlayerPosition;
    //GameObject EnergyBulletsPrefab;
    // 定义一个公有变量，用来引用子弹的预制体
    public GameObject bulletPrefab;
    // 定义一个公有变量，用来指定子弹的生成位置
    public Transform spawnPoint;
    // 定义一个私有变量，用来存储当前的子弹
    private GameObject currentBullet;
    // 定义一个私有变量，用来存储子弹的大小变化的速度
    private float scaleSpeed = 0.1f;
    // 定义一个私有变量，用来存储子弹的发射的速度
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
    // 在 Update 函数中检测玩家的输入
    void Update()
    {
        // 如果玩家按下鼠标左键
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Player Fire");
            // 生成一个子弹，位置为生成位置，旋转为玩家的旋转
            currentBullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.transform.rotation * Quaternion.Euler(90,0,0));
            // 获取子弹的脚本组件
            EnergyBullets bullet = currentBullet.GetComponent<EnergyBullets>();
            // 设置子弹的速度为 0，让它暂时不动
            bullet.speed = 0;
            currentBullet.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            // 设置子弹的初始大小为当前的大小
            bullet.scale = currentBullet.transform.localScale;
        }

        // 如果玩家持续按住鼠标左键
        if (Input.GetMouseButton(0))
        {        
            //StartCoroutine(Wait(0.5f));


            // 如果当前有子弹
            if (currentBullet != null)
            {
                // 获取子弹的脚本组件
                EnergyBullets bullet = currentBullet.GetComponent<EnergyBullets>();
                if (currentBullet.transform.localScale.x <= 0.5f)
                {
                // 让子弹的大小逐渐增加，乘以大小变化的速度和时间间隔
                currentBullet.transform.localScale += Vector3.one * scaleSpeed * Time.deltaTime;
                // 更新子弹的初始大小为当前的大小
                bullet.scale = currentBullet.transform.localScale;
                    if (shootSpeed >= 1)
                    {
                        shootSpeed -= Time.deltaTime *5;

                    }

                }
            }
        }
        // 如果玩家松开鼠标左键
        if (Input.GetMouseButtonUp(0))
        {
            // 如果当前有子弹
            if (currentBullet != null)
            {
                // 获取子弹的脚本组件
                EnergyBullets bullet = currentBullet.GetComponent<EnergyBullets>();
                // 设置子弹的速度为发射的速度
                Debug.Log(shootSpeed);
                bullet.speed = shootSpeed;
                // 将当前的子弹置空
                currentBullet = null;
                shootSpeed = 20f;
            }
        }
    }




}
