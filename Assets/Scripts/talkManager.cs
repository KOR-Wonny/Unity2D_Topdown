using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class talkManager : Singleton<talkManager>
{
    [SerializeField] RectTransform talkRect;
    [SerializeField] RectTransform downPivot;
    [SerializeField] Text talkText;

    Vector3 originPosition;         // ��Ÿ���� ��ġ
    Vector3 hidePosition;           // ���� ��ġ.

    string[] comments;                 // ��ȭ ����.
    bool isClick = false;

    private void Start()
    {
        originPosition = talkRect.position;
        hidePosition = downPivot.position;

        // ���� ���۽ÿ��� ���� ��ġ�� �̵�.
        talkRect.position = hidePosition;
    }

    public void Talk(string[] comments)
    {
        this.comments = comments;

        Player.Instance.SwitchControl(false);               // �÷��̾� �Է� �̺�Ʈ ����.
        InputManager.Instance.OnSubmit += OnSubmit;         // Ȯ�� �̺�Ʈ ���.
        InputManager.Instance.OnCancel += OnCancel;         // ��� �̺�Ʈ ���.

        StartCoroutine(MovePanel(false, () => StartCoroutine(Talk())));
    }

    public void Close()
    {
        Player.Instance.SwitchControl(true);                // �÷��̾� �Է� �̺�Ʈ ���.
        InputManager.Instance.OnSubmit -= OnSubmit;         // Ȯ�� �̺�Ʈ ����.
        InputManager.Instance.OnCancel -= OnCancel;         // ��� �̺�Ʈ ����.

        StartCoroutine(MovePanel(true));
    }

    private void OnSubmit()
    {
        isClick = true;
    }

    private void OnCancel()
    {
    }

    IEnumerator MovePanel(bool isHide, System.Action OnEndMove = null)
    {
        Vector3 destination = isHide ? hidePosition : originPosition;
        float speed = Screen.height * 0.1f; // ���� �� ȭ���� ����

        while(Vector3.Distance(talkRect.position, destination) > 0.01f)
        {
            talkRect.position = Vector3.MoveTowards(talkRect.position, destination, speed * Time.deltaTime);
            yield return null;
        }

        talkRect.position = destination;

        OnEndMove?.Invoke();
    }

    IEnumerator Talk()
    {
        foreach (string c in comments)
        {
            talkText.text = c;                                // ��ȭ �ؽ�Ʈ �Է�.
            yield return StartCoroutine(Waitting());          // ������ �Է��� �� ���� ���.
        }

        Close();
    }

    IEnumerator Waitting()
    {
        isClick = false;

        while (isClick)                     // ������ �Է��� ������ ������ �ݺ�.
            yield return null;              // �� ������ ���.
    }
}
