using UnityEngine;

public class PlayerMovementState : BaseState {
    public PlayerMovementState(string name, PlayerStateMachine stateMachine) : base(name, stateMachine) {
    }

    public override void EnterState() {
        EventBus.OnSprint += Sprint;
        EventBus.OnJump += Jump;
        EventBus.OnMove += Move;
    }

    public override void UpdateState() {
        PSM.transform.Translate(PSM.speedMult * Time.deltaTime * PSM._movement);
    }

    public override void ExitState() {
        EventBus.OnSprint -= Sprint;
        EventBus.OnJump -= Jump;
        EventBus.OnMove -= Move;
    }

    private void Move(Vector3 movement) {
        if (movement == Vector3.zero) {
            PSM.SwitchState("Idle");
        }
    }
}