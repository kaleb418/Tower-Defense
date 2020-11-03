using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover:MonoBehaviour {

    private Pathfinder pathfinder;
    private List<Waypoint> path;

    void Start() {
        pathfinder = GetComponent<Pathfinder>();
        path = pathfinder.GetPath();
        StartCoroutine(MoveEnemySequence());
    }

    private IEnumerator MoveEnemySequence() {
        foreach(Waypoint nextWP in path) {
            transform.position = new Vector3(nextWP.GetCubeCoords().x * 10f, 10, nextWP.GetCubeCoords().y * 10f);
            yield return new WaitForSeconds(1.5f);
        }
    }
}
