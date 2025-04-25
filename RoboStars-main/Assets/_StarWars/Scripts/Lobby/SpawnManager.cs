using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;
    [SerializeField] private Transform[] spawnPoints;

    private void Awake()
    {
        instance = this;
    }

    public Transform GetSpawnPoint()
    {
        if (spawnPoints.Length > 0)
        {
            return spawnPoints[Random.Range(0, spawnPoints.Length)];
        }
        Debug.LogError("No spawn points available!");
        return null;
    }
}
