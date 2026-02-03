using UnityEngine;
using TMPro;   
using System;

public class GameController : MonoBehaviour
{
    // Singleton instance
    public static GameController Instance { get; private set; }

    // Events for score changes
    public event Action<int> OnScoreChanged;
    
    // Event for game over
    public event Action OnGameOver;

    // UI reference for score display
    [SerializeField] private TextMeshProUGUI _scoreText;

    // Audio sources
    [SerializeField] private AudioSource _flapSound;
    [SerializeField] private AudioSource _scoreSound;
    [SerializeField] private AudioSource _deathSound;

    private int _score;
    private bool _isGameActive = true;

    public bool IsGameActive => _isGameActive;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Subscribe to score change event
        OnScoreChanged += UpdateScoreUI;
        OnScoreChanged += PlayScoreSound;
    }

    private void Start()
    {
        _score = 0;
        UpdateScoreUI(_score);
    }

    private void OnDestroy()
    {
        // Unsubscribe from events
        OnScoreChanged -= UpdateScoreUI;
        OnScoreChanged -= PlayScoreSound;
    }

    public void AddScore()
    {
        if (!_isGameActive) return;
        
        _score++;
        OnScoreChanged?.Invoke(_score);
    }

    private void UpdateScoreUI(int score)
    {
        if (_scoreText != null)
        {
            _scoreText.text = score.ToString();
        }
    }

    private void PlayScoreSound(int score)
    {
        if (_scoreSound != null)
        {
            _scoreSound.Play();
        }
    }

    public void PlayFlapSound()
    {
        if (_flapSound != null)
        {
            _flapSound.Play();
        }
    }

    public void GameOver()
    {
        if (!_isGameActive) return;
        
        _isGameActive = false;
        
        // Notify all subscribers that game is over
        OnGameOver?.Invoke();
        
        if (_deathSound != null)
        {
            _deathSound.Play();
        }
    }
}