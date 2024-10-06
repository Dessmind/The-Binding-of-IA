using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    private Rigidbody2D playerRb;
    private Vector2 moveInput;
    private Animator playerAnimator;

    private bool isAlive = true;

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isAlive) return; // Si no est� vivo, no procesar el movimiento

        // Obtener la entrada del usuario
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(moveX, moveY).normalized;

        // Configurar los par�metros del Animator
        playerAnimator.SetFloat("Horizontal", moveX);
        playerAnimator.SetFloat("Vertical", moveY);
        playerAnimator.SetFloat("Speed", moveInput.magnitude);

        // Espejear el personaje seg�n la direcci�n del movimiento
        if (moveX > 0.1f)
        {
            transform.localScale = new Vector3(1, 1, 1); // Mirar a la derecha
        }
        else if (moveX < -0.1f)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Mirar a la izquierda
        }
    }

    private void FixedUpdate()
    {
        if (!isAlive) return; // Si no est� vivo, no mover

        // Mover al jugador en funci�n de la entrada
        playerRb.velocity = moveInput * speed; // Cambia la velocidad directamente
    }

    // M�todo para obtener la direcci�n de movimiento
    public Vector2 GetMovementDirection()
    {
        return moveInput;
    }

    // M�todo para cambiar el estado del jugador
    public void SetAlive(bool alive)
    {
        isAlive = alive;
    }
}
