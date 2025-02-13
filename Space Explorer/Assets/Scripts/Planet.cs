using UnityEngine;

public class Planet : MonoBehaviour
{
    public float speed;// planet speed
    public bool isMoving;//flag to make planet scroll

    Vector2 min;// vi tri cuoi trai man hinh
    Vector2 max;// vi tri dau phai man hinh

    void Awake()
    {
        isMoving = false;

        min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        // add the planet sprite half height to max y
        max.y = max.y + GetComponent<SpriteRenderer>().sprite.bounds.extents.y;

        // subtract the planet sprite half height to min y
        min.y = min.y - GetComponent<SpriteRenderer>().sprite.bounds.extents.y;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving) { return; }

        //lay vi tri hien tai cua hanh tinh
        Vector2 position = transform.position;

        // tinh toan vi tri moi 
        position = new Vector2 (position.x, position.y + speed * Time.deltaTime);

        // cap nhat vi tri moi
        transform.position = position;

        //neu hanh tinh lay vi tri toi thieu y thi dung di chuyen hanh tinh
        if (transform.position.y < min.y)
        {
            isMoving = false ;
        }
    }

    //ham dat lai vi tri hanh tinh
    public void ResetPosition()
    {
        //dat lai vi tri hanh tinh theo ngau nien x va toi da y
        transform.position = new Vector2(Random.Range(min.x,max.x),max.y);
    }
}
