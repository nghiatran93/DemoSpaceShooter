using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Autocannon : MonoBehaviour, IWeapon {

    public float shotPerSec = 2;
    public GameObject projectivePrefab;
    public WeaponBarrel weaponBarrel;
    private float cooldownTimer = 0;
    private readonly float muzzleVelocity = 300f;

    // Start is called before the first frame update
    void Start() {
        weaponBarrel = transform.GetComponentInChildren<WeaponBarrel>();
    }

    public void Fire(Vector3 parentVelocity) {
        float cooldownRate = 1 / shotPerSec;
        if (cooldownTimer <= Time.time) {
            cooldownTimer = Time.time + cooldownRate;
            GameObject newProjective = Instantiate(projectivePrefab, weaponBarrel.transform.position, weaponBarrel.transform.rotation) as GameObject;
            Rigidbody projectiveRigid = newProjective.GetComponent<Rigidbody>();
            projectiveRigid.velocity = parentVelocity + newProjective.transform.forward * muzzleVelocity;

            Destroy(newProjective, 2f);
        }
    }
    public void StopFire() {
    }
}