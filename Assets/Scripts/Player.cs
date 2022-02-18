using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum VECTOR
{
    None = -1,
    Up,
    Down,
    Left,
    Right,
}

public class Player : Singleton<Player>
{
    readonly Vector2[] vectors = new Vector2[] {
       Vector2.up,
       Vector2.down,
       Vector2.left,
       Vector2.right
    };

    [SerializeField] float moveSpeed = 5f;

    Animator anim;
    VECTOR beforeInput = VECTOR.Down;
    bool isMoving = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        SwitchControl(true);
    }

    public void SwitchControl(bool isUnlock)
    {
        if (isUnlock)
        {
            InputManager.Instance.OnInputDown += OnMovement;
            InputManager.Instance.OnSubmit += OnSubmit;
            InputManager.Instance.OnCancel += OnCancel;            
        }
        else
        {
            InputManager.Instance.OnInputDown -= OnMovement;
            InputManager.Instance.OnSubmit -= OnSubmit;
            InputManager.Instance.OnCancel -= OnCancel;
        }
    }

    private void OnSubmit()
    {
        if (isMoving)
            return;

        Collider2D node = RayToVector(beforeInput);
        if (node == null)
            return;

        IInteraction target = node.GetComponent<IInteraction>();
        if (target != null)
            target.Interaction();
    }
    private void OnCancel()
    {

    }
    private void OnMovement(VECTOR input)
    {
        if (isMoving)
            return;

        // ������ �ٸ� �������� ���������� �ִϸ��̼� ���, ��ȣ�ۿ� UI�ݱ�.
        if (input != beforeInput)
        {
            anim.SetInteger("MoveVector", (int)input);
            anim.SetTrigger("onMove");

            InteractionUI.Instance.Close();
        }

        // �̵� ����.
        StartCoroutine(Movement(input));
    }
    private IEnumerator Movement(VECTOR vector)
    {
        // ���������� ��� �� �̵�.
        if (RayToVector(vector) == null)
        {
            Vector2 myPosition = transform.position;
            Vector3 destination = myPosition + vectors[(int)vector];

            isMoving = true;

            // ���������� �̵�.
            while (transform.position != destination)
            {
                transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
                yield return null;
            }
        }

        // ������ ���� ���� �̵� �������� ���� �߻�.
        Collider2D collider = RayToVector(vector);
        if (collider != null)
        {
            IInteraction target = collider.GetComponent<IInteraction>();

            // ��ȣ�ۿ� Ÿ���� �����Ѵٸ� �ش� Ÿ���� �̸��� ���.
            if (target != null)
                InteractionUI.Instance.Open(target.GetName());
        }

        isMoving = false;

        // ���� �� ���� ���� ����.
        beforeInput = vector;
    }

    private Collider2D RayToVector(VECTOR vector)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, vectors[(int)vector], 1f);
        return hit.collider;
    }
}


