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
    private TMP_Text _gender;

    public void Initial(PlayerInfo playerInfo)
    {
        _name = transform.GetChild(0).GetComponent<TMP_Text>();
        _gender = transform.GetChild(1).GetComponent<TMP_Text>();
        _isReady = transform.GetChild(2).GetComponent<TMP_Text>();

        _name.text = "Player: " + playerInfo.id;
        _gender.text = playerInfo.gender == 0 ? "男" : "女";
        _isReady.text = playerInfo.isReady ? "准备" : "未准备";
    }

    public void UpdateInfo(PlayerInfo playerInfo)
    {
        PlayerInfo = playerInfo;
        _isReady.text = PlayerInfo.isReady ? "准备" : "未准备";
        _gender.text = PlayerInfo.gender == 0 ? "男" : "女";
    }

    public void SetReady(bool isReady)
    {
        _isReady.text = isReady ? "准备" : "未准备";
    }
}
