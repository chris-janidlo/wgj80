using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;

public class DroneMovement : Singleton<DroneMovement>
{
    public Vector3 Position => transform.position;
    
    public float MoveSpeed;
    public float JumpForce;

    public LayerMask GroundLayers;
    public float HalfHeight;
    public Vector3 GroundCheckHalfExtents;

    Rigidbody rb;
    bool canJump = true;

    void Awake ()
    {
        SingletonSetInstance(this, true);
        rb = GetComponent<Rigidbody>();
    }

    void Update ()
    {
        Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized * MoveSpeed;

        movement = transform.TransformDirection(movement);

        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);

        if (grounded())
        {
            if (canJump && Input.GetButton("Jump"))
            {
                jump();
            }
        }
        else
        {
            canJump = true;
        }
    }

    bool grounded ()
    {
        // add half height because model's origin is at feet
        return Physics.BoxCast(transform.position + Vector3.up * HalfHeight, GroundCheckHalfExtents, Vector3.down, transform.rotation, HalfHeight, GroundLayers, QueryTriggerInteraction.Ignore);
    }

    void jump ()
    {
        rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        canJump = false;
    }
}
