using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    enum Direction { Left = -1, None = 0, Right = 1 };
    Direction CurrentDirection = Direction.None;
    Direction LastDirection = Direction.None;

    public float speed = 5f;
    public Rigidbody2D rb2D;
    public Colisiones colisiones;
    public Animator animator;

    public float maxVelocity = 5f;
    public float acceleration = 10f;
    public float friction = 5f; // Fricci�n ajustable para el deslizamiento

    public float brinco;
    public bool isJumping;
    private float defaultGravity;
    private bool skidActive = false; // Control interno del estado de skid

    public float raycastDistance = 0.1f;

    public bool inputmove = true;

    // Variable de tiempo para controlar cu�nto dura el skid
    public float skidTime = 0.5f; // Tiempo que el skid estar� activo
    private float skidTimer = 0f; // Temporizador interno para el skid

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        colisiones = GetComponent<Colisiones>();
        animator = GetComponent<Animator>();

        if (colisiones == null)
        {
            Debug.LogError("Colisiones no est� asignado en " + gameObject.name);
        }
    }

    void Start()
    {
        defaultGravity = rb2D.gravityScale;
    }

    void Update()
    {
        // Verificamos si el personaje est� en el suelo usando Colisiones.Grounded()
        bool grounded = colisiones != null && colisiones.Grounded();
        animator.SetBool("Grounded", grounded);

        // Detectar la direcci�n del movimiento
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            CurrentDirection = Direction.Left;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            CurrentDirection = Direction.Right;
        }
        else
        {
            CurrentDirection = Direction.None;
        }

        // Detectar si hay cambio de direcci�n con velocidad
        if (!skidActive && CurrentDirection != Direction.None && CurrentDirection != LastDirection && Mathf.Abs(rb2D.velocity.x) > 0.1f)
        {
            StartSkid(); // Iniciar skid al detectar cambio de direcci�n
        }

        // Si el skid est� activo, reducimos el temporizador
        if (skidActive)
        {
            skidTimer -= Time.deltaTime;

            // Desactivamos el skid despu�s del tiempo definido
            if (skidTimer <= 0f)
            {
                EndSkid(); // Finalizar el skid despu�s de que se haya cumplido el tiempo
            }
        }

        // Control del salto
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (grounded)
            {
                Jump();
                isJumping = true;
                animator.SetBool("Jump", true);
            }
        }

        if (isJumping)
        {
            if (rb2D.velocity.y < 0 && grounded)
            {
                isJumping = false;
                animator.SetBool("Jump", false);
            }
        }

        // Pasar la velocidad al Animator
        float velocidad = Mathf.Abs(rb2D.velocity.x);
        animator.SetFloat("Velocidad", velocidad);

        // Animaci�n de idle
        animator.SetBool("Grounded", grounded && !isJumping && velocidad <= 0.1f);

        // Voltear el sprite seg�n la direcci�n
        GetComponent<SpriteRenderer>().flipX = (CurrentDirection == Direction.Left);

        // Actualizamos la �ltima direcci�n
        LastDirection = CurrentDirection;
    }

    private void FixedUpdate()
    {
        // Deslizamiento de fricci�n cuando se cambia de direcci�n
        if (Mathf.Abs(rb2D.velocity.x) > 0f && CurrentDirection != Direction.None && CurrentDirection != LastDirection)
        {
            // Aplica la fricci�n en la direcci�n opuesta a la que se mueve
            rb2D.velocity = new Vector2(Mathf.Lerp(rb2D.velocity.x, 0, friction * Time.fixedDeltaTime), rb2D.velocity.y);
        }
        else
        {
            // Movimiento horizontal regular
            float move = (int)CurrentDirection * speed;
            rb2D.velocity = new Vector2(move, rb2D.velocity.y);
        }
    }

    void Jump()
    {
        if (colisiones != null && colisiones.Grounded())
        {
            rb2D.AddForce(new Vector2(0, brinco), ForceMode2D.Impulse);
        }
    }

    // M�todo para comenzar la animaci�n del skid
    private void StartSkid()
    {
        skidActive = true; // Activamos el skid
        animator.SetBool("Skid", true); // Activamos el bool "Skid"
        animator.SetTrigger("Skid 1"); // Activamos el trigger "Skid 1"
        skidTimer = skidTime; // Restablecemos el temporizador para el skid

        // Invertimos la direcci�n del sprite del skid al lado contrario del movimiento
        if (CurrentDirection == Direction.Left)
        {
            // Si el personaje va a la izquierda, el skid debe mirar a la derecha
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (CurrentDirection == Direction.Right)
        {
            // Si el personaje va a la derecha, el skid debe mirar a la izquierda
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    // M�todo para finalizar el skid
    private void EndSkid()
    {
        animator.SetBool("Skid", false); // Desactivamos el bool "Skid"
        skidActive = false; // Desactivamos el skid
    }

    // M�todo para finalizar la animaci�n de muerte
    public void muerte()
    {
        inputmove = false;
        rb2D.velocity = Vector2.zero;
        rb2D.gravityScale = 1;
        rb2D.AddForce(Vector2.up * 5f, ForceMode2D.Impulse);
    }

    // M�todo para el rebote del personaje
    public void BounceUp()
    {
        rb2D.velocity = Vector2.zero;
        rb2D.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
    }
}
