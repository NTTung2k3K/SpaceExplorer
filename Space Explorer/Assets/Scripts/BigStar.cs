using UnityEngine;

public class BigStar : MonoBehaviour
{
    // Tốc độ rơi của ngôi sao lớn
    public float speed = 1f;
    // Audio clip phát khi va chạm với tàu người chơi
    public AudioClip collisionSound;

    GameObject scoreUITextGO;

    void Start()
    {
        scoreUITextGO = GameObject.FindGameObjectWithTag("ScoreTextTag");
    }

    void Update()
    {
        // Lấy vị trí hiện tại
        Vector2 position = transform.position;

        // Ngôi sao rơi từ trên xuống
        position = new Vector2(position.x, position.y - speed * Time.deltaTime);

        // Cập nhật vị trí
        transform.position = position;

        // Kiểm tra nếu ngôi sao ra khỏi màn hình
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        if (transform.position.y < min.y)
        {
            Destroy(gameObject);
        }
    }

    // Xử lý va chạm
    void OnTriggerEnter2D(Collider2D col)
    {
        // Nếu người chơi ăn ngôi sao
        if (col.tag == "PlayerShipTag")
        {
            // Tăng điểm
            scoreUITextGO.GetComponent<GameScore>().Score += 500;

            // Phát âm thanh va chạm tại vị trí ngôi sao
            if (collisionSound != null)
            {
                AudioSource.PlayClipAtPoint(collisionSound, transform.position);
            }

            // Gửi tín hiệu cho player tăng tia đạn (đã được xử lý trong PlayerController)
            Destroy(gameObject);  // Hủy ngôi sao
        }
    }
}
