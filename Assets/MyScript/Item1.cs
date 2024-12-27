using Unity.VisualScripting;
using UnityEngine;

public class Item1 : MonoBehaviour
{
    private GameObject player;
    void Start()
    {
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
        Destroy(gameObject);
        }
    }
}
