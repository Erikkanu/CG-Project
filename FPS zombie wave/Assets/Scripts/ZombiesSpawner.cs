using System.Collections;
using UnityEngine;

public class ZombiesSpawner : MonoBehaviour
{
    public GameObject theEnemy;
    public GameObject zombiesContainer; // Reference to the Zombies GameObject in the scene
    public int enemyCount;

    void Start()
    {
        StartCoroutine(EnemyDrop());
    }

    IEnumerator EnemyDrop()
    {
        while (enemyCount < 10)
        {
            float xPos = Random.Range(390f, 518f);
            float yPos = Random.Range(133f, 138f);
            float zPos = Random.Range(597f, 651f);

            // Instantiate theEnemy as a child of the Zombies GameObject
            GameObject newEnemy = Instantiate(theEnemy, new Vector3(xPos, yPos, zPos), Quaternion.identity);
            newEnemy.transform.parent = zombiesContainer.transform; // Set parent to Zombies GameObject

            yield return new WaitForSeconds(1f);
            enemyCount++;
        }
    }
}
