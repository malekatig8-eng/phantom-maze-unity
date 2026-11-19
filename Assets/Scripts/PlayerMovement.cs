using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Déplacement")]
    public float moveSpeed = 5f;
    public float jumpForce = 7f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private Rigidbody rb;
    private Vector2 moveInput;
    private bool isGrounded;

    // ─── Initialisation ───
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // ─── Appelées automatiquement par Player Input (Send Messages) ───
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnJump()
    {
        if (isGrounded)
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    // ─── Physique (appelée à taux fixe, 50 fps par défaut) ───
    void FixedUpdate()
    {
        // 1. Vérifier si le joueur touche le sol
        isGrounded = Physics.CheckSphere(
            groundCheck.position,
            groundDistance,
            groundMask
        );

        // 2. Calculer la direction de déplacement
        Vector3 direction = new Vector3(moveInput.x, 0f, moveInput.y);

        // 3. Appliquer le déplacement via le Rigidbody
        rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);

        // 4. Tourner le joueur dans la direction du déplacement
        if (direction != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(direction);
    }
}