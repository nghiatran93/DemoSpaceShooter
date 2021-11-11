using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoatHp : MonoBehaviour {

    private BoatInfo m_Info;

    public float shieldPoint = 1000;
    public float hullPoint = 2000;
    public GameObject explosionVFX;
    private float maxShieldPoint;
    private float maxHullPoint;
    private IDamageDisplay m_DamageDisplay;

    public BoatHp SetUp(IDamageDisplay damageDisplay) {
        m_Info = GetComponent<BoatInfo>();
        maxHullPoint = hullPoint;
        maxShieldPoint = shieldPoint;
        this.m_DamageDisplay = damageDisplay;
        return this;
    }

    public void TakeDamage(int damage) {
        shieldPoint -= damage;
        if (shieldPoint < 0f) {
            hullPoint += shieldPoint;
            shieldPoint = 0f;
        }
        m_DamageDisplay.SetHullPercent(hullPoint / maxHullPoint);
        m_DamageDisplay.SetShieldPercent(shieldPoint / maxShieldPoint);
        if (hullPoint <= 0f) {
            m_Info.status = BoatStatus.Destroyed;
            m_DamageDisplay.SelfDestruct(explosionVFX);
            Destroy(gameObject, 0.1f);
        }
    }
}
