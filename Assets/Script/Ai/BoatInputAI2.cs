using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatInputAI2 : MonoBehaviour, IBoatInput2, IBehaviourAI {

    public event InputEvent FireEvent;
    public event InputEvent StopFireEvent;
    public event InputEventVector3 MoveEvent;
    public event InputEventVector3 TurnEvent;
    public event InputEventVector3 RotateToEvent;
    public Selector rootAI;


    public Vector3 targetPosition = Vector3.zero;
    public BoatInfo targetBoat = null;
    private bool isAvoiding = false;
    public Vector3 tempTarget = Vector3.zero;

    private BoatInfo m_Info;
    private Transform agentTF;
    public Transform MyBoatTF { get => agentTF; }
    public Vector3 TargetPosition {
        get {
            if (targetBoat == null) {
                return targetPosition;
            } else if (!targetBoat.IsTargetable()) {
                targetBoat = null;
                return targetPosition;
            } else {
                return targetBoat.transform.position;
            }
        }
        set {
            targetPosition = value;
        }
    }
    public BoatInfo TargetBoat { get { return targetBoat; } set => targetBoat = value; }

    public bool GetAvoidingFlag() {
        return isAvoiding;
    }
    public void SetTempPosition(Vector3 position) {
        isAvoiding = true;
        tempTarget = position;
    }
    public void ReturnToSaveTarget() {
        isAvoiding = false;
    }

    public IBoatInput2 SetUp(BoatInfo m_Info) {
        this.m_Info = m_Info;
        agentTF = m_Info.transform;
        return this;
    }

    // Start is called before the first frame update
    void Start() {
        Sequence decideToAttack = new Sequence(new List<BTNode> { new TaskRandomCondition(1, 10, 5), new TaskFindNewTarget(this, m_Info.clan) });
        Selector selectTargetType = new Selector(new List<BTNode> { decideToAttack, new TaskFindWanderPoint(this, 200) });
        Sequence checkArrivalSequence = new Sequence(new List<BTNode> { new TaskCheckArrival(this, 100), selectTargetType });

        Selector fireOrStop = new Selector(new List<BTNode> {
            new Sequence(new List<BTNode> {new TaskIsTargetVisible(this, 1000), new TaskFireWeapon(FireEvent)}),
            new TaskStopFireWeapon(StopFireEvent)
           });

        Sequence moveSequence = new Sequence(new List<BTNode> {
            new TaskRotateToTarget(this, RotateToEvent),
            new TaskMoveToTarget2(this, 200, MoveEvent),
            fireOrStop
            });
        rootAI = new Selector(new List<BTNode> { checkArrivalSequence, moveSequence });


        new TaskFindNewTarget(this, m_Info.clan).Evaluate();
        StopFireEvent();
    }

    // Update is called once per frame
    void Update() {
        if (!m_Info.IsMovable()) {
            return;
        }
        rootAI.Evaluate();
    }
}
