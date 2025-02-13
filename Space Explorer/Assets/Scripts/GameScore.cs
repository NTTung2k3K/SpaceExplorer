using UnityEngine;
using UnityEngine.UI;

public class GameScore : MonoBehaviour
{
    Text scoreTextUI;

    int score;

    public int Score
    {
        get { return score; }
        set 
        {
            this.score = value;
            UpdateScoreTextUI();
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // lay component Text UI tu gameObject
        scoreTextUI = GetComponent<Text>();
    }

    // ham update score text
    void UpdateScoreTextUI()
    {
        string scoreStr = string.Format("{0:00000000}", score);
        scoreTextUI.text = scoreStr;
    }

}
