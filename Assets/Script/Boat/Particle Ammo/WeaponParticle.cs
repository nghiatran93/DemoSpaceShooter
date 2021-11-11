
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParticle : MonoBehaviour, IWeapon {


    ParticleSystem.EmissionModule emission;
    public ParticleSystem bulletParticleSystem;

    void Awake() {
        emission = GetComponentInChildren<ParticleSystem>().emission;
    }

    public void Fire(Vector3 parentVelocity) {
        emission.enabled = true;
    }
    public void StopFire() {
        emission.enabled = false;
    }
}


