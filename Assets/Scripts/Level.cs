using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private int _requiredKillsRemaining = 3;

    [SerializeField] private float _goldTime = 10f;
    [SerializeField] private float _silverTime = 20f;
    [SerializeField] private float _bronzeTime = 30f;

    private float _currentTimer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        _currentTimer = 0f;
    }

    public void ReduceKillsRemaining()
    {
        _requiredKillsRemaining--;
    }

    // Update is called once per frame
    void Update()
    {
        _currentTimer += Time.deltaTime;   
    }
}
