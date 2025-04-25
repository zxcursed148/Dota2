using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemySpawn : MonoBehaviourPunCallbacks
{
    [SerializeField] private float timeToSpawnEnemy = 5f;
    [SerializeField] private List<Transform> enemyPositions;
    [SerializeField] private string enemyPrefabName = "Enemy"; // Укажи имя префаба

    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
        StartCoroutine(SpawnEnemy());
        }
    }

    private IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(timeToSpawnEnemy);

        while (enemyPositions.Count > 0)
        {
            int index = Random.Range(0, enemyPositions.Count);

            PhotonNetwork.Instantiate(enemyPrefabName,
                enemyPositions[index].position, Quaternion.identity);

            enemyPositions.RemoveAt(index);
            yield return new WaitForSeconds(timeToSpawnEnemy);
        }
    }
}
