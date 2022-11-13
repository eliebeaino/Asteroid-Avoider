using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText = null;
    [SerializeField] private float _scoreMultipler = 5f;

    private float _score;
    private bool _isPlaying = true;

    private void Update()
    {
        if (!_isPlaying) 
        {
            _scoreText.text = string.Empty;
            return; 
        }

        _score += Time.deltaTime * _scoreMultipler;

        _scoreText.text = Mathf.FloorToInt(_score).ToString();
    }

    public void CounterActiveState(bool state)
    {
        _isPlaying = state;
    }

    public int GetScore()
    {
        return Mathf.FloorToInt(_score);
    }
}
