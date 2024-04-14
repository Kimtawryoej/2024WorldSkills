using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface MoveMent
{
    public void Move(Action move);
}

public interface Rotation
{
    public Action RotateFunc { get; set; }
    public void Rotate();
}

public interface SpeedControl
{
    public Car car { get; set; }

    public void SpeedChange(float speed, bool switchbool, float time);
}
