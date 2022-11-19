using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum EnemyType
{
    Boommer,
    Normal
}

public class EnemyController : MonoBehaviour {

    private SphereCollider detectionArea;
    private Transform player;
    private Rigidbody rb;
    bool isBoom;
    bool isNormal;
    PlayerStats playerStats;
    public MilestoneSystem milestone;

    [SerializeField] private float topSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float ignoreDistanceSq;
    [SerializeField] EnemyType enemyType;
    [SerializeField] int attack;
    [SerializeField] int health;

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

        switch (enemyType)
        {
            case EnemyType.Boommer:
                EnemyBoom();
                break;
            case EnemyType.Normal:
                StartCoroutine(NormalEnemy());
                break;
            default:
                break;
        }
    }

    IEnumerator NormalEnemy()
    {
        if (isNormal)
        {
            isNormal = false;
            playerStats.TakeDamage(attack);
            yield return new WaitForSeconds(2f);
            isNormal = true;
        }
    }

    public void TakeDamage(int damage) {
        health -= damage;
        if (health <= 0) {
            Die();
        }
    }

    void EnemyBoom()
    {
        if (isBoom)
        {
            isBoom = false;
            playerStats.TakeDamage(attack * 2);
            Die();
        }
    }

    private void Die() {
        MilestoneSystem.increaseProgress();
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Player")) {
            var sp = collision.gameObject.GetComponent<Rigidbody>().velocity;
            rb.AddForce(sp * 2, ForceMode.Impulse);
            isBoom = true;
            isNormal = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isNormal = false;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            player = other.transform;
            playerStats = other.GetComponent<PlayerStats>();
        }
    }
}