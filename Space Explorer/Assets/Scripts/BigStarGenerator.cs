using UnityEngine;

public class BigStarGenerator : MonoBehaviour
{
    // Prefab ngôi sao lớn (gắn script BigStar)
    public GameObject bigStarPrefab;

    // Khoảng thời gian lặp lại để sinh Big Star
    public float spawnInterval = 10f;

    void Start()
    {
        // Gọi hàm SpawnBigStar lần đầu sau 5 giây,
        // rồi lặp lại mỗi spawnInterval giây
        InvokeRepeating("SpawnBigStar", 5f, spawnInterval);
    }

    void SpawnBigStar()
    {
        // Lấy toạ độ cạnh dưới và cạnh trên màn hình
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        // Tạo 1 Big Star ở toạ độ ngẫu nhiên phía trên màn hình
        Vector2 spawnPos = new Vector2(Random.Range(min.x, max.x), max.y);

        // Sinh Big Star
        GameObject newBigStar = Instantiate(bigStarPrefab);
        newBigStar.transform.position = spawnPos;

        // Nếu muốn, bạn có thể tuỳ chỉnh tốc độ ở đây
        // newBigStar.GetComponent<BigStar>().speed = 2.5f;
    }
}
