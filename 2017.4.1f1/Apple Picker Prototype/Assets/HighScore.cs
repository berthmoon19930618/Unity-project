using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScore : MonoBehaviour {

    static public int score = 1000;
    void Awake()
    {
        //如果ApplePickerHighScore已經存在,則讀取其值
        if (PlayerPrefs.HasKey("ApplePickerHighScore"))
        {
            score = PlayerPrefs.GetInt("ApplePickerHighScore");
        }
        //將最高分賦給ApplePickerHighScore
        PlayerPrefs.SetInt("ApplePickerHighScore", score);
    }

    // Update is called once per frame
    void Update ()
    {
        GUIText gt = this.GetComponent<GUIText>();
        gt.text = "High Score:" + score;
        //如有必要,則更新PlayerPrefs中的Update ApplePickerHighScore
        if (score > PlayerPrefs.GetInt("ApplePickerHighScore"))
        {
            PlayerPrefs.SetInt("ApplePickerHighScore", score);
        }
    }
}
