using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskFindWanderPoint : BTNode {

    private readonly IBehaviourAI myAI;
    private readonly float range;

    public TaskFindWanderPoint(IBehaviourAI behaviourAI, float range) {
        this.range = range;
        this.myAI = behaviourAI;
    }

    public override BTNodeState Evaluate() {
        myAI.TargetBoat = null;
        myAI.TargetPosition = myAI.MyBoatTF.position + (Random.insideUnitSphere * range);
        return BTNodeState.Success;
    }
}
