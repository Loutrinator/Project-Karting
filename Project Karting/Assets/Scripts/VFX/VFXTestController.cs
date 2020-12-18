﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXTestController : MonoBehaviour
{
    public ShakeTransform camera;
    public Transform effectPosition;

    public GameObject[] effects;

    public string[] keys;
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < keys.Length; i++)
        {
            GameObject g;
            if (Input.GetKeyDown(keys[i]))
            {
                g = Instantiate(effects[i], effectPosition.position,Quaternion.identity);
                if (i == 1)
                {
                    NukeBomb bomb = g.GetComponent<NukeBomb>();
                    bomb.target = effectPosition;
                    bomb.transform.position = bomb.startPosition;
                    bomb.camera = camera;
                }
            }
        }
    }
}