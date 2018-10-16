using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameDriver : MonoBehaviour {

    public PlayerController playinput;
    public PlayerCharacter character;
    public TankCharacter enemy;

    [Header("======字標======")]
    public GameObject title;
    public GameObject lose;
    public GameObject win;

    [Header("======按鈕======")]
    public GameObject play;
    public GameObject restart;
    public GameObject quit;

    [Header("======物件======")]
    public GameObject player;
    public GameObject tank;
    private bool isTank;

    [Header("======條件======")]
    public bool isPlay;
    

    // Use this for initialization
    void Awake ()
    {
        playinput = player.GetComponent<PlayerController>();
        character = player.GetComponent<PlayerCharacter>();
        enemy = tank.GetComponent<TankCharacter>();
        
        title.SetActive(true);
        lose.SetActive(false);
        win.SetActive(false);
        play.SetActive(true);
        quit.SetActive(true);
        restart.SetActive(false);
        player.SetActive(true);
        tank.SetActive(false);
        playinput.Go = false;

    }

    // Update is called once per frame
    void Update ()
    {

        if (isPlay == true&&isTank==true)
        {
           
            title.SetActive(false);

            play.SetActive(false);
            quit.SetActive(false);
           
            tank.SetActive(true);
            isTank = false;
        }
        if (isPlay == true && character.Go==false)
        {
            lose.SetActive(true);
            restart.SetActive(true);
            quit.SetActive(true);
            isPlay = false;
            playinput.Go = false;
            print("Game Over");
        }
        if (isPlay == true && enemy.Go == false)
        {
            win.SetActive(true);
            restart.SetActive(true);
            quit.SetActive(true);
            isPlay = false;
            playinput.Go = false;
            print("You Win!");
        }


    }

    public void Play()
    {
        isPlay = true;
        isTank = true;
        character.Go = true;
        enemy.Go = true;
        playinput.Go = true;

    }

    public void Restart()
    {
        SceneManager.LoadScene("Main");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
