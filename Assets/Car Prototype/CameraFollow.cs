using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [SerializeField] private Transform target;
    [SerializeField] private Vector3 dist;

    private void FixedUpdate() {
        transform.position = Vector3.Lerp(transform.position, target.position + dist, 0.9f);
    }

}
