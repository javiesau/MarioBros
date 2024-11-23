using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class M_colision : MonoBehaviour
{
    enum State { Default = 0, Super=1, Fire=2}
    State currentState = State.Default;
    public GameObject stompBox;
    Mover mover;
    Colisiones mcolisiones;
    Animaciones animaciones;
    Rigidbody2D rb2D;

    bool isDead;

    private void Awake()
    {
        mover = GetComponent<Mover>();  
        mcolisiones = GetComponent<Colisiones>();
        animaciones = GetComponent<Animaciones>();
        rb2D = GetComponent<Rigidbody2D>();

        if (animaciones == null)
        {
            Debug.LogError("Animaciones no está asignado en " + gameObject.name);
        }

    }

    public void Update()
    {
        if (rb2D.velocity.y < 0 && !isDead)
        {
            stompBox.SetActive(true); 
        }
        else
        {
            stompBox.SetActive(false);
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            animaciones.PowerUp();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            animaciones.Fire();
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            Hit();
        }
    }
    // Start is called before the first frame update
    public void Hit()
    {
        if(currentState == State.Default)
        {
            muerte();
        }
        else
        {
            animaciones.Hit();
        }  
    }
    public void muerte()
    {
        if(!isDead)
        {
         mover.inputmove = false;
                mcolisiones.muerte();
                mover.muerte();
                animaciones.muerte();
        }
    }

    void ChangeState(int newstate)
    {
        currentState = (State)newstate;
        animaciones.NewState(newstate);
        Time.timeScale = 1f;
    }
}
