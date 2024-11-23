using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAndHide : MonoBehaviour
{
    public GameObject objectToMove;
    public Transform ShowPoint;
    public Transform HidePoint;

    public float waitForShow;
    public float waitForHide;

    public float speedShow;
    public float speedHide;

    private float timerShow;
    private float timerHide;

    private float speed;
    private Vector2 targetPoint;

    void Start()
    {
        targetPoint = HidePoint.position; // Inicialmente, el objetivo es HidePoint
        speed = speedHide; // Velocidad inicial para esconderse
        timerHide = 0;
        timerShow = 0;
    }

    void Update()
    {
        // Mover el objeto hacia el punto objetivo
        objectToMove.transform.position = Vector2.MoveTowards(objectToMove.transform.position, targetPoint, speed * Time.deltaTime);

        // Si el objeto llega al punto de HidePoint
        if (Vector2.Distance(objectToMove.transform.position, HidePoint.position) < 0.1f)
        {
            // Incrementar el temporizador para mostrar
            timerShow += Time.deltaTime;

            if (timerShow >= waitForShow && !Locked())
            {
                // Cambiar el objetivo a ShowPoint
                targetPoint = ShowPoint.position;
                speed = speedShow;
                timerShow = 0; // Reiniciar el temporizador
            }
        }
        // Si el objeto llega al punto de ShowPoint
        else if (Vector2.Distance(objectToMove.transform.position, ShowPoint.position) < 0.1f)
        {
            // Incrementar el temporizador para esconder
            timerHide += Time.deltaTime;

            if (timerHide >= waitForHide)
            {
                // Cambiar el objetivo a HidePoint
                targetPoint = HidePoint.position;
                speed = speedHide;
                timerHide = 0; // Reiniciar el temporizador
            }
        }
    }
bool Locked()
    {
        return Physics2D.OverlapBox(transform.position + Vector3.zero, Vector2.one, 0f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + Vector3.zero, Vector2.one);
    }
}
