using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DataSave
{
    public List<Data<string,float>> Datas = new List<Data<string, float>>();
}

[Serializable]
public class Data<Tkey, Tvalue>
{
    public Tkey Name;
    public Tvalue Score;

    public Data(Tkey name, Tvalue score)
    {
        Name = name;
        Score = score;
    }
}
