using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour {

    private IBoatInput playerInput;
    // [SerializeField] public GameObject pilot;

    public float maxSpeed = 150f;
    public float forwardThrustPower = 1500f;
    public float sideThrustPower = 1000F;
    public float verticalThrustPower = 1000F;

    public float yawSpeed = 36f;
    public float pitchSpeed = 36f;
    public float rollSpeed = 36f;

    private float normalDrag;
    private Rigidbody myRigidbody;
    private IWeapon[] weaponList;

    private void Awake() {
        myRigidbody = GetComponent<Rigidbody>();
        weaponList = GetComponentsInChildren<IWeapon>();
        normalDrag = myRigidbody.drag;
    }

    public BoatController SetUp(IBoatInput controllerInput) {
        if (controllerInput != null) {
            playerInput = controllerInput;
            playerInput.SlideEvent += EnableSlide;
            playerInput.ForwardEvent += ForwardThrust;
            playerInput.SideStrafeEvent += SideStrafe;
            playerInput.VerticalStrafeEvent += VerticalStrafe;

            playerInput.YawEvent += YawTurn;
            playerInput.PitchEvent += PitchTurn;
            playerInput.RollEvent += RollTurn;
            playerInput.RotateEvent += RotateToTarget;

            playerInput.FireEvent += FireWeapon;
            playerInput.StopFireEvent += StopFireWeapon;
        } else {
            Debug.LogError("no pilot on", gameObject);
        }
        return this;
    }

    public void RotateToTarget(Vector3 headingPosition) {
        Quaternion desireRotation = Quaternion.LookRotation(headingPosition);
        float step = yawSpeed * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, desireRotation, step);
    }

    public void FireWeapon() {
        if (weaponList.Length > 0) {
            foreach (IWeapon weapon in weaponList) {
                weapon.Fire(myRigidbody.velocity);
            }
        }
    }

    public void StopFireWeapon() {
        if (weaponList.Length > 0) {
            foreach (IWeapon weapon in weaponList) {
                weapon.StopFire();
            }
        }
    }

    public void EnableSlide(float slide) {
        if (slide > 0) {
            myRigidbody.drag = 0;
        } else {
            myRigidbody.drag = normalDrag;
        }
    }

    public void ForwardThrust(float thrust) {
        myRigidbody.AddForce(gameObject.transform.forward * thrust * forwardThrustPower * Time.deltaTime);
        if (myRigidbody.velocity.magnitude > maxSpeed) {
            myRigidbody.velocity = myRigidbody.velocity.normalized * maxSpeed;
        }
    }
    public void SideStrafe(float value) {
        myRigidbody.AddForce(gameObject.transform.right * value * sideThrustPower * Time.deltaTime);
    }
    public void VerticalStrafe(float value) {
        myRigidbody.AddForce(gameObject.transform.up * value * verticalThrustPower * Time.deltaTime);
    }

    private void RollTurn(float roll) {
        myRigidbody.AddTorque(-gameObject.transform.forward * roll * rollSpeed * Time.deltaTime);
    }

    private void PitchTurn(float pitch) {
        myRigidbody.AddTorque(-gameObject.transform.right * pitch * pitchSpeed * Time.deltaTime);
    }

    private void YawTurn(float yaw) {
        myRigidbody.AddTorque(gameObject.transform.up * yaw * yawSpeed * Time.deltaTime);
    }
}
