using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private static InputHandler m_instance;

    #region Delegates
    
    // Delegates for key presses
    [HideInInspector] public delegate void KeyDown();
    [HideInInspector] public delegate void KeyUp();
    [HideInInspector] public delegate void KeyRight();
    [HideInInspector] public delegate void KeyLeft();

    [HideInInspector] public delegate void KeyStart();
    [HideInInspector] public delegate void KeyExit();

    // Delegates for key helds
    [HideInInspector] public delegate void KeyLeftHeld();
    [HideInInspector] public delegate void KeyDownHeld();
    [HideInInspector] public delegate void KeyUpHeld();
    [HideInInspector] public delegate void KeyRightHeld();

    // Delegates for key releases
    [HideInInspector] public delegate void KeyLeftRelease();
    [HideInInspector] public delegate void KeyDownRelease();
    [HideInInspector] public delegate void KeyUpRelease();
    [HideInInspector] public delegate void KeyRightRelease();

    // Delegates for when no key is pressed
    [HideInInspector] public delegate void NoKeyHorizontal();
    [HideInInspector] public delegate void NoKeyVertical();
    
    #endregion

    #region Events
    
    // Events - key press
    [HideInInspector] public event Action KeyLeftEvent;
    [HideInInspector] public event Action KeyDownEvent;
    [HideInInspector] public event Action KeyUpEvent;
    [HideInInspector] public event Action KeyRightEvent;

    [HideInInspector] public event Action KeyStartEvent;
    [HideInInspector] public event Action KeyRestartEvent;
    [HideInInspector] public event Action KeyExitEvent;

    // Events - key helds
    [HideInInspector] public event Action KeyLeftHeldEvent;
    [HideInInspector] public event Action KeyDownHeldEvent;
    [HideInInspector] public event Action KeyUpHeldEvent;
    [HideInInspector] public event Action KeyRightHeldEvent;

    // Events - key releases
    [HideInInspector] public event Action KeyLeftReleaseEvent;
    [HideInInspector] public event Action KeyDownReleaseEvent;
    [HideInInspector] public event Action KeyUpReleaseEvent;
    [HideInInspector] public event Action KeyRightReleaseEvent;

    // Events - no keys
    [HideInInspector] public event Action NoKeyHorizontalEvent;
    [HideInInspector] public event Action NoKeyVerticalEvent;
    
    #endregion

    #region Keys

    private KeyCode m_leftKey;
    private KeyCode m_downKey;
    private KeyCode m_upKey;
    private KeyCode m_rightKey;

    private KeyCode m_startKey;
    private KeyCode m_exitKey;


    private KeyCode m_leftKeyCustom;
    private KeyCode m_downKeyCustom;
    private KeyCode m_upKeyCustom;
    private KeyCode m_rightKeyCustom;

    private KeyCode m_startKeyCustom;
    private KeyCode m_exitKeyCustom;

    private KeyCode m_restartKeyCustom;

    #endregion

    public static InputHandler Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<InputHandler>();
                if (m_instance == null)
                {
                    GameObject obj = new()
                    {
                        name = typeof(InputHandler).Name
                    };
                    m_instance = obj.AddComponent<InputHandler>();
                    DontDestroyOnLoad(obj);
                }
            }

            return m_instance;
        }
    }

    void Update()
    {
        UpdateKeys();

        // Events - key press
        if (Input.GetKeyDown(m_leftKey) || Input.GetKeyDown(m_leftKeyCustom)) { OnKeyLeft(); }
        if (Input.GetKeyDown(m_downKey) || Input.GetKeyDown(m_downKeyCustom)) { OnKeyDown(); }
        if (Input.GetKeyDown(m_upKey) || Input.GetKeyDown(m_upKeyCustom)) { OnKeyUp(); }
        if (Input.GetKeyDown(m_rightKey) || Input.GetKeyDown(m_rightKeyCustom)) { OnKeyRight(); }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(m_startKeyCustom)) { OnKeyStart(); }
        if (Input.GetKeyDown(m_restartKeyCustom)) { OnKeyRestart(); }
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(m_exitKeyCustom)) { OnKeyExit(); }

        // Events - key helds
        if (Input.GetKey(m_leftKey) || Input.GetKey(m_leftKeyCustom)) { OnKeyLeftHeld(); }
        if (Input.GetKey(m_downKey) || Input.GetKey(m_downKeyCustom)) { OnKeyDownHeld(); }
        if (Input.GetKey(m_upKey) || Input.GetKey(m_upKeyCustom)) { OnKeyUpHeld(); }
        if (Input.GetKey(m_rightKey) || Input.GetKey(m_rightKeyCustom)) { OnKeyRightHeld(); }

        // Events - key releases
        if (Input.GetKeyUp(m_leftKey) || Input.GetKeyUp(m_leftKeyCustom)) { OnKeyLeftRelease(); }
        if (Input.GetKeyUp(m_downKey) || Input.GetKeyUp(m_downKeyCustom)) { OnKeyDownRelease(); }
        if (Input.GetKeyUp(m_upKey) || Input.GetKeyUp(m_upKeyCustom)) { OnKeyUpRelease(); }
        if (Input.GetKeyUp(m_rightKey) || Input.GetKeyUp(m_rightKeyCustom)) { OnKeyRightRelease(); }

        // Events - no keys

        if (!(Input.GetKey(m_leftKey) || Input.GetKey(m_leftKeyCustom)) &&
            !(Input.GetKey(m_rightKey) || Input.GetKey(m_rightKeyCustom)))
            { OnNoKeyHorizontal(); }

        if (!(Input.GetKey(m_upKey) || Input.GetKey(m_upKeyCustom)) &&
            !(Input.GetKey(m_downKey) || Input.GetKey(m_downKeyCustom)))
            { OnNoKeyVertical(); }
    }

    void UpdateKeys()
    {
        m_leftKey = Modifications.Instance.DefaultKeys[KeyTypes.LeftKey];
        m_downKey = Modifications.Instance.DefaultKeys[KeyTypes.DownKey];
        m_upKey = Modifications.Instance.DefaultKeys[KeyTypes.UpKey];
        m_rightKey = Modifications.Instance.DefaultKeys[KeyTypes.RightKey];

        m_startKey = Modifications.Instance.DefaultKeys[KeyTypes.StartKey];
        m_exitKey = Modifications.Instance.DefaultKeys[KeyTypes.ExitKey];

        m_leftKeyCustom = Modifications.Instance.CustomKeys[KeyTypes.LeftKey];
        m_downKeyCustom = Modifications.Instance.CustomKeys[KeyTypes.DownKey];
        m_upKeyCustom = Modifications.Instance.CustomKeys[KeyTypes.UpKey];
        m_rightKeyCustom = Modifications.Instance.CustomKeys[KeyTypes.RightKey];

        m_startKeyCustom = Modifications.Instance.CustomKeys[KeyTypes.StartKey];
        m_exitKeyCustom = Modifications.Instance.CustomKeys[KeyTypes.ExitKey];

        m_restartKeyCustom = Modifications.Instance.CustomKeys[KeyTypes.RestartKey];
    }

    #region Event Raisers

    protected virtual void OnKeyLeft()
    {
        KeyLeftEvent?.Invoke();
    }
    protected virtual void OnKeyDown()
    {
        KeyDownEvent?.Invoke();
    }
    protected virtual void OnKeyUp()
    {
        KeyUpEvent?.Invoke();
    }
    protected virtual void OnKeyRight()
    {
        KeyRightEvent?.Invoke();
    }

    protected virtual void OnKeyStart()
    {
        KeyStartEvent?.Invoke();
    }
    protected virtual void OnKeyRestart()
    {
        KeyRestartEvent?.Invoke();
    }
    protected virtual void OnKeyExit()
    {
        KeyExitEvent?.Invoke();
    }

    protected virtual void OnKeyLeftHeld()
    {
        KeyLeftHeldEvent?.Invoke();
    }
    protected virtual void OnKeyDownHeld()
    {
        KeyDownHeldEvent?.Invoke();
    }
    protected virtual void OnKeyUpHeld()
    {
        KeyUpHeldEvent?.Invoke();
    }
    protected virtual void OnKeyRightHeld()
    {
        KeyRightHeldEvent?.Invoke();
    }

    protected virtual void OnKeyLeftRelease()
    {
        KeyLeftReleaseEvent?.Invoke();
    }
    protected virtual void OnKeyDownRelease()
    {
        KeyDownReleaseEvent?.Invoke();
    }
    protected virtual void OnKeyUpRelease()
    {
        KeyUpReleaseEvent?.Invoke();
    }
    protected virtual void OnKeyRightRelease()
    {
        KeyRightReleaseEvent?.Invoke();
    }

    protected virtual void OnNoKeyHorizontal()
    {
        NoKeyHorizontalEvent?.Invoke();
    }
    protected virtual void OnNoKeyVertical()
    {
        NoKeyVerticalEvent?.Invoke();
    }

    #endregion

    #region Dancing Agents Input

    public void InputPress(Directions direction)
    {
        switch (direction)
        {
            case Directions.left:
                OnKeyLeft();
                break;
            case Directions.right:
                OnKeyRight();
                break;
            case Directions.up:
                OnKeyUp();
                break;
            case Directions.down:
                OnKeyDown();
                break;
        }
    }

    public void InputHeld(Directions direction)
    {
        switch (direction)
        {
            case Directions.left:
                OnKeyLeftHeld();
                break;
            case Directions.right:
                OnKeyRightHeld();
                break;
            case Directions.up:
                OnKeyUpHeld();
                break;
            case Directions.down:
                OnKeyDownHeld();
                break;
        }
    }

    public void InputRelease(Directions direction)
    {
        switch (direction)
        {
            case Directions.left:
                OnKeyLeftRelease();
                break;
            case Directions.right:
                OnKeyRightRelease();
                break;
            case Directions.up:
                OnKeyUpRelease();
                break;
            case Directions.down:
                OnKeyDownRelease();
                break;
        }
    }

    #endregion
}
