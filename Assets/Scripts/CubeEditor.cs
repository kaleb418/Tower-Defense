using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
[RequireComponent(typeof(Waypoint))]

public class CubeEditor:MonoBehaviour {

    [SerializeField] TextMesh textMesh;

    private Waypoint waypoint;
    private float gridSize;

    void Start() {
        waypoint = gameObject.GetComponent<Waypoint>();
        gridSize = waypoint.GetGridSize();
    }

    void Update() {
        SnapCube();
        UpdateCoordinates();
    }

    private void SnapCube() {
        Vector3 snapVector = new Vector3(
            waypoint.GetCubeCoords().x * gridSize,
            0,
            waypoint.GetCubeCoords().y * gridSize
        );

        transform.position = snapVector;
    }

    private void UpdateCoordinates() {
        string labelText = waypoint.GetCubeCoords().x + ", " + waypoint.GetCubeCoords().y;
        textMesh.text = labelText;
        gameObject.name = "Cube " + "(" + labelText + ")";
    }
}
