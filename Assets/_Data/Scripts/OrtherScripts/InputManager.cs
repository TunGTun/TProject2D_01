using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MyMonoBehaviour
{
    private static InputManager _instance;
    public static InputManager Instance { get => _instance; }

    [Header("InputManager")]

    [SerializeField] protected bool canControl = true;
    public bool CanControl => canControl;

    protected KeyCode _lastKeyPressed;

    [SerializeField] protected float _moveInput;
    public float MoveInput { get => _moveInput; }

    [SerializeField] protected bool spaceInput;
    public bool SpaceInput { get => spaceInput; }

    [SerializeField] protected bool leftShiftInput;
    public bool LeftShiftInput { get => leftShiftInput; }

    [SerializeField] protected bool leftCtrlInput;
    public bool LeftCtrlInput { get => leftCtrlInput; }

    [SerializeField] protected bool sInput;
    public bool SInput { get => sInput; }

    [SerializeField] protected bool leftMouseClick;
    public bool LeftMouseClick { get => leftMouseClick; }

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
            return;
        }
        this.CheckMoveInput();
        this.CheckSpacepInput();
        this.CheckLeftShiftInput();
        this.CheckLeftCtrlInput();
        this.CheckSInput();
        this.CheckLeftMouseClick();
    }

    public void SetCanControl(bool canControl)
    {
        this.canControl = canControl;
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

    protected virtual void CheckSpacepInput()
    {
        this.spaceInput = Input.GetKeyDown(KeyCode.Space);
    }

    protected virtual void CheckLeftShiftInput()
    {
        this.leftShiftInput = Input.GetKeyDown(KeyCode.LeftShift);
    }

    protected virtual void CheckLeftCtrlInput()
    {
        this.leftCtrlInput = Input.GetKeyDown(KeyCode.LeftControl);
    }

    protected virtual void CheckSInput()
    {
        this.sInput = Input.GetKey(KeyCode.S);
    }

    protected virtual void CheckLeftMouseClick()
    {
        this.leftMouseClick = Input.GetMouseButtonDown(0);
    }

}