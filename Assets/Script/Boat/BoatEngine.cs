using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatEngine : MonoBehaviour {

    [Tooltip("X: Lateral thrust\nY: Vertical thrust\nZ: Longitudinal Thrust")]
    public Vector3 linearForce = new Vector3(5, 5, 20);

    [Tooltip("X: Pitch\nY: Yaw\nZ: Roll")]
    public Vector3 angularForce = new Vector3(3, 3, 1);

    [Range(0, 1)]
    [Tooltip("Multiplier for longitudinal thrust when reverse thrust is requested.")]
    public float reverseMultiplier = 1;

    [Tooltip("Multiplier for all forces. Can be used to keep force numbers smaller and more readable.")]
    public float forceMultiplier = 10;

    public Rigidbody Rigidbody { get { return rbody; } }

    private Vector3 appliedLinearForce = Vector3.zero;
    private Vector3 appliedAngularForce = Vector3.zero;

    private Rigidbody rbody;
    private IWeapon[] weaponList;

    public static BoatEngine playerBoat;
    public Vector3 Velocity { get { return rbody.velocity; } }

    private void Awake() {
        rbody = GetComponent<Rigidbody>();
        weaponList = GetComponentsInChildren<IWeapon>();
    }

    public BoatEngine SetUp(IBoatInput2 boatInput, bool isPlayer) {
        boatInput.MoveEvent += MoveBoat;
        boatInput.TurnEvent += TurnBoat;
        boatInput.RotateToEvent += RotateToTarget;

        boatInput.FireEvent += FireWeapon;
        boatInput.StopFireEvent += StopFireWeapon;
        if (isPlayer) {
            playerBoat = this;
        }
        return this;
    }

    public void MoveBoat(Vector3 moveInput) {
        appliedLinearForce = Vector3.Scale(moveInput, linearForce) * forceMultiplier;
        rbody.AddRelativeForce(appliedLinearForce, ForceMode.Acceleration);

    }

    public void TurnBoat(Vector3 turnInput) {
        appliedAngularForce = Vector3.Scale(turnInput, angularForce) * forceMultiplier;
        rbody.AddRelativeTorque(appliedAngularForce, ForceMode.Acceleration);
    }

    public void RotateToTarget(Vector3 headingPosition) {
        Quaternion desireRotation = Quaternion.LookRotation(headingPosition);
        float step = angularForce.x * forceMultiplier * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, desireRotation, step);
    }
    public void FireWeapon() {
        if (weaponList.Length > 0) {
            foreach (IWeapon weapon in weaponList) {
                weapon.Fire(rbody.velocity);
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
}
