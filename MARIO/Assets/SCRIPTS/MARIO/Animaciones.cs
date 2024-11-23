using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animaciones : MonoBehaviour
{
    Animator animator;
    Colisiones colisiones;
    Mover mover;
    M_colision m_Colision;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        colisiones = GetComponent<Colisiones>();
        mover = GetComponent<Mover>();
        m_Colision = GetComponent<M_colision>();

        if (animator == null)
        {
            Debug.LogError("Animator no asignado en " + gameObject.name);
        }
    }

    public void NewState(int state)
    {
        animator.SetInteger("State", state);
    }

    public void Fire()
    {
        Time.timeScale = 0f;
        animator.SetTrigger("Fire");
    }

    public void PowerUp()
    {
        Time.timeScale = 0f;
        animator.SetTrigger("PowerUp");
    }

    public void Hit()
    {
        Time.timeScale = 0f;
        animator.SetTrigger("Hit");
    }

    public void muerte()
    {
        
        //Debug.Log("Trigger muerte activado");
        if (animator != null)
        {
            animator.SetTrigger("muerte");
        }
        else
        {
            Debug.LogError("Animator es nulo en el método muerte()");
        }
    }
}
