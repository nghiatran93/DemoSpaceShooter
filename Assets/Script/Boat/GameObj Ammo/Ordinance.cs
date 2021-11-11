using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ordinance : MonoBehaviour {
    public string ordinanceName = "rocket";
    public float armorDamage = 10f;
    public float shiledDamage = 20f;
    public GameObject explosionPrefab;


    private void OnCollisionEnter(Collision collision) {
        Explode();
    }

    private void Explode() {
        GameObject gameObject1 = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject1, 3f);
        Destroy(gameObject, 0.05f);
    }
}
