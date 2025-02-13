using UnityEngine;

public class StarGenerator : MonoBehaviour
{
    public GameObject StarGO;// khoi tao star
    public int MaxStars; // so luong toi da ngoi sao

    //mang mau 
    Color[] starColors =
    {
        new Color(0.5f,0.5f,1f),//blue
        new Color(0,1f,1f),//green
        new Color(1f,1f,0),//yellow
        new Color(1f,0,0),//red
    };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // vi tri cuoi trai man hinh
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0,0));

        //vi tri dau phai man hinh
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1,1));

        //vong lap tao ngoi sao
        for(int i = 0; i <MaxStars; ++i)
        {
            GameObject star = (GameObject)Instantiate(StarGO);

            // dat gia tri mau ngoi sao
            star.GetComponent<SpriteRenderer>().color = starColors[i % starColors.Length];

            //dat vi tri sao (ngau nhien x,y)
            star.transform.position = new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));

            //dat ngau nhien toc do ngoi sao
            star.GetComponent<Star>().speed = -(1f * Random.value + 0.5f);

            //dat ngoi sao la con cua StarGeneratorGO
            star.transform.parent = transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
