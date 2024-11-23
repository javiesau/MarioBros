using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Koopa : MonoBehaviour
{
    public Enemigos enemigos;
    public Animator animator;
    public Rigidbody2D rb2D;

    public virtual void Awake()
    {
        animator = GetComponent<Animator>();
        Debug.Log("awake koopa");
        enemigos = GetComponent<Enemigos>();
        rb2D = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    public virtual void Stomped(Transform player)
    {

    }
    public virtual void Update()
    {

    }

    public virtual void Stompedd()
    {

    }


}
