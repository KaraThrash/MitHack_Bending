﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level0 : MonoBehaviour
{
    public LevelManager levelManager;
    private bool levelOver = false;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (levelOver)
        {
            return;
        }
        levelManager.OpenDoor();
    }
}