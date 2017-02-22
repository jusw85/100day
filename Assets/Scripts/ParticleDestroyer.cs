using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleDestroyer : PoolObject {

    private ParticleSystem particleSys;

    private void Awake() {
        particleSys = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    private void Update() {
        if (particleSys.isPlaying)
            return;
        Destroy();
    }

    public override void OnObjectReuse() {
        ParticleSystem.ShapeModule shape = particleSys.shape;
        shape.angle = Random.Range(10, 50);
    }
}
