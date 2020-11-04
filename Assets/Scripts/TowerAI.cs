using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAI:MonoBehaviour {

    [SerializeField] [Range(1f,300f)] float towerRange;
    [SerializeField] Transform towerHead;
    [SerializeField] Transform enemyPos;
    [SerializeField] ParticleSystem laserParticles;
    [SerializeField] ParticleSystem smokeParticles;

    // Update is called once per frame
    void Update() {
        FireAtRange();
    }

    private void FireAtRange() {
        if(FindEnemyRange(enemyPos) <= towerRange) {
            towerHead.LookAt(enemyPos);
            if(!laserParticles.isPlaying) { laserParticles.Play(); }
            if(!smokeParticles.isPlaying) { smokeParticles.Play(); }
        } else {
            if(laserParticles.isPlaying) { laserParticles.Stop(); }
            if(smokeParticles.isPlaying) { smokeParticles.Stop(); }
        }
    }

    private float FindEnemyRange(Transform enemyTransform) {
        return Vector3.Distance(towerHead.position, enemyTransform.position);
    }
}
