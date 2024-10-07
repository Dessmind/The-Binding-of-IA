using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected int health = 100; // Vida del enemigo como entero
    [SerializeField] protected int damage = 10; // Daño que causa al jugador
    [SerializeField] protected Color damageColor = Color.red; // Color para el efecto de daño

    private bool isAlive = true; // Estado del enemigo

    // Método para aplicar daño al enemigo
    public virtual void TakeDamage(int amount)
    {
        if (!isAlive) return;

        health -= amount; // Reducir la vida
        StartCoroutine(ShowDamageEffect()); // Mostrar efecto de daño

        if (health <= 0)
        {
            Die(); // Llamar al método de morir
        }
    }

    // Método que se llama al morir
    protected virtual void Die()
    {
        isAlive = false;
        // Aquí puedes añadir animaciones o efectos de muerte
        Destroy(gameObject); // Destruir el objeto
    }

    // Método para mostrar el efecto de daño
    private IEnumerator ShowDamageEffect()
    {
        // Cambiar el color del enemigo a rojo
        GetComponent<SpriteRenderer>().color = damageColor;
        yield return new WaitForSeconds(0.2f); // Esperar un poco
        GetComponent<SpriteRenderer>().color = Color.white; // Regresar al color original
    }

    // Método para manejar la colisión con el jugador
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Comprobar si colisiona con el jugador
        {
            // Aplicar daño al jugador (necesitarás tener referencia al script del jugador)
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage); // Aplicar daño al jugador
            }
        }
    }
}
