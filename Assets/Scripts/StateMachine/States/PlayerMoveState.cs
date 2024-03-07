using UnityEngine;

public class PlayerMovementState : BaseState {
    public PlayerMovementState(string name, PlayerStateMachine stateMachine) : base(name, stateMachine) {
    }

    public override void EnterState() {
    }

    public override void UpdateState() {
        PSM.transform.Translate(PSM.speedMult * Time.deltaTime * PSM._movement);

        
        if (PSM._Jump) {
            PSM.SwitchState("Jump");
        }
        else if(PSM._Sprint){
            PSM.SwitchState("Sprint");
        }
        else if (PSM._movement == Vector3.zero) {
            PSM.SwitchState("Idle");
        }
    }

    public override void ExitState() {
    }


}