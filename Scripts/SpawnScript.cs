using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnScript : MonoBehaviour
{
    public GameObject[] enemies;
    public Transform leftSpawner;
    public Transform RightSpawner;
    public Transform player;
    
    [SerializeField] private float spawnRate = .5f;

    private float spawnTimer = 0f;
    private int spawnCounter = 0;
    
    void Start ()
    {
        
	}
	
	void Update ()
    {
        if (spawnCounter >= enemies.Length)
        {
            return;
        }

        spawnTimer += Time.deltaTime;

        if (spawnTimer > 1f / spawnRate)
        {
            GameObject Enemy = Instantiate(enemies[spawnCounter], Random.Range(0f, 1f) < .5f ? leftSpawner.position + Vector3.up * Random.Range(-6f, 6f) : RightSpawner.position + Vector3.up * Random.Range(-6f, 6f), Quaternion.identity);
            Enemy.GetComponent<EnemyScript>().player = player;

            spawnCounter += 1;
            spawnTimer = 0f;

            if (spawnCounter == 10)
            {
                spawnRate /= 2;
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
	}
}
