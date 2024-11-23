using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigos : MonoBehaviour
{

    public float speed = 1f;
    Rigidbody2D rb2d;
    SpriteRenderer spriteRenderer;
    Vector2 lastVelocity;

    bool pausa_mov;

    Vector2 currentDirection;
    float defaultSpeed;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = rb2d.GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
        defaultSpeed = MathF.Abs(speed);
    }

    private void FixedUpdate()
    {
        if (!pausa_mov)
        {
            if (rb2d.velocity.x > -0.1F && rb2d.velocity.x < 0.1f)
             {
               speed = -speed;
             }
            rb2d.velocity = new Vector2(speed, rb2d.velocity.y);

            if(rb2d.velocity.x > 0)
            {
               spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
        }
             
    }

    public void Pausa_movimiento()
    {
        if(!pausa_mov)
        {
            currentDirection = rb2d.velocity.normalized;
            lastVelocity = rb2d.velocity;
            pausa_mov = true;
            rb2d.velocity = new Vector2(0, 0);
        }
    }

    public void ContinueMovement()
    {
        if(pausa_mov)
        {
            speed = defaultSpeed * currentDirection.x;
            rb2d.velocity = new Vector2(speed, lastVelocity.y);
            pausa_mov = false;
        }
    }

    public void continueMovement(Vector2 newVelocity)
    {
        if(pausa_mov)
        {
            rb2d.velocity = newVelocity;
            pausa_mov = false;
        }
    }

}
