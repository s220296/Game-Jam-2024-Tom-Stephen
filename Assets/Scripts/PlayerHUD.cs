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
    // Start is called before the first frame update
    public void SetPlayerHealth(int health) => _playerHealthDisplay.text = health.ToString();
    public void SetPlayerCurrentAmmo(int ammo, int maxAmmo)
    {
        string ammoString = ammo.ToString(); 
        ammoString += " / "; 
        ammoString += maxAmmo.ToString();

        _playerCurrentAmmoDisplay.text = ammoString;
    }
    public void SetRequiredKillsRemaining(int kills) => _requiredKillsRemaining.text = kills.ToString();
    public void SetLevelTimer(float timer) => _levelTimer.text = timer.ToString();

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
