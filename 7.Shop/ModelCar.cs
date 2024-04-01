using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ModelCar : MonoBehaviour
{
    private List<MeshRenderer> wheel = new List<MeshRenderer>();
    public List<MeshRenderer> Wheel => wheel;
    private Parts parts;
    public Parts Parts => parts;
    private void Start()
    {
        wheel = gameObject.GetComponentsInChildren<MeshRenderer>().ToList();
        gameObject.TryGetComponent(out MeshRenderer mesh);
        wheel.Remove(mesh);
        Dele.Instance.Shop = SetImformation;
        Dele.Instance.PartsRead = GetImforMation;
    }


    private void SetImformation(Parts part, Color color)
    {
        foreach (var wheel in wheel)
        {
            wheel.material.color = color;
            parts = part;
        }
    }

    private ModelCar GetImforMation()
    {
        return this;
    }
    void Update()
    {

    }
}
