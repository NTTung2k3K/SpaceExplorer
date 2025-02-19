using UnityEngine;
using System.Collections;

public class AsteroidGenerator : MonoBehaviour
{
    public GameObject AsteroidGO;      // Prefab thiên thạch
    public GameObject BigAsteroidGO;   // Prefab thiên thạch lớn
    public int MaxAsteroids = 1;       // Số lượng mỗi loại cần spawn (sẽ được cập nhật theo thời gian)
    public bool canSpawn = false;      // Cờ cho phép spawn

    // Mảng màu cho thiên thạch
    Color[] asteroidColors =
    {
        new Color(0.5f, 0.5f, 1f), // blue
        new Color(0f, 1f, 1f),     // green
        new Color(1f, 1f, 0f),     // yellow
        new Color(1f, 0f, 0f),     // red
    };

    private bool isSpawningWave = false;
    private float gameStartTime;

    // Khi generator được bật, reset thời gian bắt đầu
    void OnEnable()
    {
        gameStartTime = Time.time;
    }

    // Phương thức khởi tạo spawn, gọi từ GameManager khi vào GamePlay
    public void StartSpawning()
    {
        gameStartTime = Time.time;
        StartCoroutine(SpawnWaveRoutine());
    }

    IEnumerator SpawnWaveRoutine()
    {
        isSpawningWave = true;

        // Tính thời gian đã trôi qua kể từ khi bắt đầu game
        float elapsedTime = Time.time - gameStartTime;

        // Sau mỗi 40 giây, MaxAsteroids tăng thêm 1 đơn vị
        MaxAsteroids = (int)(elapsedTime / 30f) + 1;

        // Lấy biên của màn hình
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        // Spawn theo số lượng đã xác định
        for (int i = 0; i < MaxAsteroids; i++)
        {
            // Spawn Asteroid với vị trí random
            Vector2 spawnPosAsteroid = new Vector2(Random.Range(min.x, max.x), max.y);
            GameObject asteroid = Instantiate(AsteroidGO, spawnPosAsteroid, Quaternion.identity);
            asteroid.GetComponent<SpriteRenderer>().color = asteroidColors[Random.Range(0, asteroidColors.Length)];
            asteroid.GetComponent<Asteroid>().speed = -(1f * Random.value + 0.5f);
            asteroid.transform.parent = transform;

            // Spawn BigAsteroid với vị trí random riêng biệt
            Vector2 spawnPosBig = new Vector2(Random.Range(min.x, max.x), max.y);
            GameObject bigAsteroid = Instantiate(BigAsteroidGO, spawnPosBig, Quaternion.identity);
            bigAsteroid.GetComponent<SpriteRenderer>().color = asteroidColors[Random.Range(0, asteroidColors.Length)];
            bigAsteroid.GetComponent<Asteroid>().speed = -(1f * Random.value + 0.5f);
            bigAsteroid.transform.parent = transform;
        }

        isSpawningWave = false;

        // Chờ cho đến khi tất cả thiên thạch đã bị tiêu diệt
        while (transform.childCount > 0)
        {
            yield return null;
        }

        // Nếu vẫn cho phép spawn, tiến hành đợt spawn tiếp theo
        if (canSpawn)
        {
            StartCoroutine(SpawnWaveRoutine());
        }
    }
    void Update()
    {
        if (!canSpawn)
            return;
    }
}