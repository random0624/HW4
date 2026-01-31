using UnityEngine;

public class Pipe : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 3f;
    [SerializeField] private float _minGapOffset = -2f;
    [SerializeField] private float _maxGapOffset = 2f;
    [SerializeField] private float _destroyXPosition = -10f;

    // Start is called before the first frame update
    void Start()
    {
        // Randomize the vertical position (gap placement variation)
        float randomYOffset = Random.Range(_minGapOffset, _maxGapOffset);
        transform.position = new Vector3(
            transform.position.x,
            transform.position.y + randomYOffset,
            transform.position.z
        );
    }

    // Update is called once per frame
    void Update()
    {
        // Only move if game is active
        if (GameController.Instance != null && !GameController.Instance.IsGameActive)
        {
            return;
        }

        // Move pipe to the left
        transform.position += Vector3.left * _moveSpeed * Time.deltaTime;

        // Destroy pipe when it goes off screen
        if (transform.position.x < _destroyXPosition)
        {
            Destroy(gameObject);
        }
    }
}
