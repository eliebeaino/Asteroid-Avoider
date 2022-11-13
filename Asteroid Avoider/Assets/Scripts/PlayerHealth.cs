using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private GameOverHandler gameOverHandler= null;

    public void Crash()
    {
        gameOverHandler.EndGame();
        gameObject.SetActive(false);
    }
}
