using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chopper : MonoBehaviour
{
    [SerializeField] private GameObject wing;
    [SerializeField] private Color wing_color;
    [SerializeField] private Color chopper_color;
    private Enemy _enemy;
    private Material chopper_mat;
    private Material wing_mat;
    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        chopper_mat = GetComponent<MeshRenderer>().material;
        wing_mat = wing.GetComponent<MeshRenderer>().material;
    }

    private void Update()
    {
        if (_enemy.GetLifeCount() < 2)
        {
            chopper_mat.color = chopper_color;
            wing_mat.color = wing_color;
        }
    }
}
