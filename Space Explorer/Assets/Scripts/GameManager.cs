﻿using System.Security.Cryptography;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // dat cac object
    public GameObject playButton;
    public GameObject quitButton;
    public GameObject playerShip;
    public GameObject enemySpawner;// khoi tao enemy
    public GameObject GameOverGO; // tao hinh gover img
    public GameObject scoreUITextGO;// khoi tao diem
    public GameObject TimeCounterGO; // khoi tao thoi gian
    public GameObject GameTitleGO; // khoi tao title
    public GameObject asteroidGeneratorGO;
    public GameObject guidePanel; // Bảng thông tin hướng dẫn
    public GameObject[] objectsToDisable; // Danh sách các object cần disable
    public GameObject ImageLivesUiBg; // Tạo một nền mờ
    public GameObject ImageScoresUiBg; // Tạo một nền mờ
    public GameObject ImageTimeUiBg; // Tạo một nền mờ
    public GameObject InfoButton; // Tạo một nền mờ



    public enum GameManagerState
    {
        Opening,
        GamePlay,
        GameOver
    }

    GameManagerState GMState;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GMState = GameManagerState.Opening;
        objectsToDisable = new GameObject[] { playButton, quitButton, ImageLivesUiBg, ImageScoresUiBg, ImageTimeUiBg, InfoButton };
    }

    // ham cap nhat vi tri quan li tro choi
    void UpdateGameManagerState()
    {
        switch (GMState) 
        {
            case GameManagerState.Opening:

                //An di game over
                GameOverGO.SetActive(false);

                // an di thien thach 
                asteroidGeneratorGO.SetActive(false);

                // hien thi title game
                GameTitleGO.SetActive(true);

                //Hien thi nut play (active)
                playButton.SetActive(true);
                InfoButton.SetActive(true);


                //Hien thi nut quit (active)
                quitButton.SetActive(true);

                break;
            case GameManagerState.GamePlay:
                //dat lai diem 
                scoreUITextGO.GetComponent<GameScore>().Score = 0;

                // an di nut play khi vao game
                playButton.SetActive(false);
                InfoButton.SetActive(false);


                //an di nut quit khi vao game
                quitButton.SetActive(false);

                //an di title game
                GameTitleGO.SetActive(false);

                // hien thi thien thach
                asteroidGeneratorGO.SetActive(true);

                // Cho phép spawn thiên thạch
                asteroidGeneratorGO.GetComponent<AsteroidGenerator>().canSpawn = true;
                asteroidGeneratorGO.GetComponent<AsteroidGenerator>().StartSpawning();

                //hien thi nguoi choi va tao so mang
                playerShip.GetComponent<PlayerController>().Init();

                //bat dau tao enemy
                enemySpawner.GetComponent<EnemySpawner>().ScheduleEnemySpawner();

                //bat dau dem gio
                TimeCounterGO.GetComponent<TimeCounter>().StartTimeCounter();
                break;

            case GameManagerState.GameOver:

                //dung dem gio
                TimeCounterGO.GetComponent<TimeCounter>().StopTimeCounter();

                //dung tao ra enemy
                enemySpawner.GetComponent<EnemySpawner>().UnscheduleEnemySpawner();

                //hien thi game over
                GameOverGO.SetActive(true);

                //hien nut quit khi game over
                quitButton.SetActive(true);

                //thay doi trang thai ve Opening sau 8 giay
                Invoke("ChangeToOpeningState", 8f);
                
                break;
        }
    }

    // ham dat trang thai quan ly tro choi
    public void SetGameManagerState(GameManagerState state)
    {
        GMState = state;
        UpdateGameManagerState();
    }

    //Nut Play duoc goi o day
    // nguoi dung an nut 
    public void StartGamePlay()
    {
        GMState = GameManagerState.GamePlay;
        UpdateGameManagerState();
        playerShip.GetComponent<PlayerController>().currentBullets = 1;
    }

    // ham thay doi giao dien tro choi sang opening
    public void ChangeToOpeningState()
    {
        SetGameManagerState(GameManagerState.Opening);
    }
    public void ToggleGuidePanel()
    {
        bool isActive = !guidePanel.activeSelf; // Kiểm tra trạng thái hiện tại

        guidePanel.SetActive(isActive); // Bật/tắt panel

        // Disable hoặc enable các object khác
        foreach (GameObject obj in objectsToDisable)
        {
            obj.SetActive(!isActive);
        }
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
