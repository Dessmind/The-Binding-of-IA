using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeEnemy : MonoBehaviour
{
    protected Rigidbody2D rb = null;

    private Vector2 TargetPos = Vector2.zero;

    private bool isFleeing = false;
    private float fleeTime = 0.0f;

    private float timeToShoot = 3.0f;
    private float lastBullet;
    private bool isShooting = false;

    [Space(3)]
    [Header("Bullet Values")]
    [SerializeField] private float bulletDelay = 0.2f;
    [SerializeField] private float bulletSpeed = 3.0f;
    public GameObject bulletPrefab;

    public GameObject targetGameObject; // Asegúrate de asignar esto en el inspector
    public float maxAcceleration = 5.0f; // Ajusta según sea necesario
    public float maxSpeed = 10.0f; // Ajusta según sea necesario

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (targetGameObject == null)
        {
            return;
        }

        FleeEnemyLogic();
    }

    void FleeEnemyLogic()
    {
        if (targetGameObject == null)
        {
            return;
        }

        Vector2 PosToTarget = -PuntaMenosCola(targetGameObject.transform.position, transform.position);

        // Solo aplica fuerza de velocidad si no está disparando
        if (!isShooting)
        {
            rb.AddForce(PosToTarget.normalized * maxAcceleration, ForceMode2D.Force);
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
        }

        // Iniciar lógica para escapar
        if (!isFleeing)
        {
            isFleeing = true;
        }

        fleeTime += Time.deltaTime;

        // Si pasaron 3 segundos desde que huye, dispara
        if (fleeTime >= timeToShoot)
        {
            // Detener el movimiento antes de disparar
            rb.velocity = Vector2.zero;

            if (Time.time > lastBullet + bulletDelay)
            {
                isShooting = true;
                Shooting(-PosToTarget.x, -PosToTarget.y);
                lastBullet = Time.time;

                // Reiniciar el tiempo de escape y marcar como no disparando después del disparo
                fleeTime = 0.0f;
                isShooting = false;
            }
        }
    }

    void Shooting(float x, float y)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
        bullet.AddComponent<Rigidbody2D>().gravityScale = 0;

        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(
            (x < 0) ? Mathf.Floor(x) * bulletSpeed : Mathf.Ceil(x) * bulletSpeed,
            (y < 0) ? Mathf.Floor(y) * bulletSpeed : Mathf.Ceil(y) * bulletSpeed
        );
    }

    Vector2 PuntaMenosCola(Vector2 punta, Vector2 cola)
    {
        return new Vector2(punta.x - cola.x, punta.y - cola.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Boundary")) // Asegúrate de que tus paredes tengan el tag "Boundary"
        {
            // Invertir la dirección del movimiento
            Vector2 newDirection = -rb.velocity.normalized; // Cambia la dirección actual
            rb.velocity = newDirection * maxSpeed; // Aplica la nueva velocidad con la dirección invertida
        }
    }
}
