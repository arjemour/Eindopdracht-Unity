using System.Collections.Generic;
using UnityEngine;

public class BanditEvent : CardEvent
{
    private bool _phase1;
    private bool _phase2;
    private bool _phase3;

    private bool _defend;
    private bool _sneak;
    private bool _run;

    public BanditEvent()
    {
        ChoiceText = "While traveling in the woods you see a wooden cart blocking the road with some people around it." +
                     "You can't make out whats wrong with the cart from this distance. " +
                     "You can go to the cart and offer help or take a closer look.";
        ChoiceButton1Text = "Go to the cart and offer you're help";
        ChoiceButton2Text = "Take a closer look at the cart and the people";
        ChoiceButton3Text = "";
        SpriteName = "cart";

        _phase1 = true;
        _phase2 = false;
        _phase3 = false;
        _defend = false;
        _sneak = false;
        _run = false;
    }

    public override void Choice1()
    {
        if (_phase1)
        {
            ChoiceText =
                "You walk to the people, but just before you're at the cart you hear the sound of swords unsheating, it's a trap!" +
                " \"This is a tax road, you can safely pass for 5 gold pieces.\" Says one of the bandits";
            ChoiceButton1Text = "Draw you're sword and try to defend yourself";
            ChoiceButton2Text = "Pay the \"Tax\" of 5 gold";
            ChoiceButton3Text = "Try to run through the blockade and bandits to safety";
            Card.GameManager.CanvasManager.ShowChoiceTextScreen(this);
            _phase1 = false;
            _phase2 = true;
            _run = true;
        }
        else if (_phase2)
        {
            _defend = true;

            Chances = new List<bool>();
            int successes = 1;

            if (PlayerStats.Str >= 4 && PlayerStats.Str <= 8)
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
        if (_phase1)
        {
            ChoiceText = "On closer inspection you see that the people are wearing swords and the cart doens't seem stuck. " +
                         "They are probally bandits";
            ChoiceButton1Text = "Attack the bandits";
            ChoiceButton2Text = "Try to sneak around the bandits";
            ChoiceButton3Text = "";
            Card.GameManager.CanvasManager.ShowChoiceTextScreen(this);
            _phase1 = false;
            _phase2 = true;
            _sneak = true;
        }
        else if (_phase2 && !_sneak)
        {
            if (PlayerStats.Gold > 5)
            {
                string text = "You decide to pay the \"tax\" and go on with you're journey";
                PlayerStats.Gold -= 5;
                Card.GameManager.CanvasManager.UpdatePlayerInfo();
                Card.GameManager.CanvasManager.ShowScreenResultFromButtons(text);
            }
            else if(PlayerStats.Health > 2)
            {
                string text = "You decide to pay the \"tax\" but you realize you don't have enough money." +
                              "You try to explain this to the bandits but suddenly you get knocked out." +
                              "When you awake all of you're gold and some supplies are gone";
                PlayerStats.Gold = 0;
                PlayerStats.Health -= 2;
                PlayerStats.Supplies -= 3;
                if (PlayerStats.Supplies < 0)
                    PlayerStats.Supplies = 0;
                Card.GameManager.CanvasManager.UpdatePlayerInfo();
                Card.GameManager.CanvasManager.ShowScreenResultFromButtons(text);
            }
            else
            {
                string text = "You decide to pay the \"tax\" but you realize you don't have enough money." +
                              "You try to explain this to the bandits but suddenly you get knocked out." +
                              "You don't wake up";

                PlayerStats.Health = 0;

                Card.GameManager.CanvasManager.UpdatePlayerInfo();
                Card.GameManager.CanvasManager.ShowScreenResultFromButtons(text);
            }
        }
        else if (_phase2 && _sneak)
        {
            Chances = new List<bool>();
            int successes = 1;

            if (PlayerStats.Dex >= 4 && PlayerStats.Dex <= 8)
                successes = 2;
            else if (PlayerStats.Dex >= 9)
                successes = 3;

            for (int i = 0; i < 4; i++)
                Chances.Add(false);

            for (int i = 0; i < successes; i++)
                Chances[i] = true;

            Card.GameManager.CanvasManager.SetCardChances(Chances);
            Chances = ShuffList(Chances);
        }
    }

    public override void Choice3()
    {
        if (_phase2 && _run)
        {
            Chances = new List<bool>();
            int successes = 1;

            if (PlayerStats.Dex >= 4 && PlayerStats.Dex <= 8)
                successes = 2;
            else if (PlayerStats.Dex >= 9)
                successes = 3;

            for (int i = 0; i < 4; i++)
                Chances.Add(false);

            for (int i = 0; i < successes; i++)
                Chances[i] = true;

            Card.GameManager.CanvasManager.SetCardChances(Chances);
            Chances = ShuffList(Chances);
        }
    }

    public override void ChanceSucces()
    {
        string text = "";
        if (_defend)
        {
            if (PlayerStats.Str <= 5 && PlayerStats.Health > 4)
            {
                text = "You draw you're sword and start fighting the three bandits. " +
                       "You parry the attack from the first bandit and try to dodge the attack of the second bandit, " +
                       "but you are to late and his blade cuts into you're arm. " +
                       "You kick the first bandit, he loses his balance and falls head first against the cart. " +
                       "A third bandit attacks you from behind but you realize it to late and are cut in the back. " +
                       "You dodge another attack from the second bandit and cut his neck open. " +
                       "The third bandit, now alone, flees. (-4 Health)";
                PlayerStats.Health -= 4;
            }
            else if (PlayerStats.Str <= 5 && PlayerStats.Health <= 4)
            {
                text = "You draw you're sword and start fighting the three bandits. " +
                       "You parry the attack from the first bandit and dodge the attack of the second bandit." +
                       "You kick the first bandit, he loses his balance and falls head first against the cart. " +
                       "A third bandit attacks you succesfully from behind and knocks you out. You don't wake up";
                PlayerStats.Health = 0;
            }
            else
            {
                text = "You draw you're sword and start fighting the three bandits. " +
                       "You parry the attack from the first bandit and dodge the attack of the second bandit." +
                       "You kick the first bandit, he loses his balance and falls head first against the cart. " +
                       "A third bandit tries to attack you from behind, you dodge his attack, and succesfully counterattack" +
                       "You dodge another attack from the second bandit and cut his neck open. " +
                       "The third bandit, now alone, flees.";
            }
        }
        else if (_sneak)
        {
            text = "You succesfully sneak past the bandits through the woods ";
        }
        else if (_run)
        {
            text = "You succesfully run past the bandits, they try to keep up with you but you are to fast.";
        }

        Card.GameManager.CanvasManager.UpdatePlayerInfo();
        Card.GameManager.CanvasManager.SetCardChancesResult(Chances, text);
    }

    public override void ChanceFailed()
    {
        string text = "";

        if (_defend)
        {
            if (PlayerStats.Health > 7)
            {
                text = "You draw you're sword and start fighting the three bandits. " +
                       "You parry the attack from the first bandit and dodge the attack of the second bandit." +
                       "You kick the first bandit, he loses his balance and falls head first against the cart. " +
                       "A third bandit attacks you succesfully from behind and knocks you out. " +
                       "You wake up with some of you're gold and supplies gone. (-7 Health, -5 Supplies, -10 Gold)";

                PlayerStats.Health -= 7;

                PlayerStats.Supplies -= 5;
                if (PlayerStats.Supplies < 0)
                    PlayerStats.Supplies = 0;

                PlayerStats.Gold -= 10;
                if (PlayerStats.Gold < 0)
                    PlayerStats.Gold = 0;
            }
            else
            {
                text = "You draw you're sword and start fighting the three bandits. " +
                       "You parry the attack from the first bandit and dodge the attack of the second bandit." +
                       "You kick the first bandit, he loses his balance and falls head first against the cart. " +
                       "A third bandit attacks you succesfully from behind and knocks you out. You don't wake up";
                PlayerStats.Health = 0;
            }
        }
        else if (_sneak)
        {
            if (PlayerStats.Health > 3)
            {
                text = "You try to sneak past the bandits, but you make to much noise and they notice you. " +
                       "You try to draw you're sword but you are knocked out. " +
                       "You wake up with some of you're gold and supplies gone. (-4 Health, -5 Supplies, -10 Gold) ";
                PlayerStats.Supplies -= 5;
                if (PlayerStats.Supplies < 0)
                    PlayerStats.Supplies = 0;

                PlayerStats.Gold -= 10;
                if (PlayerStats.Gold < 0)
                    PlayerStats.Gold = 0;
            }
            else
            {
                text = "You try to sneak past the bandits, but you make to much noise and they notice you. " +
                       "You try to draw you're sword but you are knocked out. " +
                       "You don't wake up. ";
                PlayerStats.Health = 0;
            }
        }
        else if (_run)
        {
            if (PlayerStats.Health > 3)
            {
                text = "You try to run past the bandits, but one of them throws a rock and hits you in the head. " +
                       "You fall and are knocked out. " +
                       "You wake up with some of you're gold and supplies gone. (-4 Health, -5 Supplies, -10 Gold) ";
                PlayerStats.Supplies -= 5;
                if (PlayerStats.Supplies < 0)
                    PlayerStats.Supplies = 0;

                PlayerStats.Gold -= 10;
                if (PlayerStats.Gold < 0)
                    PlayerStats.Gold = 0;
            }
            else
            {
                text = "You try to run past the bandits, but one of them throws a rock and hits you in the head. " +
                       "You fall and are knocked out. " +
                       "You don't wake up. ";
                PlayerStats.Health = 0;
            }
        }

        Card.GameManager.CanvasManager.UpdatePlayerInfo();
        Card.GameManager.CanvasManager.SetCardChancesResult(Chances, text);
    }
}