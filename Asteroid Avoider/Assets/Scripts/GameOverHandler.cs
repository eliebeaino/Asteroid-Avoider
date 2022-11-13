using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverHandler : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverDisplay;
    [SerializeField] private TMP_Text _gameOverText;
    [SerializeField] private Button _continueButton;
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
        AdManager.Instance.ShowAd(this);

        _continueButton.interactable = false;
    }


    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ContinueGame()
    {
        _player.SetActive(true);
        _player.transform.position = Vector3.zero;
        _player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        
        _asteroidSpawner.enabled = true;
        _scoreSystem.CounterActiveState(true);
        _gameOverDisplay.SetActive(false);
        // reset player movement
    }
}
