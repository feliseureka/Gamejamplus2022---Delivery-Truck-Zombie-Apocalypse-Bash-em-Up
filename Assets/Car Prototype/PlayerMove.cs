using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    [SerializeField] private float topSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float deceleration;
    [SerializeField] private float angularAcceleration;
    [SerializeField] private float angularCounterAcceleration;

    private float currentAcceleration;
    private float angularVelocity;

    private Rigidbody rb;

    private float x, y;

    bool isAttack;

    public void ChangeStat(int topSpeed) {
        this.topSpeed = topSpeed;
    }

    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }
    private void Update() {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate() {
        currentAcceleration = y < 0f ? deceleration : acceleration;
        var yt = Time.fixedDeltaTime * y;
        var f = yt * angularVelocity < 0 ? angularCounterAcceleration : angularAcceleration;
        if (y != 0f) {
            if (rb.velocity.magnitude < topSpeed) {
                rb.AddForce(currentAcceleration * yt * transform.forward);
            }
            if (x != 0f) {
                angularVelocity += f * x * yt * angularAcceleration;
            }
        }
        if (x == 0f) {
            angularVelocity /= 8;
        }
        angularVelocity = Mathf.Clamp(angularVelocity, -60f, 60f);
        if (rb.velocity.sqrMagnitude > 0.01f) {
            rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, angularVelocity * Time.fixedDeltaTime, 0f));
        }
    }
}
