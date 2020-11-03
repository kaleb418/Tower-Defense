using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint:MonoBehaviour {

    const float GRID_SIZE = 10f;
    [SerializeField] private bool isWalkable = true;
    [SerializeField] private bool isBuildable = true;

    public float GetGridSize() {
        return GRID_SIZE;
    }

    public bool IsWalkable() {
        return isWalkable;
    }

    public bool IsBuildable() {
        return isBuildable;
    }

    public Vector2Int GetCubeCoords() {
        return new Vector2Int(
            Mathf.RoundToInt(transform.position.x / GRID_SIZE),
            Mathf.RoundToInt(transform.position.z / GRID_SIZE)
        );
    }

    public override string ToString() {
        return "(" + this.GetCubeCoords().x + ", " + this.GetCubeCoords().y + ")";
    }
}
