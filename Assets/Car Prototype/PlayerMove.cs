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

    public void ChangeStat(int topSpeed) {
        this.topSpeed = topSpeed;
    }

    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }
    private void Update() {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
    }

    private void FixedUpdate() {
        currentAcceleration = y < 0f ? deceleration : acceleration;
        var yt = Time.fixedDeltaTime * y;
        var f = yt * angularVelocity < 0 ? angularCounterAcceleration : angularAcceleration;
        if (y != 0f) {
            rb.AddForce(currentAcceleration * yt * transform.forward);
            if (x != 0) {
                angularVelocity += f * x * yt * angularAcceleration;
            } else {
                angularVelocity = 0f;
            }
        }
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, topSpeed);
        angularVelocity = Mathf.Clamp(angularVelocity, -60f, 60f);
        if (rb.velocity.sqrMagnitude > 0) {
            rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, angularVelocity * Time.fixedDeltaTime, 0f));
        }
    }
}
