using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    private int currentHealth;
    private bool isInvulnerable = false;
    private SpriteRenderer spriteRenderer;
    private PlayerUI playerUI;
    private PlayerMovement playerMovement; // Referencia al script de movimiento

    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerUI = FindObjectOfType<PlayerUI>();
        playerMovement = GetComponent<PlayerMovement>(); // Obtiene el script de movimiento
        playerUI.UpdateHearts(); // Actualiza las vidas en la interfaz al inicio
    }

    public void TakeDamage(int damage)
    {
        if (isInvulnerable) return;

        currentHealth -= damage;
        playerUI.UpdateHearts(); // Actualiza la interfaz
        StartCoroutine(Invulnerability());

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player ha muerto.");
        playerMovement.SetAlive(false); // Detiene el movimiento del jugador
        StartCoroutine(BlinkAndDisappear()); // Llama al parpadeo y desaparición
    }

    private IEnumerator BlinkAndDisappear()
    {
        for (int i = 0; i < 5; i++) // Parpadeo de 5 veces
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(0.2f);
        }
        // Desactiva el objeto y sus hijos
        gameObject.SetActive(false);
    }

    private IEnumerator Invulnerability()
    {
        isInvulnerable = true;
        for (int i = 0; i < 5; i++) // Parpadeo de 5 veces
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(0.2f);
        }
        spriteRenderer.enabled = true; // Asegúrate de que el sprite esté visible
        isInvulnerable = false;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}
