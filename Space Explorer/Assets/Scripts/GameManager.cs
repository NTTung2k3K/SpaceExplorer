using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Các đối tượng UI và game
    public GameObject playButton;
    public GameObject quitButton;
    public GameObject playerShip;
    public GameObject enemySpawner;
    public GameObject GameOverGO;
    public GameObject scoreUITextGO;
    public GameObject TimeCounterGO;
    public GameObject GameTitleGO;
    public GameObject asteroidGeneratorGO;
    public GameObject guidePanel;
    public GameObject[] objectsToDisable;
    public GameObject ImageLivesUiBg;
    public GameObject ImageScoresUiBg;
    public GameObject ImageTimeUiBg;
    public GameObject InfoButton;


    // Nút pause và hình ảnh của nó
    public Button pauseButton;
    public Sprite pauseSprite;    // Hình khi game đang chạy (hình pause)
    public Sprite resumeSprite;   // Hình khi game bị tạm dừng (hình continue)

    public enum GameManagerState { Opening, GamePlay, GameOver }
    GameManagerState GMState;

    public static bool IsGameOver = false;
    private bool isPaused = false;

    void Start()
    {
        GMState = GameManagerState.Opening;
        objectsToDisable = new GameObject[] { playButton, quitButton, ImageLivesUiBg, ImageScoresUiBg, ImageTimeUiBg, InfoButton };

        // Thêm listener cho nút pause
        if (pauseButton != null)
            pauseButton.onClick.AddListener(TogglePauseGame);
    }

    void Update()
    {
        // Nếu game đang trong trạng thái GamePlay, nhấn ESC sẽ toggle pause
        if (GMState == GameManagerState.GamePlay && Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseGame();
        }
    }
    
    void UpdateGameManagerState()
    {
        switch (GMState)
        {
            case GameManagerState.Opening:
                GameOverGO.SetActive(false);
                asteroidGeneratorGO.SetActive(false);
                GameTitleGO.SetActive(true);
                playButton.SetActive(true);
                InfoButton.SetActive(true);
                quitButton.SetActive(true);
                pauseButton.gameObject.SetActive(false); // Ẩn nút pause ở trạng thái Opening
                IsGameOver = false;
                break;

            case GameManagerState.GamePlay:
                scoreUITextGO.GetComponent<GameScore>().Score = 0;
                playButton.SetActive(false);
                InfoButton.SetActive(false);
                quitButton.SetActive(false);
                GameTitleGO.SetActive(false);
                asteroidGeneratorGO.SetActive(true);
                asteroidGeneratorGO.GetComponent<AsteroidGenerator>().canSpawn = true;
                asteroidGeneratorGO.GetComponent<AsteroidGenerator>().StartSpawning();
                playerShip.GetComponent<PlayerController>().Init();
                enemySpawner.GetComponent<EnemySpawner>().ScheduleEnemySpawner();
                TimeCounterGO.GetComponent<TimeCounter>().StartTimeCounter();
                pauseButton.gameObject.SetActive(true); // Hiển thị nút pause trong lúc chơi
                IsGameOver = false;
                break;

            case GameManagerState.GameOver:
                IsGameOver = true;
                TimeCounterGO.GetComponent<TimeCounter>().StopTimeCounter();
                enemySpawner.GetComponent<EnemySpawner>().UnscheduleEnemySpawner();
                GameOverGO.SetActive(true);
                quitButton.SetActive(true);
                pauseButton.gameObject.SetActive(false);
                Invoke("ChangeToOpeningState", 8f);
                break;
        }
    }

    public void SetGameManagerState(GameManagerState state)
    {
        GMState = state;
        UpdateGameManagerState();
    }

    // Phương thức gọi khi nhấn nút Play
    public void StartGamePlay()
    {
        SetGameManagerState(GameManagerState.GamePlay);
        playerShip.GetComponent<PlayerController>().currentBullets = 1;
    }

    // Chuyển về trạng thái Opening
    public void ChangeToOpeningState()
    {
        SetGameManagerState(GameManagerState.Opening);
        ResumeGame(); // Đảm bảo khi chuyển về Opening, game được resume
    }

    public void ToggleGuidePanel()
    {
        bool isActive = !guidePanel.activeSelf;
        guidePanel.SetActive(isActive);
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

    // Tự động gọi khi nhấn nút pause hoặc phím ESC
    public void TogglePauseGame()
    {
        if (isPaused)
            ResumeGame();
        else
            PauseGame();
    }

    private void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;  // Tạm dừng game

        // Vô hiệu hóa điều khiển của người chơi (ví dụ: không cho bắn, di chuyển)
        if (playerShip != null)
        {
            // Nếu PlayerController chịu trách nhiệm nhận input, vô hiệu hóa component đó
            playerShip.GetComponent<PlayerController>().enabled = false;
        }

        // Đổi hình nút pause sang hình resume (continue)
        if (pauseButton != null && resumeSprite != null)
            pauseButton.image.sprite = resumeSprite;
    }

    private void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;  // Tiếp tục game

        // Kích hoạt lại điều khiển của người chơi
        if (playerShip != null)
        {
            playerShip.GetComponent<PlayerController>().enabled = true;
        }

        // Đổi hình nút pause về hình pause
        if (pauseButton != null && pauseSprite != null)
            pauseButton.image.sprite = pauseSprite;
    }
}