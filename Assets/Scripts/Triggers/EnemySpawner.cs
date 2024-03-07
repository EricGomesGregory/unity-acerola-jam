using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeReference]
    private EnemyController enemyType;
    [SerializeField]
    private bool spawnOnStart = false;
    [SerializeField]
    private List<Transform> spawnPoints;


    private Coroutine spawned;

    private void Start() {
        if (!spawnOnStart) return;
        Spawn();
    }

    public void Spawn(float delay = 0f) {
        if (spawned != null) return;
        spawned = StartCoroutine(SpawnEnemies(delay));
    }

    public void ResetSpawn() {
        spawned = null;
    }

    private IEnumerator SpawnEnemies(float delay) {
        if (delay > 0) {
            yield return new WaitForSeconds(delay);
        }
        foreach (var spawnPoint in spawnPoints) {
            Instantiate(enemyType, spawnPoint.position, spawnPoint.rotation);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        foreach (var spawnPoint in spawnPoints) {
            Gizmos.DrawWireSphere(spawnPoint.position, 0.5f);
        }
    }
}
