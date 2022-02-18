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
        Debug.Log("상점 구매창 열기");
        ShopManager.Instance.OpenShop();

        talkManager.Instance.Talk(new string[]
        {
            "안녕?",
            "이곳은 우리가 게임을 개발하는 곳이야.",
            "그렇다",
            "그러하다",
        });
    }

    public string GetName()
    {
        return npcName;
    }
}
