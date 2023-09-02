using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnAround : MonoBehaviour
{
    [SerializeField] private float turnSpeed =0.5f;

    private void Update()
    {
        transform.Rotate(new Vector3(0,turnSpeed,0));
    }
}
