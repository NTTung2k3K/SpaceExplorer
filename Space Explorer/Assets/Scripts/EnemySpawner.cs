using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyGO; // enemy prefab

    float maxSpawnRateInSeconds = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnEnemy()
    {
        // vi tri trai duoi cuar man hinh
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        // vi tri phai tren cua nguoi dung
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        // tao enemy ngau nhien
        GameObject anEnemy = (GameObject)Instantiate(EnemyGO);
        anEnemy.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);

        //Tao thoi gian xuat hien cho enemy tiep theo
        ScheduleNextEnemySpawn();
    }

    void ScheduleNextEnemySpawn()
    {
        float spawnInNSeconds;

        if(maxSpawnRateInSeconds > 1f)
        {
            // tao so bat ki tu 1 toi maxSpawn...
                spawnInNSeconds = Random.Range(1f, maxSpawnRateInSeconds);
        }
        else
        {
            spawnInNSeconds = 1f;
        }

        Invoke("SpawnEnemy", spawnInNSeconds);
    }

    // ham tang do kho cua game
    void IncreaseSpawnRate()
    {
        if (maxSpawnRateInSeconds > 1f) { maxSpawnRateInSeconds--; }
        if (maxSpawnRateInSeconds == 1f) { CancelInvoke("IncreateSpawnRate"); }

    }

    //ham khoi tao enemy
    public void ScheduleEnemySpawner()
    {
        //dat lai ti le spawn
        maxSpawnRateInSeconds = 5f;

        Invoke("SpawnEnemy", maxSpawnRateInSeconds);

        //gia tang ti le ra ke thu tang len moi 30 giay
        InvokeRepeating("IncreaseSpawnRate", 0f, 30f);
    }

    // ham dung viec tao enemy
    public void UnscheduleEnemySpawner()
    {
        CancelInvoke("SpawnEnemy");
        CancelInvoke("IncreaseSpawnRate");
    }
}
