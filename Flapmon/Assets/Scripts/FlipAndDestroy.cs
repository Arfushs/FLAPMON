using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipAndDestroy : MonoBehaviour
{
    [SerializeField] private float flip_time;
    [SerializeField] private float flip_speed;
    [SerializeField] private GameObject explosionPref;
    [SerializeField] private Transform _rotateObject;
    private float timer;
    private bool isActive;

    private void Start()
    {
        timer = flip_time;

    }

    private void Update()
    {
        
        if (isActive)
        {
            timer -= Time.deltaTime;
            //transform.Rotate(new Vector3(0,2f,0));
            transform.RotateAround(_rotateObject.position,new Vector3(1,0,0),Time.deltaTime * 500);

            if (timer > flip_time / 2)
            {
                transform.position += Time.deltaTime * Vector3.up * flip_speed;
            }
            if (timer < flip_time / 2)
            {
                transform.position += Time.deltaTime * Vector3.down * flip_speed;
            }

            if (timer <= 0)
            {
                Vector3 position = transform.position + new Vector3(0, 1, 0);
                Instantiate(explosionPref, position, Quaternion.identity);
                Destroy(gameObject);
            }
                
        }
            
    }

    public void DestroyObject()
    {
        isActive = true;
    }
    
    
}
