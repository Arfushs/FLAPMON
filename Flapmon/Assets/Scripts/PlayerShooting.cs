using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Fireball Info")]
    [SerializeField] private GameObject fireballPref;
    [SerializeField] private float fireballCooldown =5f;
    [SerializeField] private Transform spawnPos;

    [SerializeField] private Firebar _firebar;
    private float timer;
    private bool canShoot=true;
    private PlayerController _player;
    private void Awake()
    {
        _player = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && canShoot)
        {
            timer = fireballCooldown;
            SpawnFireball();
            _firebar.OnPlayerShoot();
        }
        
        timer -= Time.deltaTime;
        if (timer <= 0)
            canShoot = true;
        else if (timer >0)
            canShoot = false;

    }
    
    private void SpawnFireball()
    {
        GameObject fireball = Instantiate(fireballPref, spawnPos.position, Quaternion.identity);
        Vector3 shootDir = _player.GetPlayerDirection();
        fireball.GetComponent<FireballProjectile>().Setup(shootDir);
    }

    public float GetTimer()
    {
        return timer;
    }

    public float GetCooldown()
    {
        return fireballCooldown;
    }
}
