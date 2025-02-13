using UnityEngine;

public class PlayerBulletController : MonoBehaviour
{
    float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = 8f;
    }

    // Update is called once per frame
    void Update()
    {
        // lay vi tri bullet
        Vector2 position = transform.position;

        // tinh toan vi tri moi cua bullet
        position = new Vector2 (position.x, position.y + speed * Time.deltaTime);

        // cap nhat vi tri bullet
        transform.position = position;

        // day la vi tri tren cung ben phai man hinh
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        // neu bullet muon o ngoai man hinh
        if(transform.position.y > max.y)
        {
            Destroy(gameObject);
        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // phat hien va cham tau dich
        if(col.tag == "EnemyShipTag")
        {
            //pha huy dan
            Destroy (gameObject);
        }
    }
}
