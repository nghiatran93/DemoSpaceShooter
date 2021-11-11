using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    public class Selector : BTNode {

    protected List<BTNode> myNodes = new List<BTNode>();

    public Selector(List<BTNode> nodes) {
        myNodes = nodes;
    }
    public override BTNodeState Evaluate() {
     
        foreach (BTNode node in myNodes) {
            switch (node.Evaluate()) {
                case BTNodeState.Failure:
                    continue;
                case BTNodeState.Success:
                    currentState = BTNodeState.Success;
                    return currentState;
         
                default:
                    continue;
            }
        }
        currentState =  BTNodeState.Failure;
        return currentState;
    }
}


    
