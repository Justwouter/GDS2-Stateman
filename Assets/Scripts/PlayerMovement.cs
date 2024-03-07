using System.Collections;

using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour {
    [Header("Settings")]
    [SerializeField] private float speedMult = 10;
    [SerializeField] private float jumpHight = 10;
    [SerializeField] private float rotationSpeed = 100;
    [SerializeField] private LayerMask groundLayer;


    private Rigidbody _rb;
    private Vector3 _movement = Vector3.zero;
    private float _rotationDirection;
    private bool _canJump = true;
    private bool _JumpCooldown = false;


    void Start() {
        _rb = GetComponent<Rigidbody>();
    }

    void Update() {
        transform.Translate(speedMult * Time.deltaTime * _movement);
        transform.Rotate(_rotationDirection * rotationSpeed * Time.deltaTime * Vector3.up);
        GroundCheck();
        if (transform.position.y < -10) {
            transform.position = Vector3.up * 2;
        }
    }

    private void OnMove(InputValue value) {
        _movement = value.Get<Vector3>();
    }

    private void OnRotate(InputValue inputValue) {
        _rotationDirection = inputValue.Get<Vector2>().x;
    }
    private void OnJump(InputValue value) {
        if (_canJump && !_JumpCooldown) {
            _rb.AddForce(Vector3.up * jumpHight, ForceMode.Impulse);
            _canJump = false;
            StartCoroutine(JumpCooldown());
        }
    }


    IEnumerator JumpCooldown() {
        _JumpCooldown = true;
        yield return new WaitForSecondsRealtime(0.5f);
        _JumpCooldown = false;
    }

    private void GroundCheck() {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1, groundLayer)) {
            Debug.DrawRay(transform.position, Vector3.down * hit.distance, Color.yellow);
            if (!_JumpCooldown) {
                _canJump = true;
            }
        }
        else {
            Debug.DrawRay(transform.position, Vector3.down * 1, Color.white);
        }
    }
}
