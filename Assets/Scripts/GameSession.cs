using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField] int _playerLives;
    [SerializeField] int _playerScore = 0;

    [SerializeField] TextMeshProUGUI _livesText;
    [SerializeField] TextMeshProUGUI _scoreText;

    void Awake()
    {
        int gameSessionNumber = FindObjectsOfType<GameSession>().Length;
        if (gameSessionNumber > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        _livesText.text = _playerLives.ToString();
        _scoreText.text = _playerScore.ToString();
    }

    public void AddScore(int pointValue)
    {
        _playerScore += pointValue;
        _scoreText.text = _playerScore.ToString();
    }

    public void PlayerSession()
    {
        if (_playerLives > 1)
        {
            TakeLive();
        }
        else
        {
            ResetGameSession();
        }
    }

    void TakeLive()
    {
        _playerLives -= 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        _livesText.text = _playerLives.ToString();
    }

    void ResetGameSession()
    {
        FindObjectOfType<ScenePresiste>().resetScenePresiste();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }


}
