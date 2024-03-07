using UnityEngine;

public class PlayerIdleState : BaseState {
    public PlayerIdleState(string name, PlayerStateMachine stateMachine) : base(name, stateMachine) {
    }

    public override void EnterState() {
    }

    public override void ExitState() {
    }

    public override void UpdateState() {
        if (PSM._movement != Vector3.zero) {
            PSM.SwitchState("Move");
        }
        else if (PSM._Jump) {
            PSM.SwitchState("Jump");
        }
    }
}