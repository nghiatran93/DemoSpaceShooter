using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskObstacleAvoid : BTNode {
    IBehaviourAI myAI;
    Transform agentTransform;
    float avoidDistance;
    LayerMask avoidLayerMask;
    InputEventVector3 RotateEvent;

    public TaskObstacleAvoid(IBehaviourAI behaviourAI, float avoidDistance, InputEventVector3 rotateEvent) {
        this.myAI = behaviourAI;
        this.avoidDistance = avoidDistance;
        this.RotateEvent = rotateEvent;
    }
    public override BTNodeState Evaluate() {
        agentTransform = myAI.MyBoatTF;
        Vector3[] rayDirection = { agentTransform.forward, (agentTransform.forward + agentTransform.right).normalized, (agentTransform.forward - agentTransform.right).normalized };
        DrawRays(rayDirection);
        RaycastHit hit;
        if (Physics.Raycast(agentTransform.position, rayDirection[0], out hit, avoidDistance, avoidLayerMask)) {
           for(int i = 1; i < rayDirection.Length; i++) {
                bool goodTurn = CheckTurn(rayDirection[i]);
                if (goodTurn) {
                    return BTNodeState.Success;
                }
            }

        }
        RotateEvent(agentTransform.position - myAI.TargetPosition);
        return BTNodeState.Success;
    }

    private bool CheckTurn(Vector3 rayDirection) {
        RaycastHit hit;
        if (Physics.Raycast(agentTransform.position, rayDirection * (avoidDistance / 4), out hit, avoidDistance, avoidLayerMask)) {
            Vector3 newHeading = rayDirection;
            Vector3 newTarget = agentTransform.position + (newHeading * avoidDistance / 4);
            myAI.SetTempPosition(newTarget);
            if(RotateEvent != null) {
                RotateEvent(newHeading);
                return true;
            }
        }
        return false;
    }

    private void DrawRays(Vector3[] rays) {
        foreach (Vector3 ray in rays) {
            Debug.DrawRay(agentTransform.position, ray * avoidDistance, Color.blue);
        }
    }
}
