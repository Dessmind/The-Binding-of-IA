using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private int damage = 1;
    [SerializeField] private float lifetime = 3f; // Tiempo de vida del proyectil

    private Vector2 direction; // Dirección del proyectil

    private void Start()
    {
        // Destruir el proyectil después de cierto tiempo
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        // Mover el proyectil en la dirección especificada
        transform.Translate(direction * speed * Time.deltaTime);

        // Hacer que el proyectil gire (opcional)
        transform.Rotate(0, 0, speed * Time.deltaTime * 5); // Gira el proyectil mientras se mueve
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized; // Establecer la dirección del proyectil
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // Calcular el ángulo
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle)); // Rotar el proyectil a la dirección de disparo
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verificar si choca con un objeto que puede recibir daño
        if (collision.CompareTag("Destructible"))
        {
            var destructible = collision.GetComponent<Destructible>();
            if (destructible != null)
            {
                destructible.TakeDamage(damage); // Hacer daño al objeto
            }
            Destroy(gameObject); // Destruir el proyectil
        }
        //else if (collision.CompareTag("Wall"))
        //{
        //    Destroy(gameObject); // Destruir el proyectil al chocar con una pared
        //}
    }
}
