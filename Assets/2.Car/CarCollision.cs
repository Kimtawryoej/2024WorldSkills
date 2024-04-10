using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum CarParts { Front, Back, Side, body };
public class CarCollision : MonoBehaviour
{
    [SerializeField] private CarParts carParts;
    private Car car;
    private void Start()
    {
        car = gameObject.GetComponentInParent<Car>();
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag($"CollisionObj"))
        {
            switch (carParts)
            {
                case CarParts.Front:
                    StartCoroutine(car.SpeedChange(-5, false, 1));
                    Debug.Log("¾Õ");
                    break;
                case CarParts.Back:
                    StartCoroutine(car.SpeedChange(20, true, 1));
                    Debug.Log("µÚ");
                    break;
                case CarParts.Side:
                    StartCoroutine(car.SpeedChange(-5, false, 1));
                    Debug.Log("¿·");
                    break;
                //case CarParts.body:
                //    car.transform.rotation = Quaternion.Euler(new Vector3(0, transform.eulerAngles.y+40, 0));
                //    Debug.Log("¹Ùµð");
                //    break;
            }
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
