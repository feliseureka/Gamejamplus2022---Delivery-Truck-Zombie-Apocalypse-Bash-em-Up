using UnityEngine;

public class PlayerMove : MonoBehaviour {

    [SerializeField] private float topSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float deceleration;
    [SerializeField] private float angularAcceleration;
    [SerializeField] private float angularCounterAcceleration;

    private bool isDragging = false;
    private bool isSpeedUp = false;

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
            //Ngegas
            if (rb.velocity.magnitude < topSpeed) {
                rb.AddForce(currentAcceleration * yt * transform.forward);
                isSpeedUp = true;
            }
            //Set vel mbelok
            if (x != 0f) {
                angularVelocity += f * x * yt * angularAcceleration;
                isDragging = true;
            }else{
                isDragging = false;
            }
        }else{
            if(rb.velocity == new Vector3(0,0,0)){
                AudioSystem.Instance.StopEngine();
            }
        }
        int au = AudioSystem.Instance.retSFXPlaying();
        //Nggk mbelok
        if (x == 0f) {
            angularVelocity /= 8;
            if(y != 0f){
                AudioSystem.Instance.PlayEngine(Random.Range(0, AudioSystem.Instance.EngineLoop.Length));
            }
        }
        angularVelocity = Mathf.Clamp(angularVelocity, -60f, 60f);
        //mbeloknya di sini
        if (rb.velocity.sqrMagnitude > 0.01f) {
            rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, angularVelocity * Time.fixedDeltaTime, 0f));
            if(y!=0f && isDragging){
                AudioSystem.Instance.PlayDrift(Random.Range(0, AudioSystem.Instance.DriftArray.Length));
            }else{
                AudioSystem.Instance.StopDrift();
            }
        }
    }
}
