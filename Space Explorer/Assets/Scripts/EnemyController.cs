using UnityEngine;

public class EnemyController : MonoBehaviour
{
    GameObject scoreUITextGO;// khoi tao text score Ui 
    public GameObject ExplosionGO;

    float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = 2f; //set mac dinh speed

        //lay gia tri diem
        scoreUITextGO = GameObject.FindGameObjectWithTag("ScoreTextTag");
    }

    // Update is called once per frame
    void Update()
    {
        //lay vi tri cua enemy
        Vector2 position = transform.position;

        //tinh toan vi tri moi
        position = new Vector2 (position.x, position.y - speed * Time.deltaTime);
    
        //cap nhat vi tri moi
        transform.position = position;

        // day la vi tri ben trai duoi cua man hinh
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2 (0,0));

        // neu enemy ra khoi man hinh, pha huy chung
        if(transform.position.y < min.y)
        {
            Destroy (gameObject);
        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // phat hien va cham voi dich hoac dan cua dich
        if ((col.tag == "PlayerShipTag") || (col.tag == "PlayerBulletTag"))
        {
            PlayExplosion();

            // them 100 diem cho moi enemy bi ha
            scoreUITextGO.GetComponent<GameScore>().Score += 200;

            Destroy(gameObject);// huy tau
        }
    }

    // ham tao vu no
    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(ExplosionGO);

        //dat vi tri vu no
        explosion.transform.position = transform.position;
    }
}
