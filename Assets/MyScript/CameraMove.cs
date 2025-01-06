using System.Threading;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Transform cameraTransform;
    private float speed=0.5f;
    private float timer;
    private float levelUpTime=6.0f;
    private GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cameraTransform=gameObject.transform;
        player=GameObject.Find("Player");

    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.isDead && GameManager.isActive && !GameManager.ispractice)
        {
            timer+=Time.deltaTime;
            if(timer>levelUpTime)
            {
                speed+=0.3f;
                timer=0;
                levelUpTime+=1;
            }
            transform.Translate(0,speed*Time.deltaTime,0);  

            if(player.transform.position.y+7<transform.position.y)
            {
                GameManager.isDead=true;
            }   
        }

        else if(GameManager.ispractice)
        {
            if(player.transform.position.y<0)
            {
                transform.position=new Vector3(transform.position.x,0,transform.position.z);
            }
            else
            {
                transform.position=new Vector3(transform.position.x,player.transform.position.y,transform.position.z);
            }
            
        }
        

        
        
    }
}
