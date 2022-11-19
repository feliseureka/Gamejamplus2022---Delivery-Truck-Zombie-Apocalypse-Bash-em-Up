using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    [SerializeField] private float range, knockback;
    [SerializeField] private float delay;

    private void Start() {
        StartCoroutine(TimedShot(delay));
    }

    IEnumerator TimedShot(float wait) {
        while (true) {
            if (Physics.Raycast(transform.position, transform.forward, out var hit, range)) {
                var target = hit.transform.GetComponent<Rigidbody>();
                target.AddForce(transform.forward * knockback, ForceMode.Impulse);
            }
            yield return new WaitForSeconds(wait);
        }
    }

}
