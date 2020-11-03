using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint:MonoBehaviour {

    const float GRID_SIZE = 10f;

    public float GetGridSize() {
        return GRID_SIZE;
    }

    public Vector2Int GetCubeCoords() {
        return new Vector2Int(
            Mathf.RoundToInt(transform.position.x / GRID_SIZE),
            Mathf.RoundToInt(transform.position.z / GRID_SIZE)
        );
    }
}
