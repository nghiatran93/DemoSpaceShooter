using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageDisplay {
    void SetHullPercent(float percentage);
    void SetShieldPercent(float percentage);
    void SelfDestruct(GameObject explosionVFX);

}
