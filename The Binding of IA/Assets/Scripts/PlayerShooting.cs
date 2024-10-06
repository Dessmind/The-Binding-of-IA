using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab; // Prefab del proyectil
    [SerializeField] private Transform firePoint; // Punto desde donde se dispara el proyectil
    private PlayerMovement playerMovement; // Referencia al script de movimiento del jugador

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>(); // Obtener referencia al script de movimiento
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        // Instanciar el proyectil en la posici�n del firePoint
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        // Obtener la direcci�n del movimiento desde PlayerMovement
        Vector2 shootDirection = playerMovement.GetMovementDirection(); // Direcci�n del movimiento del jugador
        if (shootDirection == Vector2.zero) // Si no se est� moviendo, disparar hacia arriba
        {
            shootDirection = Vector2.up; // O cualquier direcci�n que desees
        }

        projectile.GetComponent<Projectile>().SetDirection(shootDirection); // Establecer la direcci�n del proyectil
    }
}
