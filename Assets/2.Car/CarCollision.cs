using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCollision : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private bool speedEffectbool;
    
    private Car car;

    private void Start()
    {
        car = gameObject.GetComponentInParent<Car>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag($"CollisionObj"))
        {
            StartCoroutine(car.SpeedChange(speed, speedEffectbool, 1));
            car.HitEffect.transform.position = transform.position;
            car.HitEffect.Play();
            Debug.Log(collision.name);
            car.collisionFunc();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag($"ObstacleObj"))
        {
            car.transform.rotation = Quaternion.Euler(new Vector3(0, transform.eulerAngles.y + 40, 0));
            Debug.Log("Dd");
        }
    }
}