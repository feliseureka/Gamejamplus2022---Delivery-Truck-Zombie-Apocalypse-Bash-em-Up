using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour {

    private int level;

    [SerializeField] private int damage;
    [SerializeField] private float range, knockback;
    [SerializeField] private float delay;
    [SerializeField] private float spread;
    [SerializeField] private float burstCount;

    public void Upgrade() {
        StopAllCoroutines();
        level++;
        StartCoroutine(TimedShot(delay));
        if (level < 2) { return; }
        damage++;
        delay -= 0.05f;
    }

    IEnumerator TimedShot(float wait) {
        while (true) {
            for (int i = 0; i < burstCount; i++) {
                var dir = Quaternion.Euler(0, Random.value * spread, 0) * transform.forward;
                if (Physics.Raycast(transform.position, dir, out var hit, range)) {
                    var target = hit.transform.GetComponent<Rigidbody>();
                    target.AddForce(transform.forward * knockback, ForceMode.Impulse);
                }
            }
            yield return new WaitForSeconds(wait);
        }
    }

}
