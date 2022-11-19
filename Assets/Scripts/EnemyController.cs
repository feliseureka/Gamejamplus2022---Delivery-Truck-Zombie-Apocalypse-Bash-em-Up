using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    private SphereCollider detectionArea;
    private Transform player;
    private Rigidbody rb;

    [SerializeField] private float topSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float ignoreDistanceSq;

    private void Awake() {
        detectionArea = GetComponent<SphereCollider>();
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        if (!player || Time.frameCount % 16 == 0) { return; }
        var dist = player.position - transform.position;
        rb.velocity = Vector3.Lerp(rb.velocity, topSpeed * dist.normalized, acceleration * Time.fixedDeltaTime);
        if (dist.sqrMagnitude > ignoreDistanceSq) {
            player = null;
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Player")) {
            var sp = collision.gameObject.GetComponent<Rigidbody>().velocity;
            rb.AddForce(sp * 2, ForceMode.Impulse);
        }


    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            player = other.transform;
        }
    }
}