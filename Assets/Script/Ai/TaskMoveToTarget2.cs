using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskMoveToTarget2 : BTNode {

    private readonly IBehaviourAI myAI;
    private readonly float range;

    private readonly InputEventVector3 MoveEvent;

    public TaskMoveToTarget2(IBehaviourAI behaviourAI, float range, InputEventVector3 moveEvent) {
        this.myAI = behaviourAI;
        this.range = range;
        this.MoveEvent = moveEvent;
    }
    public override BTNodeState Evaluate() {
        Vector3 agentPosition = myAI.MyBoatTF.position;

        float distance = Vector3.Distance(agentPosition, myAI.TargetPosition);
        float thrust = Mathf.Clamp(distance / range, 0.5f, 1f);
        MoveEvent(new Vector3(0, 0, thrust));
        return BTNodeState.Success;
    }


}
