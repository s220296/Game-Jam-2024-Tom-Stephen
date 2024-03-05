using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _playerHealthDisplay;
    [SerializeField] private TextMeshProUGUI _playerCurrentAmmoDisplay;
    [SerializeField] private TextMeshProUGUI _requiredKillsRemaining;
    [SerializeField] private TextMeshProUGUI _levelTimer;
    [SerializeField] private TextMeshProUGUI _medalDisplay;

    [SerializeField] private string _formatLevelTimer = "F3";
    // Start is called before the first frame update
    public void SetMedal(Level.Medal medal)
    {
        if (!_medalDisplay) return;

        switch(medal)
        {
            case Level.Medal.NONE:
                _medalDisplay.text = "NONE";
                _medalDisplay.color = Color.gray;
                break;
            case Level.Medal.BRONZE:
                _medalDisplay.text = "BRONZE";
                _medalDisplay.color = new Color(0.6f, 0.25f, 0, 1);
                break;
            case Level.Medal.SILVER:
                _medalDisplay.text = "SILVER";
                _medalDisplay.color = new Color(0.8f, 0.8f, 0.8f, 1);
                break;
            case Level.Medal.GOLD:
                _medalDisplay.text = "GOLD";
                _medalDisplay.color = new Color(1f, 0.9f, 0, 1);
                break;
            default:
                _medalDisplay.text = "";
                _medalDisplay.color = Color.clear;
                break;

        }
    }

    public void SetPlayerHealth(int health)
    {
        if(_playerHealthDisplay)
            _playerHealthDisplay.text = health.ToString();
    }
    public void SetPlayerCurrentAmmo(int ammo, int maxAmmo)
    {
        string ammoString = ammo.ToString(); 
        ammoString += " / "; 
        ammoString += maxAmmo.ToString();

        if(_playerCurrentAmmoDisplay)
            _playerCurrentAmmoDisplay.text = ammoString;
    }
    public void SetRequiredKillsRemaining(int kills)
    {
        if(_requiredKillsRemaining)
            _requiredKillsRemaining.text = kills.ToString();
    }
    public void SetLevelTimer(float timer)
    {
        if (_levelTimer)
            _levelTimer.text = timer.ToString(_formatLevelTimer);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
