using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskIsTargetVisible : BTNode {

    private readonly IBehaviourAI myAI;
    private readonly float range;

    public TaskIsTargetVisible(IBehaviourAI behaviourAI, float range) {
        this.myAI = behaviourAI;
        this.range = range;
    }
    public override BTNodeState Evaluate() {
        if (myAI.TargetBoat == null) { return BTNodeState.Failure; }
        if (Physics.Raycast(myAI.MyBoatTF.position, myAI.MyBoatTF.forward, out RaycastHit hit, range)) {
            BoatInfo aimedBoatInfo = hit.collider.transform.root.GetComponent<BoatInfo>();
            if (aimedBoatInfo == myAI.TargetBoat) {
                return BTNodeState.Success;
            }
        }
        return BTNodeState.Failure;
    }
}
