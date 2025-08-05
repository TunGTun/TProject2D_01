using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MyMonoBehaviour
{
    private static InputManager _instance;
    public static InputManager Instance { get => _instance; }

    [Header("InputManager")]

    protected KeyCode _lastKeyPressed;

    [SerializeField] protected float _moveInput;
    public float MoveInput { get => _moveInput; }

    [SerializeField] protected bool jumpInput;
    public bool JumpInput { get => jumpInput; }

    [SerializeField] protected bool dashInput;
    public bool DashInput { get => dashInput; }

    [SerializeField] protected bool canControl = true;
    public bool CanControl => canControl;

    protected override void Awake()
    {
        base.Awake();
        if (InputManager._instance != null) Debug.LogError("Only 1 InputManager allow to exist");
        InputManager._instance = this;
    }

    void Update()
    {
        if (!this.CanControl)
        {
            this._moveInput = 0;
            //this._moveAccelInput = 0;
            return;
        }

        this.CheckMoveInput();
        this.CheckJumpInput();
        this.CheckDashInput();
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

    protected virtual void CheckJumpInput()
    {
        this.jumpInput = Input.GetKeyDown(KeyCode.Space);
    }

    protected virtual void CheckDashInput()
    {
        this.dashInput = Input.GetKeyDown(KeyCode.LeftShift);
    }

}