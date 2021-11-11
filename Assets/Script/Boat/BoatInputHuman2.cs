using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatInputHuman2 : MonoBehaviour, IBoatInput2 {

    public event InputEvent FireEvent;
    public event InputEvent StopFireEvent;
    public event InputEventVector3 MoveEvent;
    public event InputEventVector3 TurnEvent;
    public event InputEventVector3 RotateToEvent;

    private BoatInfo m_Info;

    private Vector3 moveInput = Vector3.zero, turnInput = Vector3.zero;

    public float Throttle {
        get {
            return moveInput.z;
        }
    }

    public static BoatInputHuman2 playerBoat;

    public IBoatInput2 SetUp(BoatInfo m_Info) {
        this.m_Info = m_Info;
        playerBoat = this;
        return this;
    }


    // Update is called once per frame
    void Update() {
        if (!m_Info.IsMovable()) {
            return;
        }

        GetKeyboardInput();

        GetMouseInput();

        MoveEvent(moveInput);
        TurnEvent(turnInput);
    }

    void GetKeyboardInput() {

        float push = Input.GetAxis("Push");
        float target;
        if (push > 0f) {
            target = 1f;
        } else if (push < 0f) {
            target = 0f;
        } else {
            target = moveInput.z;
        }
        moveInput.z = Mathf.MoveTowards(moveInput.z, target, Time.deltaTime * 0.5f);


        moveInput.x = Input.GetAxis("MoveLR");
        moveInput.y = Input.GetAxis("MoveUD");

        turnInput.z = Input.GetAxis("Roll");
    }

    void GetMouseInput() {

        if (FireEvent != null) {
            if (Input.GetMouseButton(0)) {
                FireEvent();
            } else {
                StopFireEvent?.Invoke();
            }
        }

        Vector3 mousePos = Input.mousePosition;

        // Figure out most position relative to center of screen.
        // (0, 0) is center, (-1, -1) is bottom left, (1, 1) is top right.      
        float pitch = (mousePos.y - (Screen.height * 0.5f)) / (Screen.height * 0.5f);
        float yaw = (mousePos.x - (Screen.width * 0.5f)) / (Screen.width * 0.5f);

        // Make sure the values don't exceed limits.
        turnInput.x = -Mathf.Clamp(pitch, -1.0f, 1.0f);
        turnInput.y = Mathf.Clamp(yaw, -1.0f, 1.0f);
    }
}

