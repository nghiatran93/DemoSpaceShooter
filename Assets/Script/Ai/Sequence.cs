using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : BTNode {

    List<BTNode> myNodes = new List<BTNode>();

    public Sequence(List<BTNode> nodes) {
        myNodes = nodes;
    }

    public override BTNodeState Evaluate() {
        bool childRunning = false;

        foreach(BTNode node in myNodes) {
            switch (node.Evaluate()){
                case BTNodeState.Failure:
                    currentState = BTNodeState.Failure;
                    return currentState;
                case BTNodeState.Success:
                    continue;
                case BTNodeState.Running:
                    childRunning = true;
                    continue;
                default:
                    currentState = BTNodeState.Success;
                    return currentState;
            }
        }
        currentState = childRunning ? BTNodeState.Running : BTNodeState.Success;
        return currentState;
    }
}
