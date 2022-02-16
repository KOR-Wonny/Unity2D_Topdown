using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    static ShopManager instance;
    public static ShopManager Instance => instance;

    [SerializeField] ShopItem prefab;
    [SerializeField] Transform itemParent;
    [SerializeField] GameObject panel;

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
        Player.Instance.SwitchControl(false);

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
        for(int i = 0; i<shopItemList.Count; i++)
        {
            Button down = shopItemList[i];
            up.SetButtonOf(VECTOR.Down, down);
            down.SetButtonOf(VECTOR.Up, up);

            up = down;
        }

        // �÷��̾��� �Է� ����.
        Player.Instance.SwitchControl(false);                   // �÷��̾� �Է� ����.
        SelectManager.Instance.SetButton(shopItemList[0]);      // ��ư �Ŵ������� ���� ��ư ���
        InputManager.Instance.OnCancel += CloseShop;            // ���� �ݱ� �̺�Ʈ ���.
    }
    
    public void CloseShop()
    {
        InputManager.Instance.OnCancel -= CloseShop;            // ���� �ݱ� �̺�Ʈ ��� ����.
        Player.Instance.SwitchControl(true);                    // �÷��̾� �Է� ����.
        panel.SetActive(false);                                 // �г� ����.
    }

}
