using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatInputHuman : MonoBehaviour, IBoatInput {

    public event InputEventFloat SlideEvent;

    public event InputEventFloat ForwardEvent;
    public event InputEventFloat SideStrafeEvent;
    public event InputEventFloat VerticalStrafeEvent;

    public event InputEventFloat YawEvent;
    public event InputEventFloat PitchEvent;
    public event InputEventFloat RollEvent;
#pragma warning disable 67
    public event InputEventVector3 RotateEvent;
#pragma warning restore 67
    public event InputEvent FireEvent;
    public event InputEvent StopFireEvent;

    float mouseStallRadius = 5f;

    private BoatInfo m_Info;

    public IBoatInput SetUp(BoatInfo m_Info) {
        this.m_Info = m_Info;
        return this;
    }


    // Update is called once per frame
    void Update() {
        if (!m_Info.IsMovable()) {
            return;
        }

        GetKeyboardInput();

        GetMouseInput();
    }

    void GetKeyboardInput() {
        if (ForwardEvent != null) {
            float throttle;
            if ((throttle = Input.GetAxis("Push")) != 0) {
                ForwardEvent(throttle);
            }
        }
        if (SideStrafeEvent != null) {
            float moveLftRgt;
            if ((moveLftRgt = Input.GetAxis("MoveLR")) != 0) {
                SideStrafeEvent(moveLftRgt);
            }
        }
        if (PitchEvent != null) {
            float moveUpDown;
            if ((moveUpDown = Input.GetAxis("MoveUD")) != 0) {
                VerticalStrafeEvent(moveUpDown);
            }
        }
        if (RollEvent != null) {
            float roll;
            if ((roll = Input.GetAxis("Roll")) != 0) {
                RollEvent(roll);
            }
        }
        if (SlideEvent != null) {
            if (Input.GetButton("Slide")) {
                SlideEvent(1);
            }
            if (Input.GetButtonUp("Slide")) {
                SlideEvent(0);
            }
        }


    }

    void GetMouseInput() {
        Vector3 mousePosition = Input.mousePosition;
        if (FireEvent != null) {
            if (Input.GetMouseButton(0)) {
                FireEvent();
            } else {
                StopFireEvent?.Invoke();
            }
        }
        float yaw = (mousePosition.x - Screen.width * 0.5F) / (Screen.width * 0.5F);
        if (YawEvent != null && -mouseStallRadius <= yaw && yaw <= mouseStallRadius) {
            YawEvent(yaw);
        }

        float pitch = (mousePosition.y - Screen.height * 0.5F) / (Screen.height * 0.5F);
        if (PitchEvent != null && -mouseStallRadius <= pitch && pitch <= mouseStallRadius) {
            PitchEvent(pitch);
        }
    }


}
