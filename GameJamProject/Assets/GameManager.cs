using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> Enemies;

    private GameObject[] enemyPool;

    private byte poolPointer;

    private void Start()
    {
        poolPointer = 0;

        enemyPool = new GameObject[128]; // 128 stored enemies

        for (int i = 0; i < enemyPool.Length; i++)
        {
            // Each enemy within the pool is randomly chosen from the list of provided enemies.
            enemyPool[i] = Instantiate(Enemies[Random.Range(0, Enemies.Count)], Vector3.zero, Quaternion.identity);
            enemyPool[i].SetActive(false);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            enemyPool[poolPointer].SetActive(true);
            enemyPool[poolPointer].transform.position = new Vector3(Random.Range(-30, 30), 2.85f, Random.Range(-30, 30));
            poolPointer++;

            if (poolPointer >= enemyPool.Length)
            {
                poolPointer = 0;
            }
        }
    }
}
