using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour {

    
    public float horizontalSpeed = 100f;
    public float veticallSpeed = 80f;
    public float cameraDampValue = 0.05f;//相機追趕cameraHandle速度
    //public PlayerInput pi;
    [SerializeField]
    private float tempEulerx;
    [SerializeField]
    private PlayerInput pi;
    [SerializeField]
    private GameObject playerHandle;
    [SerializeField]
    private GameObject cameraHandle;
    [SerializeField]
    private GameObject modle;//角色模型
    [SerializeField]
    private GameObject mainCamera;//主攝影機

    private Vector3 velocityCameraDamp;//camera位置的參照空間

    // Use this for initialization
    void Awake ()
    {
        cameraHandle = transform.parent.gameObject;//控制垂直就投控制
        playerHandle = cameraHandle.transform.parent.gameObject;//控制水平就投控制鏡頭
        pi = playerHandle.GetComponent<PlayerInput>();
        tempEulerx = 20f;

        modle = playerHandle.GetComponent<ActorControll>().modle;
        mainCamera = Camera.main.gameObject;//取得主要在作用的攝影機

    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        Vector3 tempModleEuler = modle.transform.eulerAngles;//將模型的歐拉角獨立出來不受水平鏡頭控制

        //控制水平就投控制鏡頭
        playerHandle.transform.Rotate(Vector3.up, pi.JRight * horizontalSpeed * Time.fixedDeltaTime);

        //控制垂直就投控制
        //cameraHandle.transform.Rotate(cameraHandle.transform.right, pi.JUp * veticallSpeed * Time.deltaTime, Space.World);//此方法無法限制範圍
        tempEulerx -= pi.JUp * veticallSpeed * Time.fixedDeltaTime;//因此使用每次偵測鏡頭重直控制信號(pi.JUp)(有,無)有就遞減veticallSpeed的方法
        tempEulerx = Mathf.Clamp(tempEulerx, -40, 30);//將範圍限制在-40至30度之間
        cameraHandle.transform.localEulerAngles = new Vector3(tempEulerx, 0, 0);//將cameraHandle的自身歐拉角的設為tempEulerx

        modle.transform.eulerAngles = tempModleEuler;//最後再將模型歐拉角指定為鏡頭還沒水平移動時的狀態，如此一來模型的歐拉角就不會跟著playerHandle的歐拉角了，但是控制還是跟著playerHandle，因此視覺上鏡頭水平到哪中心點即為移動目標方向。

        //優化攝影機動態 改為跟蹤感

        //camera.transform.position = transform.position;//將攝影機位置指定倒CameraPos所在位置

        ////製造攝影機追趕效果方法一
        //camera.transform.position = Vector3.Lerp(camera.transform.position, transform.position, cameraDampValue);

        //製造攝影機追趕效果方法二
        mainCamera.transform.position = Vector3.SmoothDamp(mainCamera.transform.position, transform.position, ref velocityCameraDamp,cameraDampValue);

        //非教程中的，我不想在我做攝影機控制也有追趕效果所使用的方法
        if (pi.JRight!=0||pi.JUp!= 0)
        {
            mainCamera.transform.position = transform.position;
        }

        //將攝影機歐拉角指定為CameraPos的歐拉角
        mainCamera.transform.eulerAngles = transform.eulerAngles;
    }

}
