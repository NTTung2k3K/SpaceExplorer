using UnityEngine;

public class PlayerBulletController : MonoBehaviour
{
    float speed;
    Vector2 direction;  // Tạo vector hướng cho tia đạn

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = 8f;

        // Lấy hướng di chuyển từ góc quay của tia đạn
        direction = transform.up;  // Tia đạn sẽ di chuyển theo hướng của transform.up (của đối tượng)
    }

    // Update is called once per frame
    void Update()
    {
        // Tính toán vị trí mới của đạn theo hướng của nó
        Vector2 position = transform.position;
        position += direction * speed * Time.deltaTime;

        // Cập nhật vị trí của đạn
        transform.position = position;

        // Kiểm tra nếu tia đạn ra ngoài màn hình
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        if (transform.position.y > max.y)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // Phát hiện va chạm với tàu địch
        if (col.tag == "EnemyShipTag")
        {
            Destroy(gameObject);
        }
    }
}
