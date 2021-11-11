using System.Collections;
using UnityEngine;


public class TaskRandomCondition : BTNode {

    private readonly int numberOfDice, numberOfFace, numberToWin;

    public TaskRandomCondition(int numberOfDice, int numberOfFace, int numberToWin) {
        this.numberOfDice = numberOfDice;
        this.numberOfFace = numberOfFace;
        this.numberToWin = numberToWin;
    }
    public override BTNodeState Evaluate() {
        int total = 0;
        for (int i = 0; i < numberOfDice; i++) {
            total += Random.Range(1, numberOfFace);
        }
        return total >= numberToWin ? BTNodeState.Success : BTNodeState.Failure;
    }
}
