using UnityEngine;

public class Star : MonoBehaviour
{
    public float speed;// toc do ngoi sao

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //lay vi tri hien tai cua sao
        Vector2 position = transform.position;

        //tinh toan vi tri moi cua sao
        position = new Vector2(position.x, position.y + speed * Time.deltaTime);

        //cap nhat vi tri sao
        transform.position = position;

        //day la vi tri duoi trai man hinh
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0,0));

        //day la vi tri cao phai man hinh
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        // neu ngoi sao chay xuong duoi ra khoi man hinh
        //thi se hien lai o phia tren man hinh
        //va ngau nhien tu phia ben trai va phai
        if (transform.position.y < min.y)
        {
            transform.position = new Vector2(Random.Range(min.x,max.x),max.y);
        }
    }
}
