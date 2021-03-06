﻿using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour
{
    void Update()
    {
        // Move the object forward along its z axis 1 unit/second.
       // transform.Translate(0, 0, Time.deltaTime);

        // Move the object upward in world space 1 unit/second.
        transform.Translate(0, Time.deltaTime * -3, 0, Space.World);
    }
}

