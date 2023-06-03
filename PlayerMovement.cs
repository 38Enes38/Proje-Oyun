using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rgb;
    Vector3 velocity;
    public Animator animator;
    public TextMeshProUGUI playerScoreText;

    public int score;

    float speedAmount = 5f;
    float jumpAmount = 5.7f;
    bool canJump = true;

    public int can, maxcan;

    public GameObject[] canlar;

    public AudioClip collectSound; // Toplama sesi için AudioClip tanýmlayýn
    private AudioSource audioSource;

    void Start()
    {
        rgb = GetComponent<Rigidbody2D>();
        score = 0;
        can = maxcan;
        canSistemi();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        playerScoreText.text = score.ToString();
        velocity = new Vector3(Input.GetAxis("Horizontal"), 0f);
        transform.position += velocity * speedAmount * Time.deltaTime;
        animator.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")));

        if (Input.GetButtonDown("Jump") && canJump)
        {
            canJump = false;
            rgb.AddForce(Vector3.up * jumpAmount, ForceMode2D.Impulse);
        }

        if (Input.GetAxisRaw("Horizontal") == -1)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (Input.GetAxisRaw("Horizontal") == 1)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }

        if (can <= 0)
        {
            olme();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Zemin")
        {
            canJump = true;
        }

        if (collision.gameObject.tag == "Tuzak")
        {
            can -= Random.Range(1, 2);
            rgb.AddForce(Vector2.up * jumpAmount, ForceMode2D.Impulse);
            GetComponent<SpriteRenderer>().color = Color.red;
            Invoke("Duzelt", 0.5f);
            canSistemi();
        }

        if (collision.gameObject.tag == "Altin")
        {
            score += 10;
            Destroy(collision.gameObject);
            PlayCollectSound(); 
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
            player.score += 10;
            Destroy(gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Zemin")
        {
            // animator.SetBool("IsJumping", false);
        }
    }

    void olme()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void canSistemi()
    {
        for (int i = 0; i < maxcan; i++)
        {
            canlar[i].SetActive(false);
        }
        for (int i = 0; i < can; i++)
        {
            canlar[i].SetActive(true);
        }
    }

    void Duzelt()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    void PlayCollectSound()
    {
        audioSource.PlayOneShot(collectSound);
    }
}