using UnityEngine;

public class PlayerJumpState : BaseState {
    public PlayerJumpState(string name, PlayerStateMachine stateMachine) : base(name, stateMachine) {
    }

    public override void EnterState() {
        PSM.GetComponent<Rigidbody>().AddForce(Vector3.up * PSM.jumpHight, ForceMode.Impulse);
    }

    public override void UpdateState() {
        PSM.transform.Translate(PSM.speedMult * Time.deltaTime * PSM._movement);
        GroundCheck();
    }

    public override void ExitState() {
    }



    private void GroundCheck() {
        if (Physics.Raycast(PSM.transform.position, Vector3.down, out RaycastHit hit, 1, PSM.groundLayer)) {
            Debug.DrawRay(PSM.transform.position, Vector3.down * hit.distance, Color.yellow);
            PSM.SwitchState("Move");
        }
        else {
            Debug.DrawRay(PSM.transform.position, Vector3.down * 1, Color.white);
        }
    }
}