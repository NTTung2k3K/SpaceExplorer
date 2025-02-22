﻿using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float speed;           // Tốc độ thiên thạch ban đầu
    public GameObject ExplosionGO;
    public bool isBig = false;      // false: thiên thạch nhỏ, true: thiên thạch lớn

    private int hitCount = 0;       // Số lần bắn trúng (chỉ áp dụng với thiên thạch lớn)
    private GameObject scoreUITextGO;

    void Start()
    {
        // Tìm đối tượng chứa component GameScore qua tag "ScoreTextTag"
        scoreUITextGO = GameObject.FindGameObjectWithTag("ScoreTextTag");
    }

    void Update()
    {
        // Mỗi phút tăng speed thêm 1 đơn vị
        speed += (1f / 60f) * Time.deltaTime;

        // Tính toán vị trí mới
        Vector2 position = transform.position;
        position = new Vector2(position.x, position.y + speed * Time.deltaTime);
        transform.position = position;

        // Kiểm tra nếu thiên thạch trôi ra khỏi màn hình (ở phía dưới)
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        if (transform.position.y < min.y)
        {
            // Chỉ trừ điểm nếu game chưa kết thúc
            if (scoreUITextGO != null && !GameManager.IsGameOver)
            {
                scoreUITextGO.GetComponent<GameScore>().Score -= 500;
            }
            Destroy(gameObject);
        }
    }

    void PlayExplosion()
    {
        GameObject explosion = Instantiate(ExplosionGO);
        explosion.transform.position = transform.position;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "PlayerBulletTag")
        {
            // Hủy viên đạn ngay khi va chạm
            Destroy(col.gameObject);

            if (isBig)
            {
                // Với thiên thạch lớn, cần 2 lần bắn trúng
                hitCount++;
                if (hitCount >= 2)
                {
                    if (scoreUITextGO != null && !GameManager.IsGameOver)
                    {
                        scoreUITextGO.GetComponent<GameScore>().Score += 100;
                    }
                    PlayExplosion();
                    Destroy(gameObject);
                }
                // Lần đầu tiên bắn chỉ tăng hitCount mà không phá hủy
            }
            else
            {
                // Với thiên thạch nhỏ, 1 lần bắn đủ
                if (scoreUITextGO != null && !GameManager.IsGameOver)
                {
                    scoreUITextGO.GetComponent<GameScore>().Score += 50;
                }
                PlayExplosion();
                Destroy(gameObject);
            }
        }
        else if (col.tag == "PlayerShipTag")
        {
            PlayExplosion();
            Destroy(gameObject);
        }
    }
}
