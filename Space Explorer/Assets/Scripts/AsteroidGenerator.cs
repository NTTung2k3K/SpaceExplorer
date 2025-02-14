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

    public float waveDelay = 1f; // Thời gian chờ giữa các đợt

    private bool isSpawningWave = false; // Cờ kiểm tra đang tạo đợt mới hay chưa

    void Start()
    {
        // Tạo đợt đầu tiên khi bắt đầu game
        StartCoroutine(SpawnWaveRoutine());
    }

    // Coroutine tạo 1 đợt thiên thạch
    IEnumerator SpawnWaveRoutine()
    {
        isSpawningWave = true;

        // Chờ trước khi tạo đợt mới (có thể bỏ nếu không cần)
        yield return new WaitForSeconds(waveDelay);

        // Lấy vị trí của màn hình
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)); // dưới trái
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)); // trên phải

        // Mỗi thiên thạch được spawn từ phía trên (y = max.y) với vị trí x ngẫu nhiên
        for (int i = 0; i < MaxAsteroids; i++)
        {
            Vector2 spawnPosition = new Vector2(Random.Range(min.x, max.x), max.y);
            GameObject asteroid = Instantiate(AsteroidGO, spawnPosition, Quaternion.identity);

            // Gán màu ngẫu nhiên cho thiên thạch
            asteroid.GetComponent<SpriteRenderer>().color = asteroidColors[Random.Range(0, asteroidColors.Length)];

            // Gán tốc độ ngẫu nhiên (âm để di chuyển xuống dưới)
            asteroid.GetComponent<Asteroid>().speed = -(1f * Random.value + 0.5f);

            // Gán thiên thạch vừa tạo thành con của đối tượng generator (để dễ quản lý)
            asteroid.transform.parent = transform;
        }

        isSpawningWave = false;
    }

    void Update()
    {
        // Kiểm tra nếu tất cả các thiên thạch của đợt hiện tại đã bị bắn hạ (không còn con nào)
        // và không đang trong quá trình tạo đợt mới
        if (transform.childCount == 0 && !isSpawningWave)
        {
            // Tạo đợt mới
            StartCoroutine(SpawnWaveRoutine());
        }
    }
}
