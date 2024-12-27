using System;
using UnityEngine;
using UnityEngine.UI;

public class UILevelManager : MonoBehaviour
{
    private GameObject player;
    private int level=0;
    private int currentLevel;
    private Text LevelText;
    void Start()
    {
        player=GameObject.Find("Player");
        LevelText=GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        currentLevel=Mathf.FloorToInt(player.transform.position.y)+4;
        if(currentLevel>level)
        {
            level=currentLevel;
            LevelText.text=level+"m";
        }
    }
}
