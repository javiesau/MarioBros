using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colisiones : MonoBehaviour
{
    public bool isGrounded;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    M_colision m_Colision;
    Mover mover;

    private void Awake()
    {
        m_Colision = GetComponent<M_colision>();
        mover = GetComponent<Mover>();
    }

    public bool Grounded()
    { 
        if (groundCheck == null)
        {
            Debug.LogError("groundCheck no está asignado en " + gameObject.name);
            return false;
        }
        
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        return isGrounded;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("enemigo"))
        {
            if (m_Colision != null)
                m_Colision.Hit();
            else
                Debug.LogError("m_Colision no está asignado en " + gameObject.name);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*  Playerhit playerhit = collision.GetComponent<Playerhit>();
          if (playerhit != null)
          //{
          //    playerhit.Hit();
        //      mover.BounceUp();
          }*/
       SCR_Koopa Enemy = collision.GetComponent<SCR_Koopa>();
          if (Enemy!= null)
          {
             Enemy.Stomped(transform);
             mover.BounceUp();
          }
    }

    public void muerte()
    {
        gameObject.layer = LayerMask.NameToLayer("muerto");
    }
}
