using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Win : NetworkBehaviour
{
    private new AudioSource audio;
    [SerializeField] GameObject WinCanvas;
    [SerializeField] Button ReturnMuneBtn;
    private void Start()
    {
        audio = GetComponent<AudioSource>();
        WinCanvas = GameObject.Find("WinCanvas");
        Debug.Log(WinCanvas.name);
        ReturnMuneBtn = GameObject.Find("ReturnMuneBtn").GetComponent<Button>();
        WinCanvas.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            Debug.Log("IsServer: "+IsServer);
            Debug.Log("IsClient: "+IsClient);
            StartCoroutine(GetReturmMuneBtnDowm());
        }
    }

    IEnumerator GetReturmMuneBtnDowm()
    {
        yield return null;

        WinCanvas.SetActive(true);

        ReturnMuneBtn.onClick.AddListener(ReturnMenuButtonClicked);
    }

    void ReturnMenuButtonClicked()
    {
        SceneManager.LoadScene("Start");
    }

    [ClientRpc]
    private void UpdateWinClientRpc()
    {

    }

    [ServerRpc]
    private void UpdateWinServerRpc()
    {

    }


}
