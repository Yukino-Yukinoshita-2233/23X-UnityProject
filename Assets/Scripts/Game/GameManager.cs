using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : NetworkBehaviour
{
    public static GameManager Instance;
    public UnityEvent OnStartGame;

    public override void OnNetworkSpawn()
    {
        Instance = this;
        NetworkManager.Singleton.SceneManager.OnLoadEventCompleted += OnLoadEventCompleted;

        base.OnNetworkSpawn();
    }

    private void OnLoadEventCompleted(string sceneName, LoadSceneMode loadSceneMode, List<ulong> clientsCompleted, List<ulong> clientsTimedOut)
    {
        switch (sceneName)
        {
            case "Maze":
                OnStartGame.Invoke();
                break;
            case "Maze001":
                OnStartGame.Invoke();
                break;
            default:
                break;
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(string sceneName)
    {
        NetworkManager.Singleton.SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
