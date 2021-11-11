using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskMoveToTarget : BTNode {

    private readonly IBehaviourAI myAI;
    private readonly float range;

    private readonly InputEventFloat ForwardEvent;

    public TaskMoveToTarget(IBehaviourAI behaviourAI, float range, InputEventFloat forwardEvent) {
        this.myAI = behaviourAI;
        this.range = range;
        this.ForwardEvent = forwardEvent;
    }
    public override BTNodeState Evaluate() {
        Vector3 agentPosition = myAI.MyBoatTF.position;

        float distance = Vector3.Distance(agentPosition, myAI.TargetPosition);
        float thrust = distance / range;
        ForwardEvent(thrust);
        return BTNodeState.Success;
    }


}
