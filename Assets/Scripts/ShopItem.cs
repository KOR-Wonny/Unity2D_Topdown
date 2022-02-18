using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopItem : Button
{
    [SerializeField] Image itemImage;       // �̹���.
    [SerializeField] Image selectedImage;   // ���� �̹���.
    [SerializeField] Text nameText;         // �̸�.
    [SerializeField] Text countText;        // ����.
    [SerializeField] Text ownedText;        // ���� ����.
    [SerializeField] Text priceText;        // ����.

    public void Setup()
    {
        OnDeselect();
    }

    public override void OnSelect()
    {
        selectedImage.enabled = true;
    }
    public override void OnDeselect()
    {
        selectedImage.enabled = false;
    }
    public override void OnSubmit()
    {

    }
}
