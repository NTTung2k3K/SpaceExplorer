using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    public GameObject EnemyBulletGO; // enemy bullet prefab


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // ban dan sau 1 giay
        Invoke("FireEnemyBullet", 1f);
    }

    // Update is called once per frame
   

    // ham de ban dan cua enemy
    void FireEnemyBullet()
    {
        // tham chieu den tau cua nguoi choi
        GameObject playerShip = GameObject.Find("PlayerGO");

        if (playerShip != null) // nguoi choi ko chet
        {
            //khoi tao ban dan
            GameObject bullet = (GameObject)Instantiate(EnemyBulletGO);

            // dat vi tri cua dan
            bullet.transform.position = transform.position;

            // tinh toan vi tri duong dan den nguoi dung
            Vector2 direction = playerShip.transform.position - bullet.transform.position;

            // dat vi tri duong dan
            bullet.GetComponent<EnemyBulletController>().SetDirection(direction);
        }
    }
}
