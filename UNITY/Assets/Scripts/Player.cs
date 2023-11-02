using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //public static Player playerInstance;
    public float speed;
    public float gravity;
    public GameController gameController;
    public LayerMask layerGround;
    public GameObject spawnPoint;
    private Rigidbody2D rb;
    private BoxCollider2D playerCollider;
    private Animator playerAnimator;
    public bool isRight = true;
    public bool isUp = true;
    float moveInput;
    bool isGrounded = true;
    bool isDying = false;
    private AudioSource audioManager;
    public AudioClip jumpAudio;
    public AudioClip dieAudio;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
        playerAnimator = GetComponent<Animator>();
        audioManager = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Cambiar gravedad al pulsar W/S/FlechaArriba/FlechaAbajo
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow)) && isGrounded && !isDying)
        {
            audioManager.PlayOneShot(jumpAudio);
            gravity = -gravity;
            FlipY();
        }
    }

    // Movimiento en FixedUpdate
    void FixedUpdate()
    {
        if (!isDying)
        {
            isGrounded = CheckIsGrounded();
            moveInput = Input.GetAxis("Horizontal");

            rb.velocity = new Vector2(moveInput * speed, isGrounded ? 0.0f : gravity);

            playerAnimator.SetBool("isWalking", false);

            // Activamos la animación de caminar
            if (moveInput > 0.0f || moveInput < 0.0f)
            {
                playerAnimator.SetBool("isWalking", true);
            }

            // Volteamos horizontalmente
            if ((isRight == false && moveInput > 0) || (isRight == true && moveInput < 0))
            {
                FlipX();
            }
        }
    }

    // Si le da un objeto "Enemies" (tag) llamamos a Death
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemies"))
        {
            StartCoroutine(Death());
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CheckPoint"))
        {
            Checkpoint checkpoint = collision.gameObject.GetComponent<Checkpoint>();

            // Comprobamos si el checkpoint está desactivado.
            if (!checkpoint.isActive)
            {
                // Activamos el checkpoint
                gameController.ActivateCheckpoint(collision.gameObject);
            }
        }
        else if (collision.gameObject.CompareTag("Keys"))
        {
            // Eliminamos la llave
            Destroy(collision.gameObject);

            // Añadimos la llave al contador con el game controller
            gameController.AddKey();
        }
    }

    // Primera vez q uso algo como esto a conciencia
    bool CheckIsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(playerCollider.bounds.center, playerCollider.bounds.size, 0f, isUp ? Vector2.down : Vector2.up, 0.1f, layerGround);

        return raycastHit.collider != null;
    }


    // Cambiar sprite izquierda/derecha
    void FlipX()
    {
        isRight = !isRight;
        Vector2 flipx = transform.localScale;
        flipx.x *= -1;
        transform.localScale = flipx;
    }

    // Cambiar sprite arriba/abajo
    void FlipY()
    {
        isUp = !isUp;
        Vector2 flipy = transform.localScale;
        flipy.y *= -1;
        transform.localScale = flipy;
    }

    IEnumerator Death()
    {
        isDying = true;
        playerAnimator.SetBool("isDying", true);
        audioManager.PlayOneShot(dieAudio);

        // Desactivamos rigidbody y colliders para que no le afecte nada mientras muere 
        rb.Sleep();
        playerCollider.enabled = false;

        // Esperamos un segundito
        yield return new WaitForSeconds(1);

        playerAnimator.SetBool("isDying", false);

        // Si estabamos boca abajo antes de morir reseteamos la gravedad y la dirección del personaje
        if (gravity > 0.0f)
        {
            FlipY();
            gravity *= -1;
        }

        // Movemos al jugador al checkpoint activo
        transform.position = gameController.activeCheckpoint.transform.position;

        rb.WakeUp();
        playerCollider.enabled = true;
        isDying = false;
    }

    // No habia manera de que fuese >:(

    //void Awake()
    //{
    //    if (Player.playerInstance == null)
    //    {
    //        playerInstance = this;
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}

}
