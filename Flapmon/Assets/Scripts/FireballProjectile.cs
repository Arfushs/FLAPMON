using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class FireballProjectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed =8f;
    [SerializeField] private GameObject impactPref;
    private Vector3 shootDir;

    public void Setup(Vector3 dir)
    {
        shootDir = dir;
        transform.eulerAngles = new Vector3(0, 0, GetAngelFromVectorFloat(shootDir));
    }

    private void Update()
    {
        transform.position += shootDir * moveSpeed * Time.deltaTime;
    }


    public float GetAngelFromVectorFloat(Vector3 dir) 
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0)
            n += 360;
        return n;
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            SpawnImpact();
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Fence"))
        {
            SpawnImpact();
            other.GetComponent<FlipAndDestroy>().DestroyObject();
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            SpawnImpact();
            other.GetComponent<Enemy>().TakeDamage();
            Destroy(gameObject);
            
        }
        
            
    }

    private void SpawnImpact()
    {
        GameObject impact = Instantiate(impactPref, transform.position, quaternion.identity);
    }
}
