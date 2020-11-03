using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover:MonoBehaviour {

    [SerializeField] [Range(0.1f, 10f)] float enemyMoveSpeed = 1f;

    private Pathfinder pathfinder;
    private Waypoint currentWP;

    void Start() {
        pathfinder = GetComponent<Pathfinder>();
        StartCoroutine(FollowPath(pathfinder.GetPath()));
    }

    private IEnumerator FollowPath(List<Waypoint> path) {
        foreach(Waypoint nextWP in path) {
            transform.position = new Vector3(nextWP.GetCubeCoords().x * 10f, 10, nextWP.GetCubeCoords().y * 10f);
            currentWP = nextWP;
            yield return new WaitForSeconds(enemyMoveSpeed);
        }
    }
}
