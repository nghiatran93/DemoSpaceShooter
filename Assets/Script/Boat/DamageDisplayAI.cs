using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DamageDisplayAI : MonoBehaviour, IDamageDisplay {
    private Image hullBar;
    private Image shieldBar;
    private Transform parentTransform;
    private Transform camTF;

    public void SelfDestruct(GameObject explosionVFX) {
        if(explosionVFX != null) {
            GameObject explosionVFXCloned = Instantiate(explosionVFX, transform.position, Quaternion.identity);
            Destroy(explosionVFXCloned, 1f);
        }
        Destroy(gameObject, 0.1f);
    }

    public void SetHullPercent(float percentage) {
        hullBar.fillAmount = percentage;
    }

    public void SetShieldPercent(float percentage) {
        shieldBar.fillAmount = percentage;
    }

    void Start() {
        parentTransform = transform.root;
        foreach (Image image in GetComponentsInChildren<Image>()) {
            if (image.name.Equals("HullBar")) {
                hullBar = image;
            }
            if (image.name.Equals("ShieldBar")) {
                shieldBar = image;
            }
        }
        camTF = Camera.main.transform;
    }

    // Update is called once per frame
    void Update() {
        transform.LookAt(parentTransform.position + camTF.forward);
        transform.rotation = camTF.rotation;
    }
}
