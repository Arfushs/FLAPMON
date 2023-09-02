using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private GameObject _explosionPref;
    [SerializeField] private GameManager _gameManager;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
            Death();
    }

    private void Death()
    {
        Vector3 position = transform.position + new Vector3(0, 1, 0);
        Instantiate(_explosionPref, position, quaternion.identity);
        GameManager.RestartGame();
        Destroy(gameObject);
        
    }

    
}
