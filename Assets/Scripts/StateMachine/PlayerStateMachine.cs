using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Cinemachine;

using TMPro;

using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody))]
public class PlayerStateMachine : MonoBehaviour {
    [Header("Refs")]
    [SerializeField] private TextMeshProUGUI _stateIndicator;
    public LayerMask groundLayer;
    public CinemachineVirtualCamera PlayerCam;
    [SerializeField] private GameObject _bullet;



    [Header("Settings")]
    public float speedMult = 10;
    public float jumpHight = 10;
    public float rotationSpeed = 100;


    [HideInInspector] public Vector3 _movement { get; private set; } = Vector3.zero;
    [HideInInspector] public float _rotationDirection;
    [HideInInspector] public bool JumpOnCooldown;

    [HideInInspector] private BaseState currentState;

    private readonly List<BaseState> _states = new();
    private void Awake() {
        EventBus.OnAttack += UseAttack;
    }

    private void Start() {
        _states.AddRange(new List<BaseState>() {
            new PlayerIdleState("Idle", this),
            new PlayerMovementState("Move", this),
            new PlayerSprintState("Sprint", this),
            new PlayerJumpState("Jump", this)
            });
        currentState = _states.FirstOrDefault(s => s.Name == "Idle"); // Set the starting state as idle
        currentState.EnterState();
    }

    private void OnDestroy() {
        EventBus.OnAttack -= UseAttack;
    }

    public void SwitchState(string name) {
        currentState.ExitState();
        currentState = _states.FirstOrDefault(s => s.Name == name);
        currentState.EnterState();
    }

    private void Update() {
        _stateIndicator.SetText(currentState.Name);
        transform.Rotate(_rotationDirection * rotationSpeed * Time.deltaTime * Vector3.up); // Player can always rotate
        currentState.UpdateState();
    }


    private void OnMove(InputValue value) {
        _movement = value.Get<Vector3>(); // Should be replaced but im lazy
        EventBus.Move(_movement);
    }

    private void OnRotate(InputValue inputValue) {
        _rotationDirection = inputValue.Get<Vector2>().x;
    }

    private void OnJump(InputValue value) {
        if (!JumpOnCooldown) {
            EventBus.Jump();
            StartCoroutine(JumpCooldown());
        }
    }

    private void OnSprint(InputValue value) {
        EventBus.Sprint();
    }

    private void OnAttack(InputValue value) {
        EventBus.Attack();
    }

    IEnumerator JumpCooldown() {
        JumpOnCooldown = true;
        yield return new WaitForSecondsRealtime(0.5f);
        JumpOnCooldown = false;
    }

    public void UseAttack() {
        GameObject projectile = Instantiate(_bullet, transform.position + 2 * transform.forward, transform.rotation);
        projectile.GetComponent<Rigidbody>().AddForce(transform.forward * 100, ForceMode.Impulse);
    }
}