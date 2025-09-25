using System;
using Unity.VisualScripting;
using UnityEngine;

public class CharStateCtrl : BaseChar
{
    [Header("CharStateCtrl")]

    [SerializeField] protected StatusState statusState;
    public StatusState StatusState => statusState;

    [SerializeField] protected HorizontalState horizontalState;
    public HorizontalState HorizontalState => horizontalState;

    [SerializeField] protected VerticalState verticalState;
    public VerticalState VerticalState => verticalState;

    [SerializeField] protected SkillState skillState;
    public SkillState SkillState => skillState;

    [SerializeField] protected SkillLock skillLock;
    public SkillLock SkillLock => skillLock;

    public InputBuffer InputBuffer { get; private set; } //Mới chỉ hoạt động cho attack và dash trong SkillState

    protected override void Start()
    {
        base.Start();
        this.Init();
    }

    private void Update()
    {
        if (InputManager.Instance.DashInput)
            InputBuffer.AddInput(StateName.DASH_STATE);

        if (InputManager.Instance.AttackInput)
            InputBuffer.AddInput(StateName.ATTACK_ONE_STATE);
    }

    protected virtual void Init()
    {
        this.charCtrl.CharStateCtrl.HorizontalState.ChangeState(this.charCtrl.CharStateCtrl.HorizontalState.idleX);
        this.charCtrl.CharStateCtrl.VerticalState.ChangeState(this.charCtrl.CharStateCtrl.VerticalState.idleGround);
        this.charCtrl.CharStateCtrl.SkillState.ChangeState(this.charCtrl.CharStateCtrl.SkillState.idleSkill);
        InputBuffer = new InputBuffer(this.charCtrl.CharData.BufferWindow);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadStatusState();
        this.LoadHorizontalState();
        this.LoadVerticalState();
        this.LoadSkillState();
        this.LoadSkillLock();
    }

    protected virtual void LoadStatusState()
    {
        if (statusState != null) return;
        this.statusState = GetComponent<StatusState>();
        Debug.Log(transform.name + ": LoadStatusState", gameObject);
    }

    protected virtual void LoadHorizontalState()
    {
        if (horizontalState != null) return;
        this.horizontalState = GetComponent<HorizontalState>();
        Debug.Log(transform.name + ": LoadHorizontalState", gameObject);
    }

    protected virtual void LoadVerticalState()
    {
        if (verticalState != null) return;
        this.verticalState = GetComponent<VerticalState>();
        Debug.Log(transform.name + ": LoadVerticalState", gameObject);
    }

    protected virtual void LoadSkillState()
    {
        if (skillState != null) return;
        this.skillState = GetComponent<SkillState>();
        Debug.Log(transform.name + ": LoadSkillState", gameObject);
    }

    protected virtual void LoadSkillLock()
    {
        if (skillLock != null) return;
        this.skillLock = GetComponentInChildren<SkillLock>();
        Debug.Log(transform.name + ": LoadSkillLock", gameObject);
    }

    public virtual IState<CharBaseState> GetHighestPriorityState()
    {

        ICharState<CharBaseState> s1 = statusState?.StateMachine.CurrentState as ICharState<CharBaseState>;
        if (s1!= null && s1.FSMType != FSMType.Default) return s1;

        ICharState<CharBaseState> s2 = skillState?.StateMachine.CurrentState as ICharState<CharBaseState>;
        if (s2 != null && s2.FSMType != FSMType.Default) return s2;

        ICharState<CharBaseState> s3 = verticalState?.StateMachine.CurrentState as ICharState<CharBaseState>;
        if (s3 != null && s3.FSMType != FSMType.Default) return s3;

        ICharState<CharBaseState> s4 = horizontalState?.StateMachine.CurrentState as ICharState<CharBaseState>;
        return s4;
    }

    public virtual void FlipX()
    {
        if (InputManager.Instance.MoveInput == 1) this.transform.parent.localScale = new Vector3(1, 1, 1);
        if (InputManager.Instance.MoveInput == -1) this.transform.parent.localScale = new Vector3(-1, 1, 1);
    }
}
