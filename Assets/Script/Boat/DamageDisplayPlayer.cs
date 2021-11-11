using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageDisplayPlayer : MonoBehaviour, IDamageDisplay
{

    public Image hullBar;
    public Image shieldBar;

    public void SelfDestruct(GameObject explosionVFX) {
     
    }

    public void SetHullPercent(float percentage) {
        hullBar.fillAmount = percentage;
    }

    public void SetShieldPercent(float percentage) {
        shieldBar.fillAmount = percentage;
    }

 
}
