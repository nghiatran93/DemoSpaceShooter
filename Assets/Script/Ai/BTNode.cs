using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public abstract class BTNode
{
    protected BTNodeState currentState;

    public BTNodeState CurrentState {
        get { return currentState; }
       // private set { currentState = value; }
    }

    public BTNode() { }

    public abstract BTNodeState Evaluate();

}
