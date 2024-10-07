using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
    public float radioBusqueda;
    public LayerMask capaJugador;
    public Transform transformJugador;
    public float velocidadMovimiento;
    public float distanciaMaxima;
    public float distanciaAtaque; // Nueva variable para la distancia de ataque
    public Vector3 puntoInicial;
    public EstadosMovimiento estadoActual;

    public enum EstadosMovimiento
    {
        Esperando,
        Siguiendo,
        Atacando, // Nuevo estado para el ataque
        Volviendo,
    }

    private void Start()
    {
        puntoInicial = transform.position;
    }

    private void Update()
    {
        switch (estadoActual)
        {
            case EstadosMovimiento.Esperando:
                EstadoEsperando();
                break;
            case EstadosMovimiento.Siguiendo:
                EstadoSiguiendo();
                break;
            case EstadosMovimiento.Atacando:
                EstadoAtacando(); // Manejar el comportamiento de ataque
                break;
            case EstadosMovimiento.Volviendo:
                EstadoVolviendo();
                break;
        }
    }

    private void EstadoEsperando()
    {
        Collider2D jugadorCollider = Physics2D.OverlapCircle(transform.position, radioBusqueda, capaJugador);

        if (jugadorCollider)
        {
            transformJugador = jugadorCollider.transform;
            estadoActual = EstadosMovimiento.Siguiendo;
        }
    }

    private void EstadoSiguiendo()
    {
        if (transformJugador == null)
        {
            estadoActual = EstadosMovimiento.Volviendo;
            return;
        }

        transform.position = Vector2.MoveTowards(transform.position, transformJugador.position, velocidadMovimiento * Time.deltaTime);

        if (Vector2.Distance(transform.position, puntoInicial) > distanciaMaxima ||
            Vector2.Distance(transform.position, transformJugador.position) > distanciaMaxima)
        {
            estadoActual = EstadosMovimiento.Volviendo;
            transformJugador = null;
        }
        else if (Vector2.Distance(transform.position, transformJugador.position) <= distanciaAtaque)
        {
            estadoActual = EstadosMovimiento.Atacando;
        }
    }

    private void EstadoAtacando()
    {
        // Después de atacar, se puede volver al estado de seguimiento o cualquier otro estado necesario
        estadoActual = EstadosMovimiento.Siguiendo;
    }

    private void EstadoVolviendo()
    {
        transform.position = Vector2.MoveTowards(transform.position, puntoInicial, velocidadMovimiento * Time.deltaTime);

        if (Vector2.Distance(transform.position, puntoInicial) <= 0.1f)
        {
            estadoActual = EstadosMovimiento.Esperando;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radioBusqueda);
        Gizmos.DrawWireSphere(puntoInicial, distanciaMaxima);
    }
}