using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]

public class ParticleSoundEffect:MonoBehaviour {

    [SerializeField] AudioClip birthSound;
    [SerializeField] AudioClip deathSound;

    private ParticleSystem pSystem;
    private AudioSource audioSource;
    private int previousNumParticles = 0;

    // Start is called before the first frame update
    void Start() {
        pSystem = GetComponent<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        if(birthSound != null && pSystem.particleCount > previousNumParticles) {
            audioSource.PlayOneShot(birthSound);
        }
        if(deathSound != null && pSystem.particleCount < previousNumParticles) {
            audioSource.PlayOneShot(deathSound);
        }
        previousNumParticles = pSystem.particleCount;
    }
}
