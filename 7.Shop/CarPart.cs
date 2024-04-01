using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPart : MonoBehaviour
{
    [SerializeField] private Parts part;
    public Parts Part => part;
}
