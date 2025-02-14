using UnityEngine;
using System.Collections;

public class AsteroidGenerator : MonoBehaviour
{
    public GameObject AsteroidGO; // Prefab thiên thạch
    public int MaxAsteroids = 3;  // Số lượng thiên thạch trong mỗi đợt (wave)

    // Mảng màu cho thiên thạch
    Color[] asteroidColors =
    {
        new Color(0.5f, 0.5f, 1f), // blue
        new Color(0f, 1f, 1f),     // green
        new Color(1f, 1f, 0f),     // yellow
        new Color(1f, 0f, 0f),     // red
    };

    public float waveDelay = 1f;
    private bool isSpawningWave = false;

    // Thêm biến canSpawn để kiểm soát việc spawn
    public bool canSpawn = false;

    // Bỏ hàm Start()

    public void StartSpawning()
    {
        StartCoroutine(SpawnWaveRoutine());
    }

    IEnumerator SpawnWaveRoutine()
    {
        isSpawningWave = true;
        yield return new WaitForSeconds(waveDelay);

        // Lấy vị trí màn hình
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        for (int i = 0; i < MaxAsteroids; i++)
        {
            Vector2 spawnPosition = new Vector2(Random.Range(min.x, max.x), max.y);
            GameObject asteroid = Instantiate(AsteroidGO, spawnPosition, Quaternion.identity);

            asteroid.GetComponent<SpriteRenderer>().color = asteroidColors[Random.Range(0, asteroidColors.Length)];
            asteroid.GetComponent<Asteroid>().speed = -(1f * Random.value + 0.5f);

            asteroid.transform.parent = transform;
        }

        isSpawningWave = false;
    }

    void Update()
    {
        if (!canSpawn)
            return;

        if (transform.childCount == 0 && !isSpawningWave)
        {
            StartCoroutine(SpawnWaveRoutine());
        }
    }
}