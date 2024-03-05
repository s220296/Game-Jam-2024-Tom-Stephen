using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public enum Medal
    {
        NONE = 0
        , BRONZE
        , SILVER
        , GOLD
        , NODISPLAY
    }

    [SerializeField] private int _requiredKillsRemaining = 3;

    [SerializeField] private float _goldTime = 10f;
    [SerializeField] private float _silverTime = 20f;
    [SerializeField] private float _bronzeTime = 30f;

    private float _currentTimer = 0f;
    private PlayerHUD _playerHUD;
    private bool _stopTimer = false;
    public Medal medal { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        _currentTimer = 0f;
        _stopTimer = false;
        medal = Medal.NONE;
        _playerHUD = FindObjectOfType<PlayerHUD>(true);
        if(_playerHUD) _playerHUD.SetRequiredKillsRemaining(_requiredKillsRemaining);
    }

    public void Win()
    {
        _stopTimer = true;

        if (_currentTimer < _goldTime)
            medal = Medal.GOLD;
        else if (_currentTimer < _silverTime)
            medal = Medal.SILVER;
        else if (_currentTimer < _bronzeTime)
            medal = Medal.BRONZE;
        else
            medal = Medal.NONE;

        _playerHUD.SetMedal(medal);
    }

    public void ReduceKillsRemaining()
    {
        _requiredKillsRemaining--;
        if(_playerHUD) _playerHUD.SetRequiredKillsRemaining(_requiredKillsRemaining);
    }

    // Update is called once per frame
    void Update()
    {
        if(!_stopTimer) _currentTimer += Time.deltaTime;
        if(_playerHUD) _playerHUD.SetLevelTimer(_currentTimer);
    }
}
