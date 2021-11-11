using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBehaviourAI {

  //  event InputEventFloat RollEvent;
    Transform MyBoatTF { get; }
    Vector3 TargetPosition { get; set; }
    BoatInfo TargetBoat { get; set; }


    bool GetAvoidingFlag();
    void SetTempPosition(Vector3 position);
    void ReturnToSaveTarget();
}
