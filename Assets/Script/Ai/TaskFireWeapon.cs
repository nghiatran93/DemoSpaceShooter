using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskFireWeapon : BTNode {
    private readonly InputEvent FireEvent;

    public TaskFireWeapon(InputEvent fireEvent) {
        this.FireEvent = fireEvent;
    }

    public override BTNodeState Evaluate() {
        if (FireEvent != null) {
            FireEvent();
            return BTNodeState.Success;
        } else {
            return BTNodeState.Failure;
        }
    }
}
