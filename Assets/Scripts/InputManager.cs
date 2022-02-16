using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    [SerializeField] Player player;

    public delegate void InputEvent(VECTOR v);
    public event InputEvent OnInputDown;
    public event InputEvent OnInputUp;
    public event System.Action OnSubmit;
    public event System.Action OnCancel;

    public void InputDown(VECTOR vector)
    {
        OnInputDown?.Invoke(vector);
    }

    public void InputUp(VECTOR vector)
    {
        OnInputUp?.Invoke(vector);
    }

    public void Submit()
    {
        // ���� �̺�Ʈ ȣ��.
        OnSubmit?.Invoke();
    }
    public void Cancel()
    {
        // ��� �̺�Ʈ ȣ��.
        OnCancel?.Invoke();
    }
}
