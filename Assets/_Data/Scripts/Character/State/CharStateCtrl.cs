using System;
using Unity.VisualScripting;
using UnityEngine;
using FixedUpdate = UnityEngine.PlayerLoop.FixedUpdate;

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

    public VelocityHandle VelocityHandle { get; private set; }
    protected Vector2 targetVelocity;
    protected bool hasSetter = false;
    protected int currentPriority = 0;
    
    public InputBuffer InputBuffer { get; private set; } //Mới chỉ hoạt động cho attack và dash trong SkillState
    
    public bool canDoubleJump = true;
    public bool canDash = true;
    
    protected override void Start()
    {
        base.Start();
        this.Init();
    }

    protected virtual void Init()
    {
        VelocityHandle = new VelocityHandle(this.charCtrl.RigidBody2D);
        
        this.horizontalState.ChangeState(this.horizontalState.idleX);
        this.verticalState.ChangeState(this.verticalState.idleGround);
        this.skillState.ChangeState(this.skillState.idleSkill);
        this.statusState.ChangeState(this.statusState.normal);
        InputBuffer = new InputBuffer(this.charCtrl.CharData.BufferWindow);
    }

    private void Update()
    {
        if (InputManager.Instance.JumpInputDown && !this.charCtrl.EnvironmentChecker.IsGrounded)
            InputBuffer.AddInput(StateName.DOUBLE_JUMP_STATE);
        
        if (InputManager.Instance.DashInput)
            InputBuffer.AddInput(StateName.DASH_STATE);

        if (InputManager.Instance.AttackInput)
            InputBuffer.AddInput(StateName.ATTACK_ONE_STATE);

        // Debug.Log(this.statusState.StateMachine.CurrentState + " / "
        //         + this.skillState.StateMachine.CurrentState + " / "
        //         + this.verticalState.StateMachine.CurrentState + " / "
        //         + this.horizontalState.StateMachine.CurrentState);

        // Debug.Log(this.charCtrl.RigidBody2D.linearVelocityY);
    }

    private void FixedUpdate()
    {
        this.VelocityHandle.Apply();
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
    
    public void ResetSkill()
    {
        if(!canDoubleJump) canDoubleJump = true;
        if(!canDash) canDash = true;
    }
}
