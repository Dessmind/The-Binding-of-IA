using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float lifespan = 2f; // Tiempo de vida del proyectil

    private void Start()
    {
        // Destruir el proyectil después de un tiempo de vida
        Destroy(gameObject, lifespan);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verificar si el objeto con el que colisiona tiene el tag "Destructible"
        if (collision.CompareTag("Destructible"))
        {
            // Intentar obtener el componente Destructible
            Destructible destructible = collision.GetComponent<Destructible>();
            if (destructible != null)
            {
                destructible.TakeDamage(damage); // Aplicar daño
            }
            Destroy(gameObject); // Destruir el proyectil tras colisionar
        }
        else if (!collision.CompareTag("Player"))
        {
            Destroy(gameObject); // Destruir el proyectil si golpea cualquier otro objeto que no sea el jugador
        }
    }
}
