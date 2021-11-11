using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public delegate void InputEventFloat(float value);
public delegate void InputEventVector3(Vector3 value);
public delegate void InputEvent();

public interface IBoatInput {
    event InputEvent FireEvent;
    event InputEvent StopFireEvent;

    event InputEventFloat SlideEvent;

    event InputEventFloat ForwardEvent;
    event InputEventFloat SideStrafeEvent;
    event InputEventFloat VerticalStrafeEvent;

    event InputEventFloat YawEvent;
    event InputEventFloat PitchEvent;
    event InputEventFloat RollEvent;

    event InputEventVector3 RotateEvent;
    IBoatInput SetUp(BoatInfo m_Info);

}
