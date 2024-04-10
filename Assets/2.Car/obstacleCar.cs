using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody))]
public class obstacleCar : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float speed;
    [SerializeField] float yAngle;
    [SerializeField] float time;
    private Vector3 strPos;
    private void OnDrawGizmos()
    {
        transform.rotation = Quaternion.Euler(0, yAngle, 0);
    }
    private void Start()
    {
        strPos = transform.position;
        StartCoroutine(Move());
    }
    private void FixedUpdate()
    {
        transform.Translate(Vector3.right * speed);
        //rb.AddForce(, ForceMode.Acceleration);
    }

    private IEnumerator Move()
    {
        WaitForSeconds wait = new WaitForSeconds(time);
        while (true)
        {
            yield return wait;
            transform.rotation = Quaternion.Euler(0, yAngle, 0);
            transform.position = strPos;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Car car))
        {
            car.ReSpawn();
        }
    }
}
