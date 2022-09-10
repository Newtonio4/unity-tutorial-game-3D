using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Vector3 moveDirection;

    public float speed = 10;
    public int cost = 10;

    void Start()
    {
        if (transform.position.x > 25)
        {
            moveDirection = Vector3.left;
            transform.Rotate(new Vector3(0, 0, 0));
        }
        else if (transform.position.x < -25)
        {
            moveDirection = Vector3.right;
            transform.Rotate(new Vector3(0, 0, 0));
        }
        else if (transform.position.z > 25)
        {
            moveDirection = Vector3.back;
            transform.Rotate(new Vector3(0, 0, 0));
        }
        else if (transform.position.z < -25)
        {
            moveDirection = Vector3.forward;
            transform.Rotate(new Vector3(0, 0, 0));
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveDirection * Time.deltaTime * speed;

        if (Mathf.Abs(transform.position.x) > 28 || Mathf.Abs(transform.position.z) > 28)
            Destroy(gameObject);
    }
}
