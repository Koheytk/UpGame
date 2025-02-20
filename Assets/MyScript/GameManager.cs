using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool ispractice;
    public static bool isDead;
    public static bool isActive;
    [SerializeField]
    private List<GameObject> tileMap;
    [SerializeField]
    private GameObject player;
    private SpawnPoint spawnPoint;
    private HitPoint hitPoint;
    private int HP=3;
    public static int MaxHP;

    private SoundManager soundManager;

    public int level;

    private bool end;

    private Scene scene;
    void Start()
    {      
        Coin.CoinCount=0;
        soundManager=GameObject.Find("SoundManager").GetComponent<SoundManager>();

        isActive=true; 
        isDead=false;
        MaxHP=3;
        hitPoint=GameObject.Find("HP").GetComponent<HitPoint>();
        level=2;
    }

    void Update()
    {
        if(player.transform.position.y>14*level && !ispractice)
        {
            tileMap[(level-2)%6].transform.position+=new Vector3(0,84,0);
            Debug.Log("StageUp");
            SpawnManager spawnManager=tileMap[(level-2)%6].GetComponent<SpawnManager>();
            spawnManager.ReSpawn();
            level++;
        }

        if(ispractice && player.transform.position.y<-4)
        {
            PracticeReset();
        }

        

        if(isDead)
        {
            GameOver();
        }
    }

    public void HitPointSet(int currentHP)
    {
        if(HP>currentHP)
        {
            hitPoint.images[HP-1].color=new Color32(0,0,0,255);
            HP=currentHP;
        }
        else if(HP<currentHP)
        {
            hitPoint.images[HP].color=new Color32(255,255,255,255);
            HP=currentHP;
        }
        if(currentHP==0 && !ispractice)
        {
            soundManager.Play("GameOver");
            isDead=true;
        }
    }

    private void GameOver()
    {
        if(!ispractice)
        {
            if(!end)
            {
                soundManager.Play("GameOver");
            }
            GameObject Game=GameObject.Find("Game");
            GameObject Over=GameObject.Find("Over");
            GameObject Score=GameObject.Find("LevelText");
            Text text =GameObject.Find("LevelText").GetComponent<Text>();

            float speed =4;

            Vector2 targetPosition1=new Vector2(0,80);
            Game.transform.localPosition=Vector2.Lerp(Game.transform.localPosition,targetPosition1,speed*Time.deltaTime);
            Vector2 targetPosition2=new Vector2(0,-30);
            Over.transform.localPosition=Vector2.Lerp(Over.transform.localPosition,targetPosition2,speed*Time.deltaTime);
            Vector2 targetPosition3=new Vector2(0,-150);
            Vector2 targetScale=new Vector2(4,4);
            Score.transform.localPosition=Vector2.Lerp(Score.transform.localPosition,targetPosition3,speed*Time.deltaTime);
            text.alignment=TextAnchor.MiddleCenter;
            Score.transform.localScale=Vector2.Lerp(Score.transform.localScale,targetScale,speed*Time.deltaTime);
            end=true;
        }
    }

    public void Restart()
    {
        isDead=!isDead;
        SceneManager.LoadScene("Title");
    }

    public void PracticeReset()
    {
        SceneManager.LoadScene("Practice");
    }
}
