using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsListener
{
    [SerializeField] private bool _testMode = true;

#if UNITY_ANDROID
    private string _gameId = "5018559";
#elif UNITY_IOS
    private string _gameId = "5018558";
#endif

    public static AdManager Instance;

    GameOverHandler _gameOverHandler;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            Advertisement.AddListener(this);
            Advertisement.Initialize(_gameId, _testMode);
        }
    }

    public void ShowAd(GameOverHandler gameOverHandler)
    {
        this._gameOverHandler = gameOverHandler;
        Advertisement.Show("rewardedVideo");
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.Log($"Unity Ads Error: {message}");
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        switch (showResult)
        {
            case ShowResult.Finished:
                _gameOverHandler.ContinueGame();
                break;

            case ShowResult.Skipped:
                // Ad was skipped
                break;

            case ShowResult.Failed:
                Debug.LogWarning("Ad Failed");
                break;
        }
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        Debug.Log("Unity Ad Started");
    }

    public void OnUnityAdsReady(string placementId)
    {
        Debug.Log("Unity Ad Ready");
    }

}
