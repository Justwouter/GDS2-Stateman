using UnityEngine;

public class PlayerIdleState : BaseState {
    public PlayerIdleState(string name, PlayerStateMachine stateMachine) : base(name, stateMachine) {
    }

    public override void EnterState() {
        EventBus.OnJump += Jump;
        EventBus.OnMove += Move;
    }

    public override void UpdateState() {

    }

    public override void ExitState() {
        EventBus.OnJump -= Jump;
        EventBus.OnMove -= Move;
    }

    private void Move(Vector3 movement) {
        if (movement != Vector3.zero) {
            PSM.SwitchState("Move");
        }
    }
    


}