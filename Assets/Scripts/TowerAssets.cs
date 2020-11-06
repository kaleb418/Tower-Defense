using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAssets:MonoBehaviour {

    [SerializeField] GameObject blueTowerParent;
    [SerializeField] GameObject blueTowerPrefab;
    [SerializeField] GameObject yellowTowerParent;
    [SerializeField] GameObject yellowTowerPrefab;

    public GameObject GetBlueTowerParent() {
        return blueTowerParent;
    }

    public GameObject GetBlueTowerPrefab() {
        return blueTowerPrefab;
    }

    public GameObject GetYellowTowerParent() {
        return yellowTowerParent;
    }

    public GameObject GetYellowTowerPrefab() {
        return yellowTowerPrefab;
    }
}
