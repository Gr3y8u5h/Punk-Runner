using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefab;
    private Vector3 spawnPos = new Vector3(35, 0, 0);
    private float startDelay = 2;
    private float repeatRate = 2;
    private PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        Invoke("SpawnObstacle", repeatRate);
    }



    // Update is called once per frame
    void Update()
    {

    }

    void SpawnObstacle ()
    {
        float randomInterval = Random.Range(2, 5);
        Invoke("SpawnObstacle", randomInterval);

        int obIndex = Random.Range(0, obstaclePrefab.Length);

        if(playerControllerScript.gameOver == false)
        {
            
            Instantiate(obstaclePrefab[obIndex], spawnPos, obstaclePrefab[obIndex].transform.rotation);
        }
        
    }
}
