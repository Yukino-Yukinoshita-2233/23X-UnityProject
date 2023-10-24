using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerListCell : MonoBehaviour
{
    public PlayerInfo PlayerInfo { get; private set; }
    private TMP_Text _name;
    private TMP_Text _isReady;

    public void Initial(PlayerInfo playerInfo)
    {
        _name = transform.GetChild(0).GetComponent<TMP_Text>();
        _isReady = transform.GetChild(1).GetComponent<TMP_Text>();
        _name.text = "Player: " + playerInfo.id;
        _isReady.text = playerInfo.isReady ? "准备" : "未准备";
    }

    internal void SetReady(bool arg0)
    {
        _isReady.text = arg0 ? "准备" : "未准备";
    }
}
