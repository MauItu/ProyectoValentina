using UnityEngine;

public class Mover : MonoBehaviour
{
    public float moveSpeed = 5f;  // Velocidad de movimiento ajustable desde Unity
    public float jumpForce = 10f; // Fuerza del salto ajustable desde Unity

    private Rigidbody2D rb;       // Referencia al componente Rigidbody2D(Para la gravedad y fisicas)
    private bool isGrounded = false; // Indica si el personaje está en el suelo
    private Animator animator;    // Referencia al componente Animator
    private bool mirandoDerecha = true; // Indica la dirección en la que mira el personaje

    private void Awake()
    {
        // Inicializa las referencias a los componentes Rigidbody2D y Animator para usarlos en el codigo
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Movimiento horizontal
        float moveInput = Input.GetAxis("Horizontal");

        // Actualiza el estado de animación basado en la entrada de movimiento(Para saber a donde se mueve)
        if (moveInput != 0f)
        {
            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }

        // Establece la velocidad del Rigidbody2D para mover el personaje
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Gestiona el cambio de dirección del personaje
        GestionarMovimiento(moveInput);

        // Salto
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            // Aplica una fuerza hacia arriba para saltar
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Comprueba si el personaje está colisionando con el suelo(Para poder saltar de nuevo, y no saltar de manera infinita)
        if (collision.gameObject.CompareTag("ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Comprueba si el personaje ha dejado de colisionar con el suelo
        if (collision.gameObject.CompareTag("ground"))
        {
            isGrounded = false;
        }
    }

    private void GestionarMovimiento(float moveInput)
    {
        // Cambia la dirección en la que mira el personaje si cambia de dirección
        if ((mirandoDerecha == true && moveInput < 0) || (mirandoDerecha == false && moveInput > 0))
        {
            // Invierte el valor de mirandoDerecha para ir al lado opuesto
            mirandoDerecha = !mirandoDerecha;
            // Invierte la escala del personaje para reflejar el cambio de dirección(de derecha a izquierda y viseversa)
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }
}