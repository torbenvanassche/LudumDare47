﻿using System;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(CapsuleCollider))]
public class Player : MonoBehaviour
{
    public static InputSettings Controls;

    [HideInInspector] public Rigidbody rig = null;
    [HideInInspector] public new CapsuleCollider collider = null;
    [HideInInspector] public new SpriteRenderer renderer = null;

    public GameObject shadow;

    void Awake()
    {
        rig = GetComponent<Rigidbody>();
        Controls = new InputSettings();
        collider = GetComponent<CapsuleCollider>();
        renderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        Controls.Enable();
    }

    private void OnDisable()
    {
        Controls.Disable();
    }
}
