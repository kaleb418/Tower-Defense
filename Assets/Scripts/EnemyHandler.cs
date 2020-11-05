using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler:MonoBehaviour {

    private Pathfinder pathfinder;
    private Waypoint currentWP;
    private Vector3 startPos;
    private Vector3 targetPos;
    private float t;
    private bool hasStartedMoving = false;
    private float moveSpeed;
    private float health;

    void Start() {
        pathfinder = GetComponent<Pathfinder>();
        EnemySpawner spawner = transform.parent.GetComponent<EnemySpawner>();
        moveSpeed = spawner.GetEnemySpeed();
        health = spawner.GetEnemyHealth();
    }

    void Update() {
        if(!hasStartedMoving && pathfinder.IsPathDefined()) {
            StartCoroutine(FollowPath(pathfinder.GetPath()));
            hasStartedMoving = true;
        }

        if(hasStartedMoving) {
            t += Time.deltaTime / moveSpeed;
            transform.position = Vector3.Lerp(startPos, targetPos, t);
        }
    }

    void OnParticleCollision(GameObject other) {
        if(other.name == "Laser Beam") {
            float towerDmg = other.transform.parent.transform.parent.transform.parent.GetComponent<TowerHandler>().GetDamage();
            health -= towerDmg;
            if(health <= 0) {
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator FollowPath(List<Waypoint> path) {
        foreach(Waypoint nextWP in path) {
            startPos = transform.position;
            targetPos = new Vector3(nextWP.GetCubeCoords().x * 10f, 10, nextWP.GetCubeCoords().y * 10f);
            t = 0;
            currentWP = nextWP;
            yield return new WaitForSeconds(moveSpeed);
        }
    }
}
