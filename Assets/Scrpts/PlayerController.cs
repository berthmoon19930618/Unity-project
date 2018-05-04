using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        MovementRotate();
        Shoot();
    }
    public Vector3 ScreenVector;
    public float moveSpeed = 5;//移動速度
    //移動選轉功能
    void MovementRotate()
    {
        //Horizontal
        //Vertical
        //獲得玩家垂直方向輸入(上+下- W+S-)沿著Z軸移動
        float moveDirectionZ = Input.GetAxis("Vertical");
        //獲得玩家平行方向輸入(右+左- D+A-)沿著X軸移動
        float moveDirectionX = Input.GetAxis("Horizontal");
        //限制邊界
        LimitPosition(ref moveDirectionZ, ref moveDirectionX);
        Vector3 moveVector = new Vector3(moveDirectionX, 0, moveDirectionZ);
        this.transform.Translate(moveVector * Time.deltaTime * moveSpeed, Space.World);
        //主角左右移動時伴隨著左右選轉
        this.transform.rotation = Quaternion.Euler(0, 0, moveDirectionX * -30);
    }

    //限制邊界
    private void LimitPosition(ref float moveDirectionZ, ref float moveDirectionX)
    {
        //將主角世界座標轉成螢幕座標
        ScreenVector = Camera.main.WorldToScreenPoint(this.transform.position);

        //限制主角不能超過螢幕範圍

        //if (ScreenVector.x <= 20 && moveDirectionX <= 0){moveDirectionX = 0;}
        //if (ScreenVector.x>=Screen.width-20 && moveDirectionX >= 0){moveDirectionX = 0;}
        //if (ScreenVector.y <= 20 && moveDirectionZ <= 0){moveDirectionZ = 0;}
        //if (ScreenVector.y >= Screen.height-30 && moveDirectionZ >= 0){moveDirectionZ = 0;}

        if (ScreenVector.x <= 20 && moveDirectionX <= 0 || ScreenVector.x >= Screen.width - 20 && moveDirectionX >= 0)
        {
            moveDirectionX = 0;
        }
        if (ScreenVector.y <= 20 && moveDirectionZ <= 0 || ScreenVector.y >= Screen.height - 30 && moveDirectionZ >= 0)
        {
            moveDirectionZ = 0;
        }
    }

    public GameObject bullet;
    float nextTime = 0;//下一次子彈發射時間
    public float bulltinterval = 0.3f;//子彈發射間格時間

    //發射子彈
    void Shoot()
    {
        //如果玩家案下空白鍵
        if (Input.GetKey(KeyCode.Space)&& nextTime <= Time.time) //Time.time遊戲開始至今經過時間
        {
               //下一發子彈在0.3秒以後
                nextTime = Time.time + bulltinterval;

                //1.要創建的物體 bullet

                //2.位置 當前主角位置(戰機)this.transform.position

                //3.選轉角度 不選轉 Quaternion.identity
                GameObject.Instantiate(bullet, this.transform.position, Quaternion.identity);
        }
    }
}
