using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCamera : MainCamera
{
    [SerializeField] private GameObject[] moveTarget = new GameObject[2];
    
    private void Update()
    {
        CameraMove(moveTarget[0].transform.position);
        RotCamera(moveTarget[1].transform.position);
    }
}
