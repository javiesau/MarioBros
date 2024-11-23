using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SRC_Goomba : SCR_Koopa
{
    public override void Awake()
    {
        base.Awake();
        Debug.Log("Awake de goomba");
    }

    public override void Stomped(Transform player)
    {
        animator.SetTrigger("Hit");
        gameObject.layer = LayerMask.NameToLayer("OnlyGround");
        Destroy(gameObject, 1f);
        GetComponent<Enemigos>().Pausa_movimiento();
    }
}
