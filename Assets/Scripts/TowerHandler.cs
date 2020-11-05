using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerHandler:MonoBehaviour {

    [SerializeField] [Range(1f,300f)] float towerRange;
    [SerializeField] [Range(0f, 50f)] float damage;
    [SerializeField] [Range(1f, 100f)] float shotsPerSecond;
    [SerializeField] Transform towerHead;
    [SerializeField] Transform enemyPos;
    [SerializeField] ParticleSystem laserParticles;
    [SerializeField] ParticleSystem smokeParticles;

    void Start() {
        var emission = laserParticles.emission;
        emission.rateOverTime = shotsPerSecond;

        var smokeEmission = smokeParticles.emission;
        smokeEmission.rateOverTime = shotsPerSecond;
    }
    // Update is called once per frame
    void Update() {
        FindClosestEnemy();
        FireAtRange();
    }

    public float GetDamage() {
        return damage;
    }

    private void FindClosestEnemy() {
        EnemyHandler[] enemyArr = FindObjectsOfType<EnemyHandler>();
        if(enemyArr.Length == 0) {
            enemyPos = null;
            return;
        }
        EnemyHandler closestEnemy = enemyArr[0];
        foreach(EnemyHandler enemy in enemyArr) {
            if(Vector3.Distance(enemy.transform.position, towerHead.transform.position) < Vector3.Distance(closestEnemy.transform.position, towerHead.transform.position)) {
                closestEnemy = enemy;
            }
        }
        enemyPos = closestEnemy.transform;
    }

    private void FireAtRange() {
        if(enemyPos == null) {
            StopFiring();
            return;
        }
        if(FindEnemyRange(enemyPos) <= towerRange) {
            StartFiring();
        } else {
            StopFiring();
        }
    }

    private void StopFiring() {
        if(laserParticles.isPlaying) { laserParticles.Stop(); }
        if(smokeParticles.isPlaying) { smokeParticles.Stop(); }
    }

    private void StartFiring() {
        towerHead.LookAt(enemyPos);
        if(!laserParticles.isPlaying) { laserParticles.Play(); }
        if(!smokeParticles.isPlaying) { smokeParticles.Play(); }
    }

    private float FindEnemyRange(Transform enemyTransform) {
        return Vector3.Distance(towerHead.position, enemyTransform.position);
    }
}
