using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private int _maxAmmo = 16;
    [SerializeField] private float _fireRate = 0.1f;
    
    private int _currentAmmo = 0;
    private float _fireTimer = 0f;

    private PlayerHUD _playerHUD;
    private AudioSource _gunAudio;

    // Start is called before the first frame update
    void Start()
    {
        _currentAmmo = _maxAmmo;
        _playerHUD = FindObjectOfType<PlayerHUD>(true);
        _gunAudio = GetComponent<AudioSource>();
        if(_playerHUD) _playerHUD.SetPlayerCurrentAmmo(_currentAmmo, _maxAmmo);
    }

    public void Shoot(Vector2 reticlePos)
    {
        if (_fireTimer > 0f) return;
        _fireTimer = _fireRate;

        if (_currentAmmo <= 0) return;
        _currentAmmo--;

        // Set ammo counter in hud
        if (_playerHUD) _playerHUD.SetPlayerCurrentAmmo(_currentAmmo, _maxAmmo);
        if (_gunAudio) _gunAudio.Play();

        // Cast ray for shot hit detection
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(reticlePos.x, reticlePos.y, 0));

        bool rayHit = Physics.Raycast(ray, out RaycastHit hit, 200f);
        if (!rayHit) return;

        // Detect if hit enemy
        Enemy enemy = hit.transform.GetComponent<Enemy>();
        if (enemy) enemy.Damage(1);
    }

    public void Reload()
    {
        _currentAmmo = _maxAmmo;
        if(_playerHUD) _playerHUD.SetPlayerCurrentAmmo(_currentAmmo, _maxAmmo);
    }

    // Update is called once per frame
    void Update()
    {
        _fireTimer -= Time.deltaTime;
    }
}
