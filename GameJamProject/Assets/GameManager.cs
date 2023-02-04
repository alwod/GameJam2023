using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public int defeatedEnemies = 0;
    [SerializeField] private List<GameObject> Enemies;
    [SerializeField] private int waveNumber = 1;
    [SerializeField] private bool spawnCooldown = false;
    [SerializeField] private bool end = false;



    private GameObject[] enemyPool;
    
    public GameObject waveCounterUIObject;
    private TextMeshProUGUI waveCounterUI;
    

    private byte poolPointer;
    
    private void Awake()
    {
        poolPointer = 0;
        defeatedEnemies = 0;
        waveCounterUI = waveCounterUIObject.GetComponent<TextMeshProUGUI>();

        enemyPool = new GameObject[128]; // 128 stored enemies

        for (int i = 0; i < enemyPool.Length; i++)
        {
            // Each enemy within the pool is randomly chosen from the list of provided enemies.
            enemyPool[i] = Instantiate(Enemies[Random.Range(0, Enemies.Count)], Vector3.zero, Quaternion.identity);
            enemyPool[i].SetActive(false);
        }

        StartCoroutine(WaveManager());
    }

    private void Update()
    {
        // if(Input.GetKeyDown(KeyCode.E))
        // {
        //     enemyPool[poolPointer].SetActive(true);
        //     enemyPool[poolPointer].transform.position = new Vector3(Random.Range(-30, 30), 2.85f, Random.Range(-30, 30));
        //     poolPointer++;
        //
        //     if (poolPointer >= enemyPool.Length)
        //     {
        //         poolPointer = 0;
        //     }
        // }
    }

    private IEnumerator SpawnCooldown(int seconds)
    {
        spawnCooldown = true;
        yield return new WaitForSeconds(seconds);
        spawnCooldown = false;
    }
    private IEnumerator WaveManager()
    {
        int waveLimit;
        int spawnedMobs;
        while (!end)
        {
            Debug.Log("Starting new wave!");
            string waveMessage = "Current wave: " + waveNumber;
            waveCounterUI.SetText(waveMessage);
            waveLimit = 5 * waveNumber;
            spawnedMobs = 0;
            while (spawnedMobs < waveLimit)
            {
                if (!spawnCooldown)
                {
                    enemyPool[poolPointer].SetActive(true);
                    enemyPool[poolPointer].transform.position = new Vector3(Random.Range(-30, 30), 2.85f, Random.Range(-30, 30));
                    poolPointer++;

                    if (poolPointer >= enemyPool.Length)
                    {
                        poolPointer = 0;
                    }
                    
                    Debug.Log("Spawning!");

                    spawnedMobs++;
                    StartCoroutine(SpawnCooldown(2));
                }

                yield return new WaitForSeconds(1);
            }
            
            Debug.Log("Got out");

            while (defeatedEnemies < waveLimit)
            {
                yield return new WaitForSeconds(2);
            }

            waveNumber++;
        }
    }

    /// <summary>
    /// Return all active enemies in the pool.
    /// </summary>
    public GameObject[] GetActiveEnemies()
    {
        return enemyPool.Where(enemy => enemy.gameObject.activeInHierarchy).ToArray();
    }

    /// <summary>
    /// Returns all enemies in the pool.
    /// </summary>
    public GameObject[] GetEnemies()
    {
        return enemyPool;
    }
}
