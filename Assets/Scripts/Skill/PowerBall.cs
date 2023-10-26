using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBall : MonoBehaviour
{
    public GameObject powerballPrefab; // 小球的预制体
    public GameObject powerballSillPrefab; // 技能球的预制体
    public float powerballSpeed = 10.0f;
    public int BallSum = 5;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FireProjectile();
        }
    }
    void FireProjectile()
    {
        if(BallSum>0)
        {
            GameObject projectile = Instantiate(powerballPrefab, transform.position, transform.rotation);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.velocity = new Vector3(0, 0.05f, 1) * powerballSpeed;
            Destroy(projectile, 10.0f);
            BallSum--;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("SkillBall"))
        {

            string SkillName = nameof(Collision.collider.name);
            if(SkillName== "PowerBallSkill")
            {
                BallSum += 5;
                Destroy(collision.gameObject);
                Debug.Log("BallSum+5");
            }
            Destroy(collision.gameObject);
        }
    }
}
