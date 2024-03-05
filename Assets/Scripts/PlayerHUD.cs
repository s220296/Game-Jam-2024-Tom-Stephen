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

    [SerializeField] private string _formatLevelTimer = "F3";
    // Start is called before the first frame update
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
