public abstract class BaseState {
    public string Name;
    protected PlayerStateMachine PSM;

    public BaseState(string name, PlayerStateMachine stateMachine) {
        Name = name;
        PSM = stateMachine;
    }

    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();

    protected void Jump() {
        PSM.SwitchState("Jump");
    }

    protected void Sprint() {
        PSM.SwitchState("Sprint");

    }

}