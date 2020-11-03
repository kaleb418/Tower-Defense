using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAI:MonoBehaviour {

    [SerializeField] Transform towerHead;
    [SerializeField] Transform enemyObject;

    // Update is called once per frame
    void Update() {
        towerHead.LookAt(enemyObject);
    }
}
