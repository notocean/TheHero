using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    public static EnemySpawnController Instance { get; private set; }

    [SerializeField] List<GameObject> enemyPrefabs = new List<GameObject>();
    [SerializeField] List<Transform> spawnPos = new List<Transform>();

    [SerializeField] private int maxEnemyCount = 6;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
    }

    private void Start() {
        for (int i = 0; i < maxEnemyCount; i++) {
            int enemyType = Random.Range(0, enemyPrefabs.Count);
            Instantiate(enemyPrefabs[enemyType], spawnPos[i].position, enemyPrefabs[enemyType].transform.rotation);
        }
    }

    public void SpawnEnemy() {
        int enemyType = Random.Range(0, enemyPrefabs.Count);
        int enemyPos = Random.Range(0, spawnPos.Count);
        Instantiate(enemyPrefabs[enemyType], spawnPos[enemyPos].position, enemyPrefabs[enemyType].transform.rotation);
    }
}
