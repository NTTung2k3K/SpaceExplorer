using UnityEngine;

public class BigStar : MonoBehaviour
{
    // Tốc độ rơi của ngôi sao lớn
    public float speed = 1f;

    GameObject scoreUITextGO;

    void Start()
    {
        scoreUITextGO = GameObject.FindGameObjectWithTag("ScoreTextTag");
    }

    void Update()
    {
        // Lấy vị trí hiện tại
        Vector2 position = transform.position;

        // NGoi sao tu tren xuong( - o day la di chuyen xuong)
        position = new Vector2(position.x, position.y - speed * Time.deltaTime);

        // Cap nhat vi tri
        transform.position = position;

        // Lay vi tri duoi trai man hinh 
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        // Ngoi sao ra khoi man hinh thi huy 
        if (transform.position.y < min.y)
        {
            Destroy(gameObject);
        }
    }

    // Xử lý va chạm
    void OnTriggerEnter2D(Collider2D col)
    {
        // Nguoi choi cham vao ngoi sao
        if (col.tag == "PlayerShipTag")
        {
            // Cong 500 ddiem
            scoreUITextGO.GetComponent<GameScore>().Score += 500;

            // Huy ngoi sao
            Destroy(gameObject);
        }
    }
}
