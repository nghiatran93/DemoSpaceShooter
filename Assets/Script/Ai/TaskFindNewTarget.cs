using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TaskFindNewTarget : BTNode {
    private readonly IBehaviourAI myAI;
    private readonly ClanEnum myFactionEnum;

    public TaskFindNewTarget(IBehaviourAI behaviourAI, ClanEnum myFactionEnum) {
        this.myAI = behaviourAI;
        this.myFactionEnum = myFactionEnum;
    }

    public override BTNodeState Evaluate() {
        /*GameObject[] targets = GameObject.FindGameObjectsWithTag(enemyFaction);
        if (targets.Length > 0) {
            int ramdomChoice = UnityEngine.Random.Range(0, targets.Length);
            myAI.TargetBoat = targets[ramdomChoice];
            return BTNodeState.Success;
        } else {
            return BTNodeState.Failure;
        }*/
        BoatInfo target = BattleScript.FindEnemy(myFactionEnum);
        if (target != null) {
            myAI.TargetBoat = target;
            return BTNodeState.Success;
        } else {
            return BTNodeState.Failure;
        }
    }
}
