using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskRotateToTarget2 : BTNode {
    private readonly IBehaviourAI myAI;
    private readonly InputEventVector3 RotateEvent;

    public TaskRotateToTarget2(IBehaviourAI behaviourAI, InputEventVector3 turnEvent) {
        this.myAI = behaviourAI;
        this.RotateEvent = turnEvent;
    }
    public override BTNodeState Evaluate() {
        Vector3 agentPosition = myAI.MyBoatTF.position;
        Vector3 targetPosition = myAI.TargetPosition;
        Vector3 desireHeading = targetPosition - agentPosition;

        if (Vector3.Angle(agentPosition, desireHeading) > 10) {

            RotateEvent(desireHeading);
        }
        return BTNodeState.Success;
    }

}
