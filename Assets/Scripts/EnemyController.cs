using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    private SphereCollider detectionArea;
    private Transform player;
    private Rigidbody rb;

    [SerializeField] private float speed;


    private void Awake() {
        detectionArea = GetComponent<SphereCollider>();
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        if (!player || Time.frameCount % 16 == 0) { return; }
        rb.velocity = speed * (player.position - transform.position);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            player = other.transform;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            player = null;
        }
    }
}