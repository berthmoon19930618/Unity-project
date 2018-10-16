using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SceneManagement;//新的場景管理系統
using UnityEngine;
using UnityEngine.UI;

public class ApplePicker : MonoBehaviour {

    public static ApplePicker Instance;//讓此程式可被調用
    public GameObject basketPrefab;
    public int numBaskets = 3;
    public float basketBottomY = -14f;
    public float basketSpacingY = 2f;
    public List<GameObject> basketList;
    public GameObject GameTitle;
    public GameObject GameOver;
    public GameObject ResetButton;
    public GameObject QuitButton;
    public GameObject PlayButton;
    public GameObject AppleTreeObject;
    public GameObject AppleObject;

    // Use this for initialization
    void Start ()
    {
        Instance = this;
        AppleTreeObject.SetActive(false);
        ResetButton.SetActive(false);
        GameOver.SetActive(false);
        AppleObject.SetActive(false);
        basketList = new List<GameObject>();


	}
    public void GameStart()
    {
        GameTitle.SetActive(false);
        PlayButton.SetActive(false);
        QuitButton.SetActive(false);
        AppleTreeObject.SetActive(true);
        AppleObject.SetActive(true);
        for (int i = 0; i < numBaskets; i++)
        {
         GameObject tBasketGO = Instantiate(basketPrefab) as GameObject;
         Vector3 pos = Vector3.zero;
         pos.y = basketBottomY + (basketSpacingY * i);
         tBasketGO.transform.position = pos;
         basketList.Add(tBasketGO);
        }
    }
    public void ResetGame()
    {
        Application.LoadLevel("_Scene_0");
    }
    public void OuitGame()
    {
        Application.Quit();
    }
    public void AppleDestroyed()
    {
        //消除所有下落的蘋果
        //GameObject.Find("name");  //用名字尋找GameObject子物件
        //GameObject.FindWithTag("Tag");  //用Tag尋找物件(第一個物件)
        //GameObject.FindGameObjectsWithTag("Tag");  //用Tag尋找物件(全部物件)
        //Resources.FindObjectsOfTypeAll(typeof(colliderTest)) //尋找Objects
        GameObject[] tAppleArray = GameObject.FindGameObjectsWithTag("Apple");

        foreach (GameObject tGO in tAppleArray)
        {
            Destroy(tGO);
        }
        //消除一個籃子

        //獲取basketList中最後一個籃子的序號
        int basketIndex = basketList.Count - 1;
        //取得對該籃子的引用
        GameObject tBasketGO = basketList[basketIndex];
        //從列表中清除該籃子並銷毀該遊戲對象
        basketList.RemoveAt(basketIndex);
        Destroy(tBasketGO);
        //重新開始遊戲,HighScorce.scorxe不會受影響
        if (basketList.Count == 0)
        {
            GameOver.SetActive(true);
            ResetButton.SetActive(true);
            QuitButton.SetActive(true);
            AppleTreeObject.SetActive(false);
            AppleObject.SetActive(false);
            //Application.LoadLevel("_Scene_0");//舊方法
            //EditorSceneManager.LoadScene("_Scene_0");
        }
    }

	
}
