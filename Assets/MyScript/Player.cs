using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameManager gameManager;
    private Rigidbody2D playerRb;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpPower;
    public int jumpLevel=1;
    private bool isGround;
    [SerializeField]
    private LayerMask ground;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private int direction;
    private float timer;
    private int HP=3;

    [SerializeField]
    private GameObject shopText;
    private bool inShop;

    private GameObject Shop;

    private SoundManager soundManager;
    


    public int jump2wait;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        soundManager=GameObject.Find("SoundManager").GetComponent<SoundManager>();
        jump2wait=jumpLevel;
        gameManager=GameObject.Find("GameManager").GetComponent<GameManager>();
        playerRb=GetComponent<Rigidbody2D>();
        animator=GetComponent<Animator>();
        spriteRenderer=GetComponent<SpriteRenderer>();
        Shop=GameObject.Find("Shop");
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.isDead && GameManager.isActive)
        {
            //歩く
            float x=Input.GetAxisRaw("Horizontal");
            if(x!=0)
            {
                direction=(int)x;
            }
            playerRb.AddForce(Vector2.right*x*speed*Time.deltaTime);

            //ジャンプ
            if(isGround)
            {
                jump2wait=jumpLevel;
            }
            if(Input.GetButtonDown("Jump") && jump2wait>0){
                soundManager.Play("Jump");
                playerRb.linearVelocityY=0;
                playerRb.AddForce(Vector2.up*jumpPower,ForceMode2D.Impulse);
                jump2wait--;
            }

            //早くなりすぎないように
            float velX=playerRb.linearVelocityX;
            float velY=playerRb.linearVelocityY;
            if(Mathf.Abs(velX)>6)
            {
                if(velX>6)
                {
                    playerRb.linearVelocityX=6;
                }
                if(velX<-6)
                {
                    playerRb.linearVelocityX=-6;
                }
            }

            //向き反転
            if(x<0)
            {
                spriteRenderer.flipX=true;
            }
            else if(x>0)
            {
                spriteRenderer.flipX=false;
            }

            
            AnimationChange(velX,velY);

            if(Input.GetKeyDown(KeyCode.I) && inShop)
            {
                Shop.GetComponent<Shop>().ShopOpen();
            }
        }
        else if(GameManager.isDead)
        {
            Dead();
        }
        
        
        
    }

    void FixedUpdate()
    {
        isGround=false;

        Vector2 groundPos=new Vector2(transform.position.x,transform.position.y-0.5f);

        Vector2 groundArea =new Vector2(0.4f,0.05f);

        Debug.DrawLine(groundPos+groundArea,groundPos-groundArea,Color.red);

        isGround=Physics2D.OverlapArea(groundPos+groundArea,groundPos-groundArea,ground);
    }


    private void AnimationChange(float velX,float velY)
    {
        animator.SetFloat("Speed",Math.Abs(velX));

        if(velY>0)
        {
            animator.SetBool("IsJump",true);
            animator.SetBool("IsFall",false);
        }
        else if(velY<0)
        {
            animator.SetBool("IsFall",true);
            animator.SetBool("IsJump",false);
        }
        else
        {
            animator.SetBool("IsJump",false);
            animator.SetBool("IsFall",false);
        }
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if(collisionInfo.gameObject.CompareTag("Enemy"))
        {
            playerRb.AddForce(new Vector2(0,7),ForceMode2D.Impulse);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            soundManager.Play("Damage");
            Debug.Log("Player Damaged");
            Damaged();
            if(HP!=0)
            {
                HP--;
            }
            gameManager.HitPointSet(HP);
        }

        if(other.gameObject.CompareTag("Item"))
        {           
            soundManager.Play("Recover");
            Debug.Log("Player Recover");
            if(HP<GameManager.MaxHP)
            {
                HP++;
            }
            gameManager.HitPointSet(HP);
        }

        if(other.gameObject.CompareTag("Shop"))
        {           
            shopText.SetActive(true);
            inShop=true;
        }
    }

    

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Shop"))
        {           
            shopText.SetActive(false);
            inShop=false;
        }
    }

    void Damaged()
    {
        playerRb.AddForce(new Vector2(direction*5*(-1),7),ForceMode2D.Impulse);
        StartCoroutine(Blinking());         
    }

    void Dead()
    {
        
        CircleCollider2D circleCollider2D=GetComponent<CircleCollider2D>();
        circleCollider2D.enabled=false;
    }

    IEnumerator Blinking()
    {
        float duration = 1.0f; // 点滅する時間
        float interval = 0.1f; // 点滅の間隔
        Color originalColor = spriteRenderer.color;

        for (float t = 0; t < duration; t += interval)
        {
            
            spriteRenderer.enabled = !spriteRenderer.enabled; 
            
            yield return new WaitForSeconds(interval);
        }

        spriteRenderer.enabled=true;
        
    }

}
