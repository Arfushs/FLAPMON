using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    [SerializeField] private bool isRotating = true;
    [SerializeField] private float rotateSpeed = .5f;
    [SerializeField] private float rise_speed = 3f;
    [SerializeField] private float life_time = 5f;

    private float life_timer;
    private bool isRising = false;
    private BoxCollider _boxCollider;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
    }

    void Update()
    {
        if(isRotating)
            transform.Rotate(new Vector3(rotateSpeed,0,0));

        if (isRising)
        {
            transform.position += new Vector3(0, rise_speed, 0) * Time.deltaTime;
            life_timer -= Time.deltaTime;
            if(life_timer<0)
                Destroy(gameObject);
        }
            
    }

    public void CloseRotation()
    {
        isRotating = false;
    }

    public void Rise()
    {
        life_timer = life_time;
        isRising = true;
        isRotating = true;
    }

    public void CloseCollider() => _boxCollider.enabled = false;
}
