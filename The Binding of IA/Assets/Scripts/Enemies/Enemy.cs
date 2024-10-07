using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected int health = 100; // Vida del enemigo como entero
    [SerializeField] protected int damage = 10; // Da�o que causa al jugador
    [SerializeField] protected Color damageColor = Color.red; // Color para el efecto de da�o

    private bool isAlive = true; // Estado del enemigo

    // M�todo para aplicar da�o al enemigo
    public virtual void TakeDamage(int amount)
    {
        if (!isAlive) return;

        health -= amount; // Reducir la vida
        StartCoroutine(ShowDamageEffect()); // Mostrar efecto de da�o

        if (health <= 0)
        {
            Die(); // Llamar al m�todo de morir
        }
    }

    // M�todo que se llama al morir
    protected virtual void Die()
    {
        isAlive = false;
        // Aqu� puedes a�adir animaciones o efectos de muerte
        Destroy(gameObject); // Destruir el objeto
    }

    // M�todo para mostrar el efecto de da�o
    private IEnumerator ShowDamageEffect()
    {
        // Cambiar el color del enemigo a rojo
        GetComponent<SpriteRenderer>().color = damageColor;
        yield return new WaitForSeconds(0.2f); // Esperar un poco
        GetComponent<SpriteRenderer>().color = Color.white; // Regresar al color original
    }

    // M�todo para manejar la colisi�n con el jugador
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Comprobar si colisiona con el jugador
        {
            // Aplicar da�o al jugador (necesitar�s tener referencia al script del jugador)
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage); // Aplicar da�o al jugador
            }
        }
    }
}
