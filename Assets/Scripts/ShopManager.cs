using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : SelectManager
{
    static ShopManager instance;
    public static ShopManager Instance => instance;

    [SerializeField] ShopItem prefab;
    [SerializeField] Transform itemParent;
    [SerializeField] GameObject panel;

    [SerializeField] RectTransform scrollView;
    [SerializeField] RectTransform content;

    List<ShopItem> shopItemList = new List<ShopItem>();

    private void Awake()
    {
        instance = this;        
    }
    private void Start()
    {
        panel.SetActive(false);
    }

    public void OpenShop()
    {
        panel.SetActive(true);
        
        // 10���� ������ ����.
        for (int i = 0; i<10; i++)
        {
            ShopItem newItem = Instantiate(prefab);
            newItem.transform.SetParent(itemParent);
            newItem.Setup();            

            shopItemList.Add(newItem);
        }

        // shopItem���� ������ ���⿡ ���� ��ư�� ����.
        Button up = shopItemList[0];
        for(int i = 1; i<shopItemList.Count; i++)
        {
            Button down = shopItemList[i];
            up.SetButtonOf(VECTOR.Down, down);
            down.SetButtonOf(VECTOR.Up, up);

            up = down;
        }

        // �÷��̾��� �Է� ����.
        Player.Instance.SwitchControl(false);               // �÷��̾� �Է� ����.
        InputManager.Instance.OnCancel += CloseShop;        // ���� �ݱ� �̺�Ʈ ���.

        SetButton(shopItemList[0]);
    }
    public void CloseShop()
    {
        InputManager.Instance.OnCancel -= CloseShop;    // ���� �ݱ� �̺�Ʈ ��� ����.
        Player.Instance.SwitchControl(true);            // �÷��̾� �Է� ����.
        panel.SetActive(false);                         // �г� ����.

        // ��ư ���� ����.
        ClearButton();

        // �����ߴ� ���� ������ ��� ����.
        for (int i = 0; i<shopItemList.Count; i++)
            Destroy(shopItemList[i].gameObject);

        // ����Ʈ Clear.
        shopItemList.Clear();                
    }

    protected override void MoveButton(VECTOR v)
    {
        base.MoveButton(v);

        // ���� ����� ���� ����.
        float viewHeight = scrollView.rect.height;
        float currentY = current.transform.localPosition.y;
        float currentHeight = current.GetComponent<RectTransform>().rect.height;

        float contentY = Mathf.Abs(currentY) - viewHeight + currentHeight;   // content�� y�� ����.

        // �������� ���� �� y���� ���� ���� �� ����.
        Vector3 localPosition = content.transform.localPosition;
        localPosition.y = contentY;
        content.transform.localPosition = localPosition;
    }
}
