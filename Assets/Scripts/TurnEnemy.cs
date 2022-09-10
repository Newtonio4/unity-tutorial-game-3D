using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnEnemy : MonoBehaviour
{
    

    void Start()
    {
        if (transform.position.z > 25)
            transform.Rotate(new Vector3(0, 180, 0));
        else if (transform.position.x > 25)
            transform.Rotate(new Vector3(0, -90, 0));
        else if (transform.position.x < -25)
            transform.Rotate(new Vector3(0, 90, 0));
    }

    void Update()
    {
        
    }
}
