using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskCheckArrival : BTNode {

    private readonly IBehaviourAI myAI;
    private readonly float range;

    public TaskCheckArrival(IBehaviourAI behaviourAI, float range) {
        this.myAI = behaviourAI;
        this.range = range;
    }
    public override BTNodeState Evaluate() {
        Vector3 agentPosition = myAI.MyBoatTF.position;
        Vector3 targetPosition = myAI.TargetPosition;
        float distance = Vector3.Distance(agentPosition, targetPosition);
        return distance < range ? BTNodeState.Success : BTNodeState.Failure;
    }
}
