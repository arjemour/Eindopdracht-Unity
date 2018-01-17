using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public Card Card { get; set; }
    public GameManager GameManager { get; set; }

    public void NextButtonPressed()
    {
        GameManager.CanvasManager.ShowChoiceButtonScreen(Card.CardEvent);
    }

    public void Choice1Pressed()
    {
        Card.CardEvent.Choice1();
    }

    public void Choice2Pressed()
    {
        Card.CardEvent.Choice2();
    }

    public void Choice3Pressed()
    {
        Card.CardEvent.Choice3();
    }

    public void ChanceResult(int cardNumber)
    {
        bool result = Card.CardEvent.Chances[cardNumber];

        if(result)
            Card.CardEvent.ChanceSucces();
        else
            Card.CardEvent.ChanceFailed();
    }

    public void ContinueResultPressed()
    {
        if (PlayerStats.Health > 0)
        {
            GameManager.CanvasManager.Animator.SetTrigger("GoToCardScreen");
            GameManager.ContinueCardScreen();
        }
        else
            GameManager.GameOver();
    }
}
