using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{

    [SerializeField] private Asteroid[] _asteroidPrefabs;
    [SerializeField] private float _secondsBetweenSpawns = 1.5f;
    [SerializeField] private Vector2 _forceRange;

    private Camera _mainCamera;
    private float timer;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            SpawnNewAsteroid();
            timer += _secondsBetweenSpawns;
        }
    }

    private void SpawnNewAsteroid()
    {
        int side = Random.Range(0, 4);

        Vector2 spawnPoint = Vector2.zero;
        Vector2 directon = Vector2.zero;

        //select side of viewport
        switch (side)
        {
            case 0:
                // Left
                spawnPoint.x = 0;
                spawnPoint.y = Random.value;
                directon = new Vector2(1f, Random.Range(-1f, 1f));
                break;
            case 1:
                // Right
                spawnPoint.x = 1;
                spawnPoint.y = Random.value;
                directon = new Vector2(-1f, Random.Range(-1f, 1f));
                break;
            case 2:
                // Bottom
                spawnPoint.x = Random.value;
                spawnPoint.y = 0;
                directon = new Vector2(Random.Range(-1f, 1f), 1f);
                break;
            case 3:
                // Top
                spawnPoint.x = Random.value;
                spawnPoint.y = 1;
                directon = new Vector2(Random.Range(-1f, 1f), -1f);
                break;
        }

        // convert to world point
        Vector3 worldSpawnPoint = _mainCamera.ViewportToWorldPoint(spawnPoint);
        worldSpawnPoint.z = 0;

        // spawn the asteroid
        Asteroid selectedAsterpod = _asteroidPrefabs[Random.Range(0, _asteroidPrefabs.Length)];

        Asteroid asteroidInstance = Instantiate(selectedAsterpod, worldSpawnPoint, 
            Quaternion.Euler(0f,0f,Random.Range(0f,360f)));


        // move the asteroid
        Rigidbody rb = asteroidInstance.GetComponent<Rigidbody>();

        rb.velocity = directon.normalized * Random.Range(_forceRange.x,_forceRange.y);
     }
}
