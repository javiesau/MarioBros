using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class koopa : SCR_Koopa
{
    bool isHidden;
    public float maxStopedTime;
    float StopedTimer;
    public float rollingSpeed;

    public override void Update()
    {
           base.Update();
        if (isHidden && rb2D.velocity.x == 0f)
        {
            StopedTimer = StopedTimer += Time.deltaTime;
            if (StopedTimer >= maxStopedTime )
            {
                ResetMove();
            }
        }
    }

    public override void Stomped(Transform player)
    {
        if (!isHidden)
        {
            isHidden = true;
            animator.SetBool("Hidden", isHidden);
            GetComponent<Enemigos>().Pausa_movimiento();
             
        }
        else
        {
            if(Mathf.Abs(rb2D.velocity.x) > 0f)
            {
                enemigos.Pausa_movimiento();
            }
            else
            {
             if(player.position.x < transform.position.x)
                        {
                            enemigos.speed = rollingSpeed;
                        }

                        else
                        {
                            enemigos.speed = -rollingSpeed;
                        }
                        enemigos.continueMovement(new Vector2(enemigos.speed, 0f));
            }

           
        }

        gameObject.layer = LayerMask.NameToLayer("OnlyGround");
        Invoke("ResetLayer", 0.1f);
        StopedTimer = 0f;
    }

    void ResetLayer()
    {
        gameObject.layer = LayerMask.NameToLayer("enemigo");
    }
    void ResetMove()
    {
        enemigos.ContinueMovement();
        isHidden = false;
        animator.SetBool("Hidden", isHidden);
        StopedTimer = 0;
    }
}
