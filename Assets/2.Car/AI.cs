using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using System;
using UnityEngine;

public class AI : Car
{
    private void Start()
    {
        rotation.RotateFunc = () =>
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, -FindDir(), 0), Time.deltaTime);
        };
    }
    protected override void Update()
    {
        base.Update();
        moveMent.Move((() =>
        {
            if (Time.timeScale.Equals(1)) { transform.Translate(Vector3.right * inGameSet.CurrentSpeed * Time.deltaTime); }
        }));
        

    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        RayCastAction(() => { });
    }

}
