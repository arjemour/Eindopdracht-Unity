using System.Collections.Generic;
using UnityEngine;

public abstract class CardEvent
{
    public string ChoiceText { get; protected set; }
    public string ChoiceButton1Text { get; protected set; }
    public string ChoiceButton2Text { get; protected set; }
    public string ChoiceButton3Text { get; protected set; }
    public string SpriteName { get; protected set; }

    public List<bool> Chances { get; protected set; }

    public Card Card { get; set; }

    protected List<bool> ShuffList(List<bool> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            bool temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }

        return list;
    }

    public abstract void Choice1();
    public abstract void Choice2();
    public abstract void Choice3();

    public abstract void ChanceSucces();
    public abstract void ChanceFailed();
}
