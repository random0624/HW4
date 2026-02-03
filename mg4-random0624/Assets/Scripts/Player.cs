using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private float _jump = 5f;

    private bool _isDead = false;

    void OnEnable()
    {
        if (GameController.Instance != null)
        {
            GameController.Instance.OnGameOver += OnDeath;
        }
    }

    void OnDisable()
    {
        if (GameController.Instance != null)
        {
            GameController.Instance.OnGameOver -= OnDeath;
        }
    }

    void OnDeath()
    {
        _isDead = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Flap();
    }

    void Flap()
    {
        if (_isDead) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rigidbody.velocity = new Vector2(
                _rigidbody.velocity.x,
                _jump
            );

            // Play flap sound through GameController
            if (GameController.Instance != null)
            {
                GameController.Instance.PlayFlapSound();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if player passed through a scoring trigger
        if (other.CompareTag("ScoreZone"))
        {
            if (GameController.Instance != null)
            {
                GameController.Instance.AddScore();
            }
        }
        if (other.CompareTag("Pipe"))
        {
            if (GameController.Instance != null)
            {
                GameController.Instance.GameOver();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if player collided with a pipe
        if (collision.gameObject.CompareTag("Pipe"))
        {
            if (GameController.Instance != null)
            {
                GameController.Instance.GameOver();
            }
        }
    }
}
