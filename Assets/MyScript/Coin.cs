using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    private GameObject player;
    public static int CoinCount;

    private SoundManager soundManager;
    void Start()
    {
        soundManager=GameObject.Find("SoundManager").GetComponent<SoundManager>();
        player=GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.y-20>transform.position.y)
            {
                Destroy(gameObject);

            }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            soundManager.Play("Coin");
            Destroy(gameObject);
            CoinCount++;
            
            CoinSet();
        }
    }

    public static void CoinSet()
    {
        Text text=GameObject.Find("CoinCount").GetComponent<Text>();
        text.text=""+CoinCount;
    }
}
