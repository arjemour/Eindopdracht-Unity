using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _card;
    [SerializeField] private GameObject _player;
    [SerializeField] private Canvas _canvas;

    public List<Card> Cards { get; private set; }
    public Player Player { get; private set; }
    public LevelManager LevelManager { get; private set; }
    public CanvasManager CanvasManager { get; private set; }
    public EventManager EventManager { get; private set; }

    private void Start()
    {
        Cards = new List<Card>();

        Player tempPlayer;
        List<Card> tempCards;

        LevelManager = new LevelManager();
        LevelManager.LoadLevelFromFile("1");
        LevelManager.SpawnLevelObjects(out tempCards, out tempPlayer, _card, _player);

        Cards = tempCards;
        Player = tempPlayer;

        CanvasManager = _canvas.GetComponent<CanvasManager>();
        EventManager = GetComponent<EventManager>();
        EventManager.GameManager = this;

        Player.CheckAvailableMovement(Cards);
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch t in Input.touches)
            {
                if (t.phase == TouchPhase.Ended)
                {
                    Clicked();
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            Clicked();
        }
    }

    private void Clicked()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Card"))
            {
                Card card = hit.collider.gameObject.GetComponent<Card>();
                if (card.Clickable)
                {
                    StartTurn(card);
                }
            }
        }
    }

    private void StartTurn(Card card)
    {
        foreach (Card c in Cards)
        {
            c.Clickable = false;
            c.enabled = false;
        }

        if (card.transform.GetChild(1) != null)
            Destroy(card.transform.GetChild(1).gameObject);
        
        card.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Cards/" + card.CardEvent.SpriteName);
        card.Turned = true;

        if (PlayerStats.Supplies <= 0 && PlayerStats.Health > 4)
            PlayerStats.Health -= 4;
        else if (PlayerStats.Supplies <= 0 && PlayerStats.Health <= 4)
        {
            GameOver();
            return;
        }
        else
            PlayerStats.Supplies -= 1;

        card.GameManager = this;
        EventManager.Card = card;
        Player.MoveToPosition(card.transform);
        
        CanvasManager.UpdatePlayerInfo();
        CanvasManager.ShowChoiceTextScreen(card.CardEvent);
    }

    public void ContinueCardScreen()
    {
        bool gameCompleted = true;

        foreach (Card c in Cards)
        {
            c.enabled = true;
            if (c.Turned == false)
                gameCompleted = false;
        }

        if (gameCompleted)
        {
            PlayerStats.Won = true;
            SceneManager.LoadScene(2);
        }
        else
            Player.CheckAvailableMovement(Cards);
    }

    public void GameOver()
    {
        PlayerStats.Won = false;
        SceneManager.LoadScene(2);
    }
}