using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnEvent : CardEvent
{
    private bool _phase1;
    private bool _phase2;
    private bool _phase3;

    public InnEvent()
    {
        ChoiceText = "You come across an old inn. The sign outside has a picture of an filled mug, but the words below it have faded away.";
        ChoiceButton1Text = "Go inside the inn";
        ChoiceButton2Text = "Go on with you're journey";
        ChoiceButton3Text = "";
        SpriteName = "inn";

        _phase1 = true;
        _phase2 = false;
        _phase3 = false;
    }

    public override void Choice1()
    {
        if (_phase1)
        {
            ChoiceText = "You go inside the inn, aside from the innkeeper there is no one else.";
            ChoiceButton1Text = "Go to the innkeeper and ask if he sells supplies and/or if there is a room available";
            ChoiceButton2Text = "Leave the inn and go on with you're journey";
            ChoiceButton3Text = "";
            Card.GameManager.CanvasManager.ShowChoiceTextScreen(this);
            _phase1 = false;
            _phase2 = true;
        }
        else if (_phase2)
        {
            ChoiceText = "You talk to the innkeeper, he has a room available and some supplies to sell.";
            ChoiceButton1Text = "Buy some supplies (-2 gold, +1 supplies)";
            ChoiceButton2Text = "Rent the room for the night (-10 gold)";
            ChoiceButton3Text = "Leave the inn and go on with you're journey";
            Card.GameManager.CanvasManager.ShowChoiceTextScreen(this);
            _phase2 = false;
            _phase3 = true;
        }
        else if (_phase3)
        {
            if (PlayerStats.Gold >= 2)
            {
                ChoiceText = "You buy some supplies (-2 gold, +1 supplies).";
                PlayerStats.Gold -= 2;
                PlayerStats.Supplies += 1;
            }
            else
                ChoiceText = "You don't have enough gold to buy supplies.";

            Card.GameManager.CanvasManager.UpdatePlayerInfo();
            Card.GameManager.CanvasManager.ShowChoiceTextScreen(this);
        }
    }

    public override void Choice2()
    {
        if (_phase1)
        {
            string text = "You decide to not go inside the inn and go on with you're journey"; 
            Card.GameManager.CanvasManager.ShowScreenResultFromButtons(text);
        }
        else if (_phase2)
        {
            string text = "You decide to leave the inn and go on with you're journey";
            Card.GameManager.CanvasManager.ShowScreenResultFromButtons(text);
        }
        else if (_phase3)
        {
            if (PlayerStats.Gold >= 10)
            {
                string text = "You decide to rent the room and spend the night in the inn instead of outside by a campfire. (-10 gold, +5 Health)";
                PlayerStats.Gold -= 10;
                PlayerStats.Health += 5;
                Card.GameManager.CanvasManager.UpdatePlayerInfo();
                Card.GameManager.CanvasManager.ShowScreenResultFromButtons(text);
            }
            else
            {
                ChoiceText = "You don't have enough gold to rent a room.";
                Card.GameManager.CanvasManager.ShowChoiceTextScreen(this);
            }
        }
    }

    public override void Choice3()
    {
        if (_phase3)
        {
            string text = "You decide to leave the inn and go on with you're journey";
            Card.GameManager.CanvasManager.ShowScreenResultFromButtons(text);
        }
    }

    public override void ChanceSucces()
    {
        
    }

    public override void ChanceFailed()
    {
        
    }
}
