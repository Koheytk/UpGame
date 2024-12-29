using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    private Rigidbody2D enemyRb;
    private int forward=1;
    private bool isNotEdge=true;
    [SerializeField]
    private LayerMask ground;
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private GameObject player;
    private bool isDead;
    [SerializeField]
    CircleCollider2D circleCollider2D;
    [SerializeField]
    CircleCollider2D circleCollider2Dtrigger;
    [SerializeField]
    BoxCollider2D boxCollider2D;

    private SoundManager soundManager;
    void Start()
    {
        soundManager=GameObject.Find("SoundManager").GetComponent<SoundManager>();
        enemyRb=GetComponent<Rigidbody2D>();
        spriteRenderer=GetComponent<SpriteRenderer>();
        player=GameObject.Find("Player");
    }

   

    void Update()
    {
        if(!isDead)
        {
            enemyRb.linearVelocityX=1*forward;

            Vector2 enemyGroundPos=new Vector2(transform.position.x+0.6f*forward,transform.position.y-0.5f);

            Vector2 groundArea =new Vector2(forward*-0.4f,0.4f);

            Debug.DrawLine(enemyGroundPos,enemyGroundPos-groundArea,Color.red);

            isNotEdge=Physics2D.OverlapArea(enemyGroundPos,enemyGroundPos-groundArea,ground);
            if(!isNotEdge)
            {
                if(spriteRenderer.flipX)
                {
                    spriteRenderer.flipX=false;
                }
                else
                {
                    spriteRenderer.flipX=true;
                }
                forward*=-1;
            }

            if(player.transform.position.y-20>transform.position.y)
            {
                Destroy(gameObject);
            }
        }
        
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if(collisionInfo.gameObject.CompareTag("Player"))
        {
            Debug.Log("PlayerHit");
            Dead();
        }
    }

    private void Dead()
    {
        soundManager.Play("Attack");

        enemyRb.AddForce(new Vector2(0,2),ForceMode2D.Impulse);
        spriteRenderer.flipY=true;

        circleCollider2D.enabled=false;
        boxCollider2D.enabled=false;
        circleCollider2Dtrigger.enabled=false;
    }
}
