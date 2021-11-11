using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatInfo : MonoBehaviour {

    public BoatStatus status = BoatStatus.Ready;
    public ClanEnum clan;


    public BoatInfo SetUp(ClanEnum myClan) {
        clan = myClan;
        return this;
    }

    public bool IsMovable() {
        return status == BoatStatus.Ready;
    }

    public bool IsTargetable() {
        return status != BoatStatus.Destroyed;
    }
}
