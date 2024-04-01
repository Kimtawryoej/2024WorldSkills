using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class AI : Car
{
    protected override void Update()
    {
        base.Update();
        Move();
        
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        Error(() => { });
    }
    protected override void Angle()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, -FindDir(), 0), Time.deltaTime);
    }

    protected override void Move()
    {
        if(Time.timeScale.Equals(1)) { transform.Translate(Vector3.right * inGameSet.CurrentSpeed * Time.deltaTime); }
        //rig.AddForce(transform.right * inGameSet.CurrentSpeed * Time.deltaTime);
    }


}
