using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallWarp : MonoBehaviour
{
    private float speed = 3.0f;

    /*void Update()
    {
        float moveX = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float moveZ = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        transform.position = new Vector3
        (
            transform.position.x + moveX, transform.position.y, transform.position.z + moveZ
        );
    }*/

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "skip")
        {
            this.transform.position = new Vector3(3.0f, 15.5f, 2.0f);
        }
    }
}