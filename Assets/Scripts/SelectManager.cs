using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public interface IButtonHandler
{
    void OnSelect();                        // ���� �Ǿ��� ��.
    void OnDeselect();                      // ���� �Ǿ��� ��.
    void OnSubmit();                        // ��ư�� ������ ��.
    IButtonHandler GetButtonOf(VECTOR v);
}

[System.Serializable]
public struct ButtonOf
{
    public Button upButton;
    public Button downButton;
    public Button leftButton;
    public Button rightButton;
}

public abstract class Button : MonoBehaviour, IButtonHandler
{
    [SerializeField] protected ButtonOf buttons;

    public void SetButtonOf(VECTOR v, Button button)
    {
        switch (v)
        {
            case VECTOR.Up:
                buttons.upButton = button;
                break;

            case VECTOR.Down:
                buttons.downButton = button;
                break;

            case VECTOR.Left:
                buttons.leftButton = button;
                break;

            case VECTOR.Right:
                buttons.rightButton = button;
                break;
        }
    }

    public IButtonHandler GetButtonOf(VECTOR v)
    {
        switch (v)
        {
            case VECTOR.Up:
                return buttons.upButton;

            case VECTOR.Down:
                return buttons.downButton;

            case VECTOR.Left:
                return buttons.leftButton;

            case VECTOR.Right:
                return buttons.rightButton;
        }

        return null;
    }

    public abstract void OnDeselect();

    public abstract void OnSelect();
    public abstract void OnSubmit();
}

public class SelectManager : MonoBehaviour
{
    static SelectManager instance;
    public static SelectManager Instance => instance;

    IButtonHandler current;

    private void Awake()
    {
        instance = this;
    }

    public void SetButton(IButtonHandler target)
    {
        if (target == null)
        {
            return;
        }

        // ���ʿ� ��ư ���� �� �̺�Ʈ ���.
        if (current == null)
        {
            // �Է� �Ŵ������� �ڽ��� �̺�Ʈ �Լ� ���.
            InputManager.Instance.OnInputUp += MoveButton;
            InputManager.Instance.OnSubmit += SubmitButton;
            InputManager.Instance.OnCancel += CancelButton;
        }

        // ���� ������ �ִٸ� ����.
        if (current != null)
            current.OnDeselect();

        // ���� �������� ����.
        current = target;
        current.OnSelect();
    }
    public void ClearButton()
    {
        if (current != null)
            current.OnDeselect();

        current = null;

        // �Է� �Ŵ������� �ڽ��� �̺�Ʈ �Լ� ��� ����.
        InputManager.Instance.OnInputUp -= MoveButton;
        InputManager.Instance.OnSubmit -= SubmitButton;
        InputManager.Instance.OnCancel -= CancelButton;
    }
    public void MoveButton(VECTOR v)
    {
        if (current == null)
        {
            return;
        }

        // vector�� �ش��ϴ� ���� ��ư�� �ִٸ�
        IButtonHandler nextSelect = current.GetButtonOf(v);
        if (nextSelect != null)
        {
            // �װ��� ����.
            SetButton(nextSelect);
        }
    }
    public void SubmitButton()
    {
        if (current == null)
        {
            return;
        }

        // Ŭ��.
        current.OnSubmit();
    }
    public void CancelButton()
    {

    }
}