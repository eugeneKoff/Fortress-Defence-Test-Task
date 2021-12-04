using System.Collections;
using System;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemies;

    public Transform[] spawnPoints;

    public GameObject parentObj;

    public float spawnFrequency;

    public int enemiesQuantity;

    public int enemiesKilled;

    public event Action AllEnemiesDefeated;

   private void Start()
    {
        enemiesKilled = 0;

        StartCoroutine(SpawnEnemy());

    }



    private IEnumerator SpawnEnemy()
    {
        int num = enemiesQuantity;

        while (num > 0)
        {
            foreach (var point in spawnPoints)
            {
                if (num <= 0)
                {
                    break;
                }


                var randomEnemy = enemies[UnityEngine.Random.Range(0, enemies.Length)];

                EnemyController controller = Instantiate(randomEnemy, point.position, Quaternion.identity, parentObj.transform).GetComponent<EnemyController>();

                controller.SubscribeDeath(OnEnemyDefeated);

                num--;

                

                yield return new WaitForSeconds(spawnFrequency);
            }

            
        }
        

    }

    private void OnEnemyDefeated()
    {
        enemiesKilled++;

        if(enemiesKilled >= enemiesQuantity)
        {
            AllEnemiesDefeated?.Invoke();
        }

    }
}
