using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{

    [SerializeField] protected float speed;
    protected virtual void RotCamera(Vector3 target)
    {
        transform.LookAt(target);
    }
    protected virtual void CameraMove(Vector3 target)
    {
        transform.position = Vector3.Lerp(transform.position, target, Time.unscaledDeltaTime*speed);
    }
}
