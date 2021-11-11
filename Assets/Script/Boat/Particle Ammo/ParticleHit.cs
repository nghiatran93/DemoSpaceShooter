using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHit : MonoBehaviour {
    [Range(1, 1000)]
    public int damage = 50;



    private void OnParticleCollision(GameObject other) {

   //   print(transform.root.name + " - " + transform.root.gameObject.GetInstanceID() + " HIT "+ other.GetInstanceID());

        BoatHp hullPointManager = other.GetComponent<BoatHp>();
        if(hullPointManager != null) {
            hullPointManager.TakeDamage(damage);
        }
    }
}
