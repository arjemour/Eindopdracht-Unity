using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerEvent : CardEvent
{
    public TowerEvent()
    {
        ChoiceText = "You are travelling through a hilly landscape when suddenly there is a bright flash." +
                     "Suddenly there is a big stone tower on top of one of the hills. The tower is to far away to make out any details. " +
                     "You can go out of you're way and investigate the mysterious tower or go further on the path you're on.";
        ChoiceButton1Text = "Go and investigate the mysterious tower";
        ChoiceButton2Text = "Follow the path and ignore the tower";
        ChoiceButton3Text = "";
        SpriteName = "cart";
    }

    public override void Choice1()
    {
        string text =
            "You walk through the hills towards the tower. After a few hours you arrive at the tower, but it is getting dark." +
            "You make a campfire near the entrance of the tower, in the light of the campfire you see that the walls of the tower are covered in strange glyphs." +
            "Suddenly the door flies open and a small figure in a red robe comes out of the tower. " +
            "His hair stands up as if he was electrocuted. \"Where am I?\" He asks excitetly. " +
            "After you tell him the location you're in he screams \"Wrong place!\" and tosses a few gold coins at you." +
            "After a few moments there is a bright flash and the tower is gone. (+3 Gold, -1 Supplies)";

        PlayerStats.Gold += 3;
        if (PlayerStats.Supplies > 0)
        {
            PlayerStats.Supplies -= 1;
        }
        else
        {
            PlayerStats.Health -= 2;
        }
        Card.GameManager.CanvasManager.UpdatePlayerInfo();
        Card.GameManager.CanvasManager.ShowScreenResultFromButtons(text);
    }

    public override void Choice2()
    {
        string text = "You decide to ignore the tower and follow the path.";
        Card.GameManager.CanvasManager.ShowScreenResultFromButtons(text);
    }

    public override void Choice3()
    {
        
    }

    public override void ChanceSucces()
    {
        
    }

    public override void ChanceFailed()
    {
        
    }
}
