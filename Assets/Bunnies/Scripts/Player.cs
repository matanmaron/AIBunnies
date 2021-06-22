using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] KeyCode UP;
    [SerializeField] KeyCode DOWN;
    [SerializeField] KeyCode LEFT;
    [SerializeField] KeyCode RIGHT;

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = GetInput();
        transform.position = new Vector3(transform.position.x + (direction.y * speed), transform.position.y, transform.position.z + (direction.x * speed));
    }

    private Vector2 GetInput()
    {
        Vector2 direction = new Vector2();
        if (Input.GetKeyDown(UP))
        {
            direction.x += 1;
        }
        if (Input.GetKeyDown(DOWN))
        {
            direction.x -= 1;
        }
        if (Input.GetKeyDown(LEFT))
        {
            direction.y -= 1;
        }
        if (Input.GetKeyDown(RIGHT))
        {
            direction.y += 1;
        }
        return direction;
    }
}
