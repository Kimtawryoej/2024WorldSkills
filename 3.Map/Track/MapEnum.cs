using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MapList{Normal,DesertMap,MountainMap,CityMa,Special}
public class MapEnum : MonoBehaviour
{
    [SerializeField] private MapList mapList;
    public MapList MapList => mapList;
}
