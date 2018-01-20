using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class CartEvent : CardEvent
{
    private bool _phase1;
    private bool _phase2;

    public CartEvent()
    {
        ChoiceText = "While traveling in the woods you see a wooden cart blocking the road with some people around it." +
                     "You can't make out whats wrong with the cart from this distance. " +
                     "You can go to the cart and offer help or take ignore the cart and go on with the journey.";
        ChoiceButton1Text = "Go to the cart and offer you're help";
        ChoiceButton2Text = "Pass the cart and the people and go on with the journey";
        ChoiceButton3Text = "";
        SpriteName = "cart";

        _phase1 = true;
        _phase2 = false;
    }

    public override void Choice1()
    {
        if (_phase1)
        {
            ChoiceText = "You walk to the people. An old man speaks first, \"Can you help us? " +
                            "The wheel of our cart broke and we are unable to repair it.\"";
            ChoiceButton1Text = "Try to repair the cartwheel";
            ChoiceButton2Text = "Apologize to the people that you can't help and go on with you're journey";
            ChoiceButton3Text = "";
            Card.GameManager.CanvasManager.ShowChoiceTextScreen(this);
            _phase1 = false;
            _phase2 = true;
        }
        else if (_phase2)
        {
            Chances = new List<bool>();
            int successes = 1;

            if (PlayerStats.Intel >= 4 && PlayerStats.Intel <= 8)
                successes = 2;
            else if (PlayerStats.Str >= 9)
                successes = 3;

            for (int i = 0; i < 4; i++)
                Chances.Add(false);

            for (int i = 0; i < successes; i++)
                Chances[i] = true;

            Card.GameManager.CanvasManager.SetCardChances(Chances);
            Chances = ShuffList(Chances);
        }
    }

    public override void Choice2()
    {
        if (_phase1 || _phase2)
        {
            string text = "You leave the people and broken cart behind and go on with you're journey.";
            Card.GameManager.CanvasManager.ShowScreenResultFromButtons(text);
        }
    }

    public override void Choice3()
    {
        
    }

    public override void ChanceSucces()
    {
        int reward = Random.Range(3, 7);
        string text = "You succesfully repair the cartwheel. The old man thanks you and gives you a reward (+" + reward + " Gold)";
        PlayerStats.Gold += reward;
        Card.GameManager.CanvasManager.UpdatePlayerInfo();
        Card.GameManager.CanvasManager.SetCardChancesResult(Chances, text);
    }

    public override void ChanceFailed()
    {
        string text;
        if (PlayerStats.Gold >= 4)
        {
            text = "You try to repair the cartwheel but the longer you go on the worse it gets. " +
                          "The old man asks you to stop. You stop and give him some gold to pay for the repair of the extra damage you have done (-4 Gold)";
            PlayerStats.Gold -= 4;
            Card.GameManager.CanvasManager.UpdatePlayerInfo();
        }
        else if (PlayerStats.Gold == 0)
        {
            text = "You try to repair the cartwheel but the longer you go on the worse it gets. " +
                          "The old man asks you to stop. You stop and give him the gold you have left to pay for the repair of the extra damage you have done";
            PlayerStats.Gold = 0;
            Card.GameManager.CanvasManager.UpdatePlayerInfo();
        }
        else
        {
            text = "You try to repair the cartwheel but the longer you go on the worse it gets. " +
                          "The old man asks you to stop. You stop and go on with you're journey, leaving the people and cart behind";
        }

        Card.GameManager.CanvasManager.SetCardChancesResult(Chances, text);
    }
}