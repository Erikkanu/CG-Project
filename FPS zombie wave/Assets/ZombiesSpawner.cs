using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiesSpawner : MonoBehaviour
{

    public GameObject theEnemy;
    public float xPos;
    public float yPos;
    public float zPos;
    public int enemyCount;

    void Start()
    {
        StartCoroutine(EnemyDrop());
    }

    IEnumerator EnemyDrop()
    {
        while(enemyCount < 10)
        {
            xPos = Random.Range(390, 518);
            yPos = Random.Range(133, 138);
            zPos = Random.Range(597, 651);
            Instantiate(theEnemy, new Vector3(xPos, yPos, zPos), Quaternion.identity);
            yield return new WaitForSeconds(1f);
            enemyCount++;
        }
    }

}
