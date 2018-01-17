using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StatSelection : MonoBehaviour
{
    private int _statPoints = 15;

    [SerializeField] private Text _points;
    [SerializeField] private Text _str;
    [SerializeField] private Text _dex;
    [SerializeField] private Text _intel;

    private void Start()
    {
        PlayerStats.Str = 0;
        PlayerStats.Dex = 0;
        PlayerStats.Intel = 0;

        PlayerStats.Health = 15;
        PlayerStats.Supplies = 10;
        PlayerStats.Gold = 20;
    }

    public void AddStrength()
    {
        if (_statPoints > 0 && PlayerStats.Str <= 9)
        {
            _statPoints -= 1;
            PlayerStats.Str += 1;
            _str.text = "Strength: "+ PlayerStats.Str;
            UpdatePointsText();
        }
    }

    public void AddDex()
    {
        if (_statPoints > 0 && PlayerStats.Dex <= 9)
        {
            _statPoints -= 1;
            PlayerStats.Dex += 1;
            _dex.text = "Agility: " + PlayerStats.Dex;
            UpdatePointsText();
        }
    }

    public void AddIntel()
    {
        if (_statPoints > 0 && PlayerStats.Intel <= 9)
        {
            _statPoints -= 1;
            PlayerStats.Intel += 1;
            _intel.text = "Intelligence: " + PlayerStats.Intel;
            UpdatePointsText();
        }
    }

    public void RemoveStrength()
    {
        if (PlayerStats.Str > 0)
        {
            _statPoints += 1;
            PlayerStats.Str -= 1;
            _str.text = "Strength: " + PlayerStats.Str;
            UpdatePointsText();
        }
    }

    public void RemoveDex()
    {
        if (PlayerStats.Dex > 0)
        {
            _statPoints += 1;
            PlayerStats.Dex -= 1;
            _dex.text = "Agility: " + PlayerStats.Dex;
            UpdatePointsText();
        }
    }

    public void RemoveIntel()
    {
        if (PlayerStats.Intel > 0)
        {
            _statPoints += 1;
            PlayerStats.Intel -= 1;
            _intel.text = _intel.text = "Intelligence: " + PlayerStats.Intel;
            UpdatePointsText();
        }
    }

    private void UpdatePointsText()
    {
        _points.text = "Select attributes (" + _statPoints + " points left)";
    }

    public void ContinuePressed()
    {
        if (_statPoints <= 0)
        {
            SceneManager.LoadScene(1);
        }
    }
}
