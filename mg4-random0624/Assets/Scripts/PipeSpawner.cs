using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _pipePrefab;
    [SerializeField] private float _spawnXPosition = 5f;
    [SerializeField] private float _spawnInterval = 2f;

    private GameObject _prefabReference;
    private float _timer;
    private bool _isStopped = false;

    void Awake()
    {
        // Store a copy of the prefab reference so it doesn't get lost
        _prefabReference = _pipePrefab;
    }

    void Start()
    {
        // Subscribe to game over event
        if (GameController.Instance != null)
        {
            GameController.Instance.OnGameOver += StopSpawning;
        }

        // Spawn first pipe immediately
        SpawnPipe();
        _timer = 0f;
    }

    void OnDestroy()
    {
        if (GameController.Instance != null)
        {
            GameController.Instance.OnGameOver -= StopSpawning;
        }
    }

    void StopSpawning()
    {
        _isStopped = true;
    }

    void Update()
    {
        if (_isStopped) return;

        _timer += Time.deltaTime;

        if (_timer >= _spawnInterval)
        {
            SpawnPipe();
            _timer = 0f;
        }
    }

    void SpawnPipe()
    {
        if (_prefabReference != null)
        {
            Instantiate(_prefabReference, new Vector3(_spawnXPosition, 0f, 0f), Quaternion.identity);
        }
        else
        {
            Debug.LogError("Pipe Prefab is missing!");
        }
    }
}
