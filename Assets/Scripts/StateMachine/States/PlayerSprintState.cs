using UnityEngine;

public class PlayerSprintState : BaseState {
    public PlayerSprintState(string name, PlayerStateMachine stateMachine) : base(name, stateMachine) {
    }

    public override void EnterState() {
        PSM.PlayerCam.m_Lens.FieldOfView += 5;
        EventBus.OnJump += Jump;
        EventBus.OnMove += Move;
    }

    public override void UpdateState() {
        PSM.transform.Translate((PSM.speedMult * 1.5f) * Time.deltaTime * PSM._movement);
    }

    public override void ExitState() {
        PSM.PlayerCam.m_Lens.FieldOfView -= 5;
        EventBus.OnJump -= Jump;
        EventBus.OnMove -= Move;
    }
    
    private void Move(Vector3 movement) {
        if (PSM._movement == Vector3.zero) {
            PSM.SwitchState("Idle");
        }
    }
}