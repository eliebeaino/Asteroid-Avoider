using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverHandler : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverDisplay;
    [SerializeField] private TMP_Text _gameOverText;
    [SerializeField] private ScoreSystem _scoreSystem;
    [SerializeField] private AsteroidSpawner _asteroidSpawner;
    [SerializeField] private GameObject _player;

    private void Start()
    {
        _gameOverDisplay.SetActive(false);
    }

    public void EndGame()
    {
        _asteroidSpawner.enabled = false;
        _gameOverDisplay.SetActive(true);
        _scoreSystem.CounterActiveState(false);

        int finalScore = _scoreSystem.GetScore();
        _gameOverText.text = $"Your Score: {finalScore}";
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
    }

    public void Continue()
    {
        _player.SetActive(true);
        _asteroidSpawner.enabled = true;
        _scoreSystem.CounterActiveState(true);
        _gameOverDisplay.SetActive(false) ;
        // reset player movement
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
