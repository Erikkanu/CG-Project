using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySystem : MonoBehaviour
{
    bool lookat = false;
    public GameObject player;
    Rigidbody rb;
    public float speed = 10.0f;
    public float multiplier = 10.0f;
    public float speed_limit = 2;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerDetection.found)
        {
            lookat = true;
        }
        if (lookat)
        {
            transform.LookAt(player.transform);
            Vector3 vel = rb.velocity;

            if (!PlayerDetection.found && vel.x > -2 && vel.x < 2 && vel.z > -2 && vel.z < 2)
            {
                rb.AddForce(speed * multiplier * Time.deltaTime * transform.forward);
            }
        }
    }
}
