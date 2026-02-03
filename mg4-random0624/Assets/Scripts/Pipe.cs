using UnityEngine;

public class Pipe : MonoBehaviour
{
    [Header("Top Pipe Parts")]
    [SerializeField] private SpriteRenderer _topCap;
    [SerializeField] private SpriteRenderer _topMiddle;

    [Header("Bottom Pipe Parts")]
    [SerializeField] private SpriteRenderer _bottomCap;
    [SerializeField] private SpriteRenderer _bottomMiddle;

    [Header("Score Zone")]
    [SerializeField] private BoxCollider2D _scoreZone;

    [Header("Settings")]
    [SerializeField] private float _moveSpeed = 3f;
    [SerializeField] private float _gapSize = 2.8f;
    [SerializeField] private float _minGapY = -3f;
    [SerializeField] private float _maxGapY = 3f;
    [SerializeField] private float _destroyXPosition = -10f;

    private const float _topOfScreen = 5.5f;
    private const float _bottomOfScreen = -5f;

    private bool _isStopped = false;

    void OnEnable()
    {
        if (GameController.Instance != null)
        {
            GameController.Instance.OnGameOver += StopPipe;
        }
    }

    void OnDisable()
    {
        if (GameController.Instance != null)
        {
            GameController.Instance.OnGameOver -= StopPipe;
        }
    }

    void StopPipe()
    {
        _isStopped = true;
    }

    void Start()
    {
        // Random gap center position
        float gapCenterY = Random.Range(_minGapY, _maxGapY);
        float gapTop = gapCenterY + (_gapSize / 2f);
        float gapBottom = gapCenterY - (_gapSize / 2f);

        // Get the base sprite height at scale 1 (default scale is 2, so divide by 2)
        float topCapHeight = _topCap.sprite.bounds.size.y * _topCap.transform.localScale.y;
        float topMiddleBaseHeight = _topMiddle.sprite.bounds.size.y;
        float topMiddleHeight = _topOfScreen - gapTop - topCapHeight;
        
        // Scale top middle pipe by adjusting Y scale
        float topMiddleScaleY = topMiddleHeight / topMiddleBaseHeight;
        _topMiddle.transform.localScale = new Vector3(2f, topMiddleScaleY, 2f);
        
        // Position top middle at top of screen going down
        _topMiddle.transform.localPosition = new Vector3(0, _topOfScreen - (topMiddleHeight / 2f), 0);
        
        // Position top cap directly below top middle (connected) and flip it
        _topCap.transform.localPosition = new Vector3(0, _topOfScreen - topMiddleHeight - (topCapHeight / 2f), 0);
        _topCap.transform.localScale = new Vector3(_topCap.transform.localScale.x, -Mathf.Abs(_topCap.transform.localScale.y), _topCap.transform.localScale.z);

        // === BOTTOM PIPE (rises up from bottom of screen) ===
        float bottomCapHeight = _bottomCap.sprite.bounds.size.y * _bottomCap.transform.localScale.y;
        float bottomMiddleBaseHeight = _bottomMiddle.sprite.bounds.size.y;
        float bottomMiddleHeight = gapBottom - bottomCapHeight - _bottomOfScreen;
        
        // Scale bottom middle pipe by adjusting Y scale
        float bottomMiddleScaleY = bottomMiddleHeight / bottomMiddleBaseHeight;
        _bottomMiddle.transform.localScale = new Vector3(2f, bottomMiddleScaleY, 2f);
        
        // Position bottom middle at bottom of screen going up
        _bottomMiddle.transform.localPosition = new Vector3(0, _bottomOfScreen + (bottomMiddleHeight / 2f), 0);
        
        // Position bottom cap directly above bottom middle (connected) and flip it
        _bottomCap.transform.localPosition = new Vector3(0, _bottomOfScreen + bottomMiddleHeight + (bottomCapHeight / 2f), 0);
        _bottomCap.transform.localScale = new Vector3(_bottomCap.transform.localScale.x, -Mathf.Abs(_bottomCap.transform.localScale.y), _bottomCap.transform.localScale.z);

        // Position score zone in the gap
        if (_scoreZone != null)
        {
            _scoreZone.transform.localPosition = new Vector3(0.3f, gapCenterY, 0);
            _scoreZone.size = new Vector2(_scoreZone.size.x, _gapSize);
        }
    }

    void Update()
    {
        if (_isStopped) return;

        transform.position += Vector3.left * _moveSpeed * Time.deltaTime;

        if (transform.position.x < _destroyXPosition)
        {
            Destroy(gameObject);
        }
    }
}
