using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskStopFireWeapon : BTNode {
    private readonly InputEvent StopFireEvent;

    public TaskStopFireWeapon(InputEvent stopFireEvent) {
        this.StopFireEvent = stopFireEvent;
    }


    public override BTNodeState Evaluate() {
        if (StopFireEvent != null) {
            StopFireEvent();
            return BTNodeState.Success;
        } else {
            return BTNodeState.Failure;
        }
    }
}
