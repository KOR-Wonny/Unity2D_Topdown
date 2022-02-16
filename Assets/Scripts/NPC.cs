using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteraction
{
    void Interaction();
    string GetName();
}

public class NPC : MonoBehaviour, IInteraction
{
    [SerializeField] string npcName;

    public void Interaction()
    {
        Debug.Log("���� ����â ����");
        ShopManager.Instance.OpenShop();
    }

    public string GetName()
    {
        return npcName;
    }
}
