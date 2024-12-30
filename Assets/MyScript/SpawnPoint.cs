using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField]
    private GameObject spawned;
    [SerializeField]
    private GameObject Coin;
    public GameObject spawnedObject;
    private int randomSpawn;
    private GameManager gameManager;
    public int currentLevel=2;
    void Start()
    {
        gameManager=GameObject.Find("GameManager").GetComponent<GameManager>();
        randomSpawn=Random.Range(0,3);
        switch(randomSpawn)
        {
            case 0:
            Spawn();
            break;

            case 1:
            if(Coin!=null)
            {
                SpawnCoin();
            }
            break;

            case 2:
            break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(currentLevel<gameManager.level)
        {
            currentLevel=gameManager.level;
        }
    }

    public void Spawn()
    {
        spawnedObject=Instantiate(spawned,transform.position,Quaternion.identity);
    }

    public void SpawnCoin()
    {
        spawnedObject=Instantiate(Coin,transform.position,Quaternion.identity);
    }

    public void RandomSpawn()
    {
        gameManager=GameObject.Find("GameManager").GetComponent<GameManager>();
        randomSpawn=Random.Range(0,3);
        switch(randomSpawn)
        {
            case 0:
            Spawn();
            break;

            case 1:
            if(Coin!=null)
            {
                SpawnCoin();
            }
            break;

            case 2:
            break;
        }
    }
}
