using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkInitial : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
