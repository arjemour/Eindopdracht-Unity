using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Text _gameOver;
    [SerializeField] private Text _tryAgain;

    private void Start()
    {
        if (PlayerStats.Won)
        {
            _gameOver.text = "You won!";
            _tryAgain.text = "Another round?";
        }
        else
        {
            _gameOver.text = "You lost!";
            _tryAgain.text = "Try another round?";
        }
    }

    public void GoToStartScreen()
    {
        SceneManager.LoadScene(0);
        PlayerStats.Str = 0;
        PlayerStats.Dex = 0;
        PlayerStats.Intel = 0;
        PlayerStats.Won = false;
    }
}
