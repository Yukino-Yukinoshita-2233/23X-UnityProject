using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DeathWall : MonoBehaviour
{
    public GameObject Canvas;
    private Button _btn;
    private void Start()
    {
        _btn = Canvas.GetComponentInChildren<Button>();
        _btn.onClick.AddListener(Respawn);
    }

    private void Respawn()
    {
        //Time.timeScale = 1;
        MazeGenerator.Instance.PlayerCreate();
        Canvas.SetActive(false);  
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        Destroy(other.gameObject);
        Canvas.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        //Time.timeScale = 0;

    }

}
