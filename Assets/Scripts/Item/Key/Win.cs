using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    private new AudioSource audio;
    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }
    // Win the game
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            Debug.Log("You win the game!");
            audio.Play();
            Time.timeScale = 0;
        }
    }
}
