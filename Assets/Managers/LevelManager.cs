using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Object = UnityEngine.Object;

public class LevelManager
{
    public string[][] Level { get; private set; }
    private List<string> _events;

    public LevelManager()
    {
        _events = new List<string>
        {
            "CartEvent",
            "BanditEvent",
            "InnEvent",
            "RiverEvent",
            "TowerEvent"
        };
    }

    public void LoadLevelFromFile(string levelNumber)
    {
        TextAsset levelText = Resources.Load<TextAsset>("Levels/level-" + levelNumber);
        if (levelText == null)
            return;

        using (StreamReader reader = new StreamReader(new MemoryStream(levelText.bytes)))
        {
            string line1 = reader.ReadLine();
            string line2 = reader.ReadLine();
            string line3 = reader.ReadLine();

            if (line1 == null || line2 == null || line3 == null)
            {
                reader.Close();
                Application.Quit();
            }

            string[] row1 = line1.Split('-');
            string[] row2 = line2.Split('-');
            string[] row3 = line3.Split('-');

            var level = new[] { row1, row2, row3 };
            reader.Close();

            Level = level;
        }
    }

    public void SpawnLevelObjects(out List<Card> cards, out Player player, GameObject gCard, GameObject gPlayer)
    {
        cards = new List<Card>();
        player = null;

        int startX = -690;
        int startY = 340;

        for (int i = 0; i < Level.Length; i++)
        {
            for (int j = 0; j < Level[i].Length; j++)
            {
                switch (Level[i][j])
                {
                    case "C":
                        GameObject card = Object.Instantiate(gCard, new Vector3(startX, startY, 0), Quaternion.identity);
                        Card cardScript = card.GetComponent<Card>();
                        cardScript.CardEvent = Activator.CreateInstance(Type.GetType(_events[UnityEngine.Random.Range(0, _events.Count)]), cardScript) as CardEvent;
                        //cardScript.CardEvent = new TowerEvent();
                        cardScript.CardEvent.Card = cardScript;
                        cardScript.Turned = false;
                        cardScript.XPos = j;
                        cardScript.YPos = i;
                        cards.Add(cardScript);
                        break;
                    case "P":
                        GameObject tempPlayer = Object.Instantiate(gPlayer, new Vector3(startX, startY, -1), Quaternion.identity);
                        Player playerScript = tempPlayer.GetComponent<Player>();
                        playerScript.XPos = j;
                        playerScript.YPos = i;
                        player = playerScript;
                        break;
                }

                startX += 230;
            }

            startX = -690;
            startY -= 340;
        }
    }
}
