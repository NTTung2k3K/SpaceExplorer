using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    float speed;// toc do dan 
    Vector2 _direction;// huong di cua dan
    bool isReady; // kiem tra huong di cua dan da duoc tao chua

    //dat gia tri mac dinh 
    private void Awake()
    {
        speed = 5f;
        isReady = false;

    }


    // ham dat gia tri huong di cua dan
    public void SetDirection(Vector2 direction)
    {
        // dat huong di de co vecto nhat dinh
        _direction= direction.normalized;

        isReady = true; //dat flag la true


    }

    // Update is called once per frame
    void Update()
    {
        if (isReady) {
            // lay vi tri hien tai cua bullet
            Vector2 position = transform.position;

            //tinh toan vi tri moi
            position += _direction * speed * Time.deltaTime;

            //cap nhat vi tri
            transform.position = position;

            //buoc tiep theo la xoa duong dan ra khoi game
            //neu duong dan ra khoi man hinh

            // day la vi tri duoi trai man hinh
            Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0,0));

            //vi tri tren phai man hinh
            Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

            //neu ra khoi man hinh, pha huy chung
            if ( (transform.position.x < min.x) || (transform.position.x > max.x) ||
                (transform.position.y < min.y) || (transform.position.y > max.y))
            {
                Destroy(gameObject);
            }
        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // phat hien va cham voi tau nguoi choi
        if (col.tag == "PlayerShipTag")
        {
            Destroy (gameObject);
        }
    }
}
