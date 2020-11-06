using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint:MonoBehaviour {

    const float GRID_SIZE = 10f;
    [SerializeField] bool isWalkable = true;
    [SerializeField] bool isBuildable = true;

    private TowerAssets worldInteractionManager;
    private GameObject blueTowerParent;
    private GameObject blueTowerPrefab;
    private GameObject yellowTowerParent;
    private GameObject yellowTowerPrefab;

    void Start() {
        worldInteractionManager = transform.parent.transform.parent.GetComponent<TowerAssets>();
        blueTowerParent = worldInteractionManager.GetBlueTowerParent();
        blueTowerPrefab = worldInteractionManager.GetBlueTowerPrefab();
        yellowTowerParent = worldInteractionManager.GetYellowTowerParent();
        yellowTowerPrefab = worldInteractionManager.GetYellowTowerPrefab();
    }

    private void OnMouseDown() {
        BuildTower(blueTowerPrefab, blueTowerParent);
        BlockWaypoint();
    }

    private void BuildTower(GameObject towerPrefab, GameObject towerParent) {
        if(isBuildable) {
            GameObject newTower = Instantiate(towerPrefab, new Vector3(transform.position.x, 10.5f, transform.position.z), Quaternion.identity);
            newTower.transform.parent = towerParent.transform;
        }
    }

    private void BlockWaypoint() {
        isWalkable = false;
        isBuildable = false;
    }

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
