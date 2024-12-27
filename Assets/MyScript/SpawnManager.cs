using System.Collections.Generic;
using UnityEngine;


public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private List<SpawnPoint>  spawnPoint;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ReSpawn()
    {
        for(int i=0;i<spawnPoint.Count;i++)
        {
            spawnPoint[i].GetComponent<SpawnPoint>().Spawn();
        }
    }
}
