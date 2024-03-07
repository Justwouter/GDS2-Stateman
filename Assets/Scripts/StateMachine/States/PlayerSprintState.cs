using UnityEngine;

public class PlayerSprintState : BaseState {
    public PlayerSprintState(string name, PlayerStateMachine stateMachine) : base(name, stateMachine) {
    }

    public override void EnterState() {
        PSM.PlayerCam.m_Lens.FieldOfView += 5;
    }

    public override void UpdateState() {
        PSM.transform.Translate((PSM.speedMult * 1.5f) * Time.deltaTime * PSM._movement);

        if (PSM._Jump) {
            PSM.SwitchState("Jump");
        }
        else if (!PSM._Sprint) {
            PSM.SwitchState("Move");
        }
        else if (PSM._movement == Vector3.zero) {
            PSM.SwitchState("Idle");
        }
    }

    public override void ExitState() {
        PSM._Sprint = false;
        PSM.PlayerCam.m_Lens.FieldOfView -= 5;
    }
}