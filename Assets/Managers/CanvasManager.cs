using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public Animator Animator { get; private set; }

    [SerializeField] private Text _choiceText;
    [SerializeField] private Image _cardImage;
    [SerializeField] private Button _choice1Button;
    [SerializeField] private Button _choice2Button;
    [SerializeField] private Button _choice3Button;

    [SerializeField] private Button _cardChance1;
    [SerializeField] private Button _cardChance2;
    [SerializeField] private Button _cardChance3;
    [SerializeField] private Button _cardChance4;
    [SerializeField] private Image _cardChance1Image;
    [SerializeField] private Image _cardChance2Image;
    [SerializeField] private Image _cardChance3Image;
    [SerializeField] private Image _cardChance4Image;
    [SerializeField] private Sprite _cardSucces;
    [SerializeField] private Sprite _cardFailure;
    [SerializeField] private Sprite _cardBack;

    [SerializeField] private Text _resultText;

    [SerializeField] private Text _health;
    [SerializeField] private Text _supplies;
    [SerializeField] private Text _gold;

    public List<bool> Chances { get; set; }

    private void Start ()
    {
        Animator = GetComponent<Animator>();
    }

    public void ShowChoiceTextScreen(CardEvent cardEvent)
    {
        _choiceText.text = cardEvent.ChoiceText;
        _cardImage.sprite = Resources.Load<Sprite>("Cards/" + cardEvent.SpriteName);
        Animator.SetTrigger("ShowChoiceTextScreen");
    }

    public void ShowChoiceButtonScreen(CardEvent cardEvent)
    {
        _choice1Button.GetComponentInChildren<Text>().text = cardEvent.ChoiceButton1Text;
        _choice2Button.GetComponentInChildren<Text>().text = cardEvent.ChoiceButton2Text;
        _choice3Button.GetComponentInChildren<Text>().text = cardEvent.ChoiceButton3Text;
        Animator.SetTrigger("ShowChoiceButtonsScreen");
    }

    public void SetCardChances(List<bool> chances)
    {
        _cardChance1.GetComponent<Image>().sprite = chances[0] ? _cardSucces : _cardFailure;
        _cardChance2.GetComponent<Image>().sprite = chances[1] ? _cardSucces : _cardFailure;
        _cardChance3.GetComponent<Image>().sprite = chances[2] ? _cardSucces : _cardFailure;
        _cardChance4.GetComponent<Image>().sprite = chances[3] ? _cardSucces : _cardFailure;
        Animator.SetTrigger("ShowChanceScreen");
    }

    public void SetCardChanceBack()
    {
        _cardChance1.GetComponent<Image>().sprite = _cardBack;
        _cardChance2.GetComponent<Image>().sprite = _cardBack;
        _cardChance3.GetComponent<Image>().sprite = _cardBack;
        _cardChance4.GetComponent<Image>().sprite = _cardBack;
    }

    public void SetCardChancesResult(List<bool> chances, string text)
    {
        _resultText.text = text;
        _cardChance1Image.sprite = chances[0] ? _cardSucces : _cardFailure;
        _cardChance2Image.sprite = chances[1] ? _cardSucces : _cardFailure;
        _cardChance3Image.sprite = chances[2] ? _cardSucces : _cardFailure;
        _cardChance4Image.sprite = chances[3] ? _cardSucces : _cardFailure;
        Animator.SetTrigger("ShowChanceResult");
    }

    public void ShowScreenResultFromButtons(string text)
    {
        _resultText.text = text;
        Animator.SetTrigger("ShowResultScreenFromButton");
    }

    public void UpdatePlayerInfo()
    {
        _health.text = PlayerStats.Health.ToString();
        _supplies.text = PlayerStats.Supplies.ToString();
        _gold.text = PlayerStats.Gold.ToString();
    }
}
