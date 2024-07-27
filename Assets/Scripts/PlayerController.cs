using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float torqueAmount = 4000f;
    [SerializeField] float boostSpeed = 35f;
    [SerializeField] float normalSpeed = 20f;
    [SerializeField] float lowSpeed = 10f;
    Rigidbody2D rb2d;
    [SerializeField] float jumpAmount = 20;
    bool canMove = true;
    private SurfaceEffector2D surfaceEffector2D;
    private CapsuleCollider2D snowBoardCollider;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        surfaceEffector2D = FindObjectOfType<SurfaceEffector2D>();
        snowBoardCollider = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        if (canMove)
        {
            RotatePlayer();
            RespondToBoost();
            Jump();
        }
        else
        {
            DisableControls();
        }
    }

    void RotatePlayer()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb2d.AddTorque(torqueAmount * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rb2d.AddTorque(-torqueAmount * Time.deltaTime);
        }
    }

    public void DisableControls()
    {
        canMove = false;
    }

    void RespondToBoost()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            surfaceEffector2D.speed = boostSpeed;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            surfaceEffector2D.speed = lowSpeed;
        }
        else
        {
            surfaceEffector2D.speed = normalSpeed;
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (snowBoardCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
            {
                rb2d.AddForce(Vector2.up * jumpAmount, ForceMode2D.Impulse);
            }
        }
    }
}