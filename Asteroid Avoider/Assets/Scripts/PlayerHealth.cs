using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private GameOverHandler gameOverHandler= null;
    [SerializeField] private int health = 5;

    [SerializeField] AudioClip[] crashSFX;

    public Action OnHealthChanged;

    public void Crash()
    {
        // change health
        health--;
        OnHealthChanged();
        
        // play hit SFX
        AudioSource.PlayClipAtPoint(crashSFX[UnityEngine.Random.Range(0, 2)], Camera.main.transform.position);

        // death condition
        if (health <= 0)
        {
            gameOverHandler.EndGame();
            gameObject.SetActive(false);
        }
    }

    public int GetPlayerHP()
    {
        return health;
    }
}
