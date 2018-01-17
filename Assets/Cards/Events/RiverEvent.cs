using System.Collections.Generic;
using UnityEngine;

public class RiverEvent : CardEvent
{
    public RiverEvent()
    {
        ChoiceText = "You come across a wide river with a strong current but there is no bridge or ferry in sight to get across safely. " +
                     "You can try to swim to the other bank or follow the river until you find a safe way to cross.";
        ChoiceButton1Text = "Try to swim to the other side of the river";
        ChoiceButton2Text = "Follow the river until you find a bridge or ferry to safely get across.";
        ChoiceButton3Text = "";
        SpriteName = "river";
    }

    public override void Choice1()
    {
        Chances = new List<bool>();
        int successes = 1;

        if (PlayerStats.Str >= 4 && PlayerStats.Str <= 8)
            successes = 2;
        else if (PlayerStats.Str >= 9)
            successes = 3;

        for (int i = 0; i < 4; i++)
        {
            Chances.Add(false);
        }
        for (int i = 0; i < successes; i++)
        {
            Chances[i] = true;
        }

        Card.GameManager.CanvasManager.SetCardChances(Chances);
        Chances = ShuffList(Chances);
    }

    public override void Choice2()
    {
        int supplyLose = Random.Range(1, 5);

        string text = "You follow the river downwards and after a few hours you find a bridge that spans to the other side. " +
                      "You use the bridge to cross the river and go on with the journey. (-" + supplyLose + " Supplies)";

        PlayerStats.Supplies -= supplyLose;
        if (PlayerStats.Supplies < 0)
            PlayerStats.Supplies = 0;
        
        Card.GameManager.CanvasManager.UpdatePlayerInfo();
        Card.GameManager.CanvasManager.ShowScreenResultFromButtons(text);
    }

    public override void Choice3()
    {
        
    }

    public override void ChanceSucces()
    {
        string text = "You decide you don't want to walk until you find a ferry or bridge and decide to swim to the other side. " +
                      "The current almost gets you but you manage to safely swim to the other side. " +
                      "On the bank of the river you make a campfire to dry up";

        if (PlayerStats.Supplies >= 1)
        {
            text += " and to cook something to get some energy back. (-1 Supplies)";
            PlayerStats.Supplies -= 1;
        }
        else
        {
            text += ". You look in you're backpack but there is nothing left to eat. (-2 Health)";
            PlayerStats.Health -= 2;
        }

        Card.GameManager.CanvasManager.UpdatePlayerInfo();
        Card.GameManager.CanvasManager.SetCardChancesResult(Chances, text);
    }

    public override void ChanceFailed()
    {
        string text =
            "You decide to try you're luck and swim to the other side. " +
            "You almost get to the other side but the current proves to strong and you start struggling to keep you're head above water. " +
            "You see a small goblin on the river bank and cry for help, but instead of helping he gets a stone and throws it at you're head. " +
            "The stone hits you're head and you lose consiousness.";

        if (PlayerStats.Health > 5 && PlayerStats.Supplies > 4)
        {
            text += "By a miracle you wake up on the other side of the river, but the first thing you see is the face of the goblin. " +
                    "The goblin panicks and runs away with some of you're supplies and gold. " +
                   "You are to tired to chase it and make a campfire to dry up and eat something. (-5 Health, -4 Supplies, -5 Gold)";
            PlayerStats.Health -= 5;
            PlayerStats.Supplies -= 4;

            if (PlayerStats.Gold > 5)
                PlayerStats.Gold -= 5;
            else
                PlayerStats.Gold = 0;

        }
        else if (PlayerStats.Health > 7 && PlayerStats.Supplies <= 4)
        {
            text += "By a miracle you wake up on the other side of the river, but the first thing you see is the face of the goblin. " +
                    "The goblin panicks and runs away with some of you're supplies and gold. " +
                   "You are to tired to chase it and make a campfire to dry up but you have nothing to eat. (-7 Health, -4 Supplies, -5 Gold)";
            PlayerStats.Health -= 7;
            PlayerStats.Supplies = 0;

            if (PlayerStats.Gold > 5)
                PlayerStats.Gold -= 5;
            else
                PlayerStats.Gold = 0;
        }
        else
        {
            text += "You don't wake up.";
            PlayerStats.Health = 0;
        }

        Card.GameManager.CanvasManager.UpdatePlayerInfo();
        Card.GameManager.CanvasManager.SetCardChancesResult(Chances, text);
    }
}