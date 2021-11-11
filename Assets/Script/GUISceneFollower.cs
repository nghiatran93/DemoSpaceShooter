using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUISceneFollower : MonoBehaviour
{
    public Transform FollowThis;

    void Update() {
        if (FollowThis == null) {
            return;
        }

        Vector2 sp = Camera.main.WorldToScreenPoint(FollowThis.position);

        this.transform.position = sp;
    }

    void OnEnable() {
        // this is here because there can be a single frame where the position is incorrect
        // when the object (or its parent) is activated.
        if (gameObject.activeInHierarchy) {
            Update();
        }
    }
}
