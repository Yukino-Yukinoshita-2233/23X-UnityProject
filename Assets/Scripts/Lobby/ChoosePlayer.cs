using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoosePlayer : MonoBehaviour
{
    public static ChoosePlayer Instance;

    void Start()
    {
        Instance = this;
    }

    public void SwitchPlayer(int gender)
    {
        transform.GetChild(gender).gameObject.SetActive(true);
        transform.GetChild(1 - gender).gameObject.SetActive(false);
    }
}
