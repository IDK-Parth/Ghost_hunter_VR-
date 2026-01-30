using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int maxLives = 3;
    private int currentLives;

    [Header("Ghost Hunting Settings")]
    [SerializeField] private int maxKillsAllowed = 20;
    private int currentKills;

    [Header("Events")]
    public UnityEvent OnGameOver;
    public UnityEvent OnWin;

    void Start()
    {
        currentLives = maxLives;
        currentKills = 0;
    }

    public void TakeDamage(int damage)
    {
        currentLives -= damage;
        Debug.Log($"Lives left: {currentLives}");

        if (currentLives <= 0)
        {
            GameOver();
        }
    }

    public void AddKill()
    {
        currentKills++;
        Debug.Log($"Ghosts killed: {currentKills}");

        if (currentKills >= maxKillsAllowed)
        {
            WinGame();
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over! You ran out of lives.");
        OnGameOver?.Invoke();
        // Add logic to freeze time or reload scene
    }

    private void WinGame()
    {
        Debug.Log("Exorcism Complete! You've cleared the limit.");
        OnWin?.Invoke();
        // Add logic to trigger an ending sequence
    }
}