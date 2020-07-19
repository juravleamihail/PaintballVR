﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndependentRotation : MonoBehaviour
{
    Quaternion rotation;

    private void Awake()
    {
        rotation = this.transform.rotation;
    }

    void Update()
    {
        this.transform.rotation = rotation;
    }
}
