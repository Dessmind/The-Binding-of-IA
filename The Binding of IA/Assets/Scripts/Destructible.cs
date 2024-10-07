using UnityEngine;
using System.Collections; // Necesario para usar coroutines

public class Destructible : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    private int currentHealth;

    [SerializeField] private float blinkDuration = 0.5f; // Duración total del parpadeo
    [SerializeField] private int blinkCount = 5; // Cantidad de parpadeos

    private SpriteRenderer spriteRenderer; // Referencia al SpriteRenderer

    private void Start()
    {
        currentHealth = maxHealth; // Inicializa la salud
        spriteRenderer = GetComponent<SpriteRenderer>(); // Obtiene el componente SpriteRenderer
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Reduce la salud actual
        StartCoroutine(Blink()); // Inicia el efecto de parpadeo

        if (currentHealth <= 0)
        {
            Destroy(gameObject); // Destruir el objeto si su salud llega a 0
        }
    }

    private IEnumerator Blink()
    {
        for (int i = 0; i < blinkCount; i++)
        {
            spriteRenderer.enabled = false; // Desactiva el sprite
            yield return new WaitForSeconds(blinkDuration / (blinkCount * 2)); // Espera medio tiempo de parpadeo
            spriteRenderer.enabled = true; // Activa el sprite
            yield return new WaitForSeconds(blinkDuration / (blinkCount * 2)); // Espera medio tiempo de parpadeo
        }
    }
}
