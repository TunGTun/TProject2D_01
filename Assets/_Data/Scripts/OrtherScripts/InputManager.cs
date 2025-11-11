using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MySingleton<InputManager>
{
    [Header("InputManager")]

    [SerializeField] protected bool canControl = true;
    public bool CanControl => canControl;

    protected KeyCode _lastKeyPressed;

    [SerializeField] protected float _moveInput;
    public float MoveInput { get => _moveInput; set => _moveInput = value; }

    [SerializeField] protected bool jumpInputDown;
    public bool JumpInputDown { get => jumpInputDown; set => jumpInputDown = value; }

    [SerializeField] protected bool jumpInputUp;
    public bool JumpInputUp { get => jumpInputUp; set => jumpInputUp = value; }

    [SerializeField] protected bool dashInput;
    public bool DashInput { get => dashInput; }

    [SerializeField] protected bool downInput;
    public bool DownInput { get => downInput; }

    [SerializeField] protected bool upInput;
    public bool UpInput { get => upInput; }

    [SerializeField] protected bool attackInput;
    public bool AttackInput { get => attackInput; }

    [SerializeField] protected bool healInput;
    public bool HealInput { get => healInput; }

    [SerializeField] protected bool backInput;
    public bool BackInput { get => backInput; }

    void Update()
    {
        this.CheckInput();
    }

    protected virtual void CheckInput()
    {
        this.CheckBackInput();

        if (!this.CanControl) return;

        this.CheckMoveInput();
        this.CheckJumpInputDown();
        this.CheckJumpInputUp();
        this.CheckDashInput();
        this.CheckDownInput();
        this.CheckDownInput();
        this.CheckUpInput();
        this.CheckAttackClick();
        this.CheckHealInput();
    }

    public void SetCanControl(bool canControl)
    {
        this.canControl = canControl;
        this._moveInput = 0;
    }

    protected virtual void CheckMoveInput()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _moveInput = 0;
            _lastKeyPressed = KeyCode.A;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            _moveInput = 0;
            _lastKeyPressed = KeyCode.D;
        }

        if (Input.GetKeyUp(KeyCode.A) && Input.GetKey(KeyCode.D)) _lastKeyPressed = KeyCode.D;

        if (Input.GetKeyUp(KeyCode.D) && Input.GetKey(KeyCode.A)) _lastKeyPressed = KeyCode.A;

        if (_lastKeyPressed == KeyCode.A && Input.GetKey(KeyCode.A))
        {
            _moveInput = -1;
            return;
        }

        if (_lastKeyPressed == KeyCode.D && Input.GetKey(KeyCode.D))
        {

            _moveInput = 1;
            return;
        }
        _moveInput = 0;
    }

    protected virtual void CheckJumpInputDown()
    {
        this.jumpInputDown = Input.GetKeyDown(KeyCode.Space);
    }

    protected virtual void CheckJumpInputUp()
    {
        this.jumpInputUp = Input.GetKeyUp(KeyCode.Space);
    }

    protected virtual void CheckDashInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.LeftControl))
            this.dashInput = true;
        else
            this.dashInput = false;
    }

    protected virtual void CheckDownInput()
    {
        this.downInput = Input.GetKey(KeyCode.S);
    }

    protected virtual void CheckUpInput()
    {
        this.upInput = Input.GetKey(KeyCode.W);
    }

    protected virtual void CheckAttackClick()
    {
        this.attackInput = Input.GetMouseButtonDown(0);
    }

    protected virtual void CheckHealInput()
    {
        this.healInput = Input.GetKeyDown(KeyCode.F);
    }

    protected virtual void CheckBackInput()
    {
        this.backInput = Input.GetKeyDown(KeyCode.Escape);
    }
}