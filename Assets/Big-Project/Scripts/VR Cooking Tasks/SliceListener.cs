using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceListener : MonoBehaviour
{
    public Slicer slicer;

    private void OnCollisionEnter(Collision other)
    {
        slicer.isTouched = true;
    }
}
