using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> Enemies;

    private GameObject[] enemyPool;

    private byte poolPointer;

    [SerializeField] private GameObject DMGText;

    private void Awake()
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

    public void InstantiateDamageNumbers(int incDamage, GameObject enemy)
    {
        GameObject damageText = Instantiate(DMGText,
            Vector3.one,
            Quaternion.identity);

        // set damage text position to above the enemy in screen space.
        //damageText.transform.position = Camera.main.WorldToScreenPoint(enemy.transform.position);
        damageText.GetComponent<TextMeshPro>().transform.position =
            Camera.main.WorldToScreenPoint(enemy.transform.position);

        damageText.GetComponent<TextMeshPro>().SetText(incDamage.ToString());

    }
}
