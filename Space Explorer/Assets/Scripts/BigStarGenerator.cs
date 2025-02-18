using System.Collections;
using UnityEngine;

public class BigStarGenerator : MonoBehaviour
{
    // Prefab ngôi sao lớn (gắn script BigStar)
    public GameObject bigStarPrefab;

    // Khoảng thời gian để sinh Big Star
    public float spawnInterval = 30f;

    void Start()
    {
        // Gọi hàm SpawnBigStar lần đầu sau spawnInterval giây (30 giây)
        StartCoroutine(SpawnBigStarWithDelay());
    }

    IEnumerator SpawnBigStarWithDelay()
    {
        // Đợi thời gian spawnInterval trước khi tạo ngôi sao đầu tiên
        yield return new WaitForSeconds(spawnInterval);


        // Gọi hàm SpawnBigStar
        SpawnBigStar();


        // Sau đó tiếp tục gọi lại SpawnBigStar sau mỗi spawnInterval giây
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnBigStar();
        }
    }

    void SpawnBigStar()
    {
        // Lấy tọa độ cạnh dưới và cạnh trên màn hình
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));


        // Tạo 1 Big Star ở tọa độ ngẫu nhiên phía trên màn hình
        Vector2 spawnPos = new Vector2(Random.Range(min.x, max.x), max.y);


        // Sinh Big Star
        GameObject newBigStar = Instantiate(bigStarPrefab);
        newBigStar.transform.position = spawnPos;
    }
}
