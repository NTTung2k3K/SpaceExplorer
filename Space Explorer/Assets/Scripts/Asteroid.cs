using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float speed; // tốc độ thiên thạch
    public GameObject ExplosionGO;

    void Start()
    {
        // Khởi tạo nếu cần
    }

    void Update()
    {
        // Lấy vị trí hiện tại của thiên thạch
        Vector2 position = transform.position;

        // Tính toán vị trí mới của thiên thạch
        position = new Vector2(position.x, position.y + speed * Time.deltaTime);

        // Cập nhật vị trí của thiên thạch
        transform.position = position;

        // Lấy vị trí dưới cùng bên trái của màn hình
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        // Lấy vị trí trên cùng bên phải của màn hình
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        // Nếu thiên thạch di chuyển ra khỏi màn hình ở phía dưới, đưa nó trở lại từ phía trên với vị trí X ngẫu nhiên
        if (transform.position.y < min.y)
        {
            transform.position = new Vector2(Random.Range(min.x, max.x), max.y);
        }
    }

    // Phương thức xử lý va chạm (cần đánh dấu collider của thiên thạch là Trigger)
    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(ExplosionGO);

        //dat vi tri vu no
        explosion.transform.position = transform.position;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // phat hien va cham voi dich hoac dan cua dich
        if ((col.tag == "PlayerShipTag") || (col.tag == "PlayerBulletTag"))
        {
            PlayExplosion();

            Destroy(gameObject);// huy tau
        }
    }
}
