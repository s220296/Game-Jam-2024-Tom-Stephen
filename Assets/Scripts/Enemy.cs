using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(TriggerObject))]
public class Enemy : MonoBehaviour
{
    private TriggerObject _trigger;
    [SerializeField] private int _health = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        _trigger = GetComponent<TriggerObject>();
    }

    public void Damage(int damage)
    {
        _health -= damage;
        if (_health <= 0) Kill();
    }

    public void Kill()
    {
        _trigger.CallTrigger();

        Level level = FindObjectOfType<Level>();
        if (level) level.ReduceKillsRemaining();

        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
