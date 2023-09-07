using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField]
    public int Column = 1; // 列
    public int Row = 1; // 行
    public float spacing = 10f;
    public GameObject Ground;
    public GameObject Player;
    public GameObject Key;
    private List<GameObject> groundLists = new();
    void Start()
    {

        var height = new Vector3(0, 1, 0);
        if (Ground != null)
        {
            Debug.Log("Groung_Row:" + Row);
            Debug.Log("Groung_Column:" + Column);
            for (int row = 0; row < Row; row++)
            {
                for (int col = 0; col < Column; col++)
                {
                    float x = col * spacing;
                    float z = row * spacing;
                    GameObject goj = GameObject.Instantiate(Ground, new Vector3(x, 0, z), Quaternion.identity);
                    groundLists.Add(goj);
                }
            }

            // Generate Key
            int num1 = Random.Range(0, groundLists.Count);
            Instantiate(Key, groundLists[num1].transform.position + height, Quaternion.identity);

            // Generate Player
            int num2 = Random.Range(0, groundLists.Count);
            Instantiate(Player, groundLists[num2].transform.position + height, Quaternion.identity);
        }
    }
}
