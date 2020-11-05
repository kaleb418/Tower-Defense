using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner:MonoBehaviour {

    [SerializeField] GameObject enemyPrefab;
    [SerializeField] Waypoint startingWaypoint;
    [SerializeField] Waypoint endingWaypoint;
    [SerializeField] [Range(0.1f, 10f)] float enemySpeed;
    [SerializeField] [Range(1f, 50f)] float enemyHealth;
    [SerializeField] bool spawnEnabled = true;
    [SerializeField] float spawnFrequencyPerSecond;

    // Start is called before the first frame update
    void Awake() {
        StartCoroutine(StartEnemySpawn());
    }

    public Waypoint GetStartingWaypoint() {
        return startingWaypoint;
    }

    public Waypoint GetEndingWaypoint() {
        return endingWaypoint;
    }

    public float GetEnemySpeed() {
        return enemySpeed;
    }

    public float GetEnemyHealth() {
        return enemyHealth;
    }

    private IEnumerator StartEnemySpawn() {
        while(true) {
            yield return new WaitForSeconds(spawnFrequencyPerSecond);
            if(spawnEnabled) {
                SpawnEnemy();
            }
        }
    }

    private void SpawnEnemy() {
        Vector3 startPos = new Vector3(transform.position.x, 10, transform.position.z);
        GameObject newEnemy = Instantiate(enemyPrefab, startPos, Quaternion.identity);
        newEnemy.transform.parent = transform;
    }
}
