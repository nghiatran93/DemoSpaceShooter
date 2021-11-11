using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBoatInput2 {

    event InputEvent FireEvent;
    event InputEvent StopFireEvent;

    event InputEventVector3 MoveEvent;

    event InputEventVector3 TurnEvent;

    event InputEventVector3 RotateToEvent;
    IBoatInput2 SetUp(BoatInfo m_Info);
}
