using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject GameManagerGO;

    public GameObject PlayerBulletGO;   // Prefab đạn của người chơi
    public GameObject bulletPosition01; // Vị trí bắn đạn thứ nhất
    public GameObject bulletPosition02; // Vị trí bắn đạn thứ hai
    public GameObject bulletPosition03; // Vị trí bắn đạn thứ ba, vị trí này sẽ được thêm khi có 3 tia
    public GameObject ExplosionGO;      // Prefab vụ nổ

    // Tham chiếu đến UI hiển thị số mạng của người chơi
    public Text LivesUIText;

    const int MaxLives = 3;
    int lives;

    public float speed;

    // Biến lưu trữ số lượng tia đạn hiện tại
    public int currentBullets = 1;

    // Thành phần AudioSource được dùng để phát âm thanh
    private AudioSource audioSource;

    public void Init()
    {
        lives = MaxLives;
        LivesUIText.text = lives.ToString();

        // Reset vị trí của người chơi về giữa màn hình
        transform.position = Vector2.zero;
        gameObject.SetActive(true);
    }

    void Start()
    {
        // Lấy AudioSource đã được gán vào GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // Nếu chưa có, thêm AudioSource vào GameObject
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        // Kiểm tra phím space để bắn đạn
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Phát âm thanh bắn súng
            audioSource.Play();

            // Bắn đạn dựa trên số lượng tia đạn hiện tại
            if (currentBullets == 1)
            {
                Instantiate(PlayerBulletGO, bulletPosition03.transform.position, Quaternion.identity);
            }

            if (currentBullets == 2)
            {
                Instantiate(PlayerBulletGO, bulletPosition02.transform.position, Quaternion.identity);
                Instantiate(PlayerBulletGO, bulletPosition01.transform.position, Quaternion.identity);
            }

            if (currentBullets >= 3)
            {
                Instantiate(PlayerBulletGO, bulletPosition03.transform.position, Quaternion.identity);
                Instantiate(PlayerBulletGO, bulletPosition02.transform.position, Quaternion.Euler(0, 0, -30));  // Xéo qua trái
                Instantiate(PlayerBulletGO, bulletPosition01.transform.position, Quaternion.Euler(0, 0, 30));   // Xéo qua phải
            }
        }
        // Xử lý di chuyển của người chơi
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector2 direction = new Vector2(x, y).normalized;
        Move(direction);
    }

    void Move(Vector2 direction)
    {
        // Tính toán giới hạn di chuyển dựa trên kích thước màn hình
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        max.x -= 0.225f;
        min.x += 0.225f;
        max.y -= 0.285f;
        min.y += 0.285f;

        Vector2 pos = transform.position;
        pos += direction * speed * Time.deltaTime;

        // Đảm bảo vị trí không vượt ngoài màn hình
        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);
        transform.position = pos;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // Kiểm tra va chạm với địch hoặc đạn của địch
        if (col.CompareTag("EnemyShipTag") || col.CompareTag("EnemyBulletTag"))
        {
            PlayExplosion();
            lives--;
            LivesUIText.text = lives.ToString();

            if (lives == 0)
            {
                GameManagerGO.GetComponent<GameManager>()
                    .SetGameManagerState(GameManager.GameManagerState.GameOver);
                gameObject.SetActive(false);
            }
        }

        // Kiểm tra va chạm với ngôi sao lớn
        if (col.CompareTag("BigStarTag"))
        {

            // Khi ăn ngôi sao, tăng số tia đạn lên
            if (currentBullets < 3)
            {
                currentBullets++;
            }
            Destroy(col.gameObject);  // Hủy ngôi sao
        }
    }

    void PlayExplosion()
    {
        GameObject explosion = Instantiate(ExplosionGO);
        explosion.transform.position = transform.position;
    }
}
