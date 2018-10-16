using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIControll : MonoBehaviour {


    public void ResetScene()
    {
        SceneManager.LoadScene("TestScene");
    }
}
