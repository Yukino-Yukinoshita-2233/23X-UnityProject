using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerInit.Instance.SetPlayerSpawn(MazeGenerator.Instance.Spawnpositon);
        }
    }
}