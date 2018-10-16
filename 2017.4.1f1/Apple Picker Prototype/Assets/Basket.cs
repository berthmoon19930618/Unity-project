using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour {

    public GUIText scoreGT;
    void Start()
    {
        //查找ScoreCounter遊戲對象
        GameObject scoreGO = GameObject.Find("ScoreCounter");
        //獲取該遊戲對象的GUIText組件
        scoreGT = scoreGO.GetComponent <GUIText> ();
        //將初始分數設置為0
        scoreGT.text = "0";
    }
    // Update is called once per frame
    void Update ()
    {
        //從Input中獲取鼠標在畫面中的當前位置
        Vector3 mousePos2D = Input.mousePosition;

        //攝影機的z座標決定在三維空間中將滑鼠沿著z軸向前移動多遠
        mousePos2D.z = -Camera.main.transform.position.z;

        //將該點從二維畫面空間轉換成三維遊戲空間
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        //將籃子的x位置移動到鼠標處的x位置外
        Vector3 pos = this.transform.position;
        pos.x = mousePos3D.x;
        this.transform.position = pos;
	}
    void OnCollisionEnter(Collision coll)
    {
        //檢查與籃子碰撞的是什麼對象
        GameObject collidedWith = coll.gameObject;
        if (collidedWith.tag == "Apple")
        {
            Destroy(collidedWith);
        }
        //將scoreGT轉換為整數值
        int score = int.Parse(scoreGT.text);//int.parse(textbox1.text); 就是把textbox中的文字轉成數值的語法
        //每次接住蘋果就為玩家加分
        score += 100;
        //將分數轉換為字符串顯示在畫面上
        scoreGT.text = score.ToString();
        if (score > HighScore.score)
        {
            HighScore.score = score;
        }
    }

}
