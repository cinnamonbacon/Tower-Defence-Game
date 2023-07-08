using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour {

    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header("Attributes")]
    [SerializeField] private int baseEnemies = 8;           // number of enemies per wave
    [SerializeField] private float enemiesPerSecond = 0.5f; // enemy spawn rate
    [SerializeField] private float waveInterval = 5f;       // time between end of last wave and start of next wave
    [SerializeField] private float difficultyScalingFactor = 0.75f;

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    private int currentWave = 1;        // wave index
    private float timeSinceLastSpawn;
    private int enemiesAlive;           // when 0 indicates the end of a wave
    private int enemiesToSpawn;         // remaining enemies to spawn in a given wave
    private bool isSpawning = false;

    private void Awake() {
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    void Start() {
        StartCoroutine(StartWave());
    }

    void Update() {
        if (!isSpawning) return;

        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= 1f / enemiesPerSecond && enemiesToSpawn > 0) {  // T = 1/f
            SpawnEnemy();
            enemiesToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }

        if (enemiesAlive == 0 && enemiesToSpawn == 0)
            EndWave();
    }

    private void EnemyDestroyed() {
        enemiesAlive--;
    }

    private void SpawnEnemy() {
        GameObject prefabToSpawn = enemyPrefabs[0];
        Instantiate(prefabToSpawn, Manager.main.startPoint.position, Quaternion.identity);  // Quaternion.identity indicates that there is no rotation
    }

    private IEnumerator StartWave() {
        yield return new WaitForSeconds(waveInterval);
        isSpawning = true;
        enemiesToSpawn = EnemiesPerWave();
    }

    private void EndWave() {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;
        StartCoroutine(StartWave());
    }

    private int EnemiesPerWave() {
        // NOTE: if you wish to have preset enemies per wave, you can instead have a matrix of enemy quantities based on wave and type
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));
    }
}
