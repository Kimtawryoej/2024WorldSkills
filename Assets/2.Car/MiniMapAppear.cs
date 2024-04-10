using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapAppear : MonoBehaviour
{
    [SerializeField] private GameObject target;

    private void Update()
    {
        transform.rotation = Quaternion.identity;
        transform.position = target.transform.position;
    }
}
