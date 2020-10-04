﻿using System;
using UnityEngine;
using Utilities;

[RequireComponent(typeof(AudioManager)), RequireComponent(typeof(SceneManagement))]
public class Manager : Singleton<Manager>
{
    [NonSerialized] public AudioManager _audio = null;
    public Room CurrentRoom = null;
    public Camera camera = null;
    
    private void Awake()
    {
        _audio = GetComponent<AudioManager>();
        camera = Camera.main;
    }
}