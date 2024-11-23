using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerhit : MonoBehaviour
{

    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Hit()
    {
        animator.SetTrigger("Hit");
        gameObject.layer = LayerMask.NameToLayer("OnlyGround");
        Destroy(gameObject, 1f);
        GetComponent<Enemigos>().Pausa_movimiento();
    }



}
