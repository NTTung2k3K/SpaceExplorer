using UnityEngine;
using UnityEngine.UI;

public class TimeCounter : MonoBehaviour
{
    Text timeUI;

    float startTime; // thoi gian nguoi dung bat dau an nut play
    float ellapsedTime; // thoi gian da troi qua khi nguoi dung an nut play
    bool startCounter; // bat dau dem

    int minutes;
    int seconds;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startCounter = false;

        //lay ui text tu gameObject
        timeUI = GetComponent<Text>();
    }

    //ham bat dau dem thoi gian
    public void StartTimeCounter()
    {
        startTime = Time.time;
        startCounter = true;
    }

    //ham de dung dem thoi gian
    public void StopTimeCounter()
    {
        startCounter = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (startCounter)
        {
            //tinh thoi gian dem nguoc
            ellapsedTime = Time.time - startTime;

            minutes = (int)ellapsedTime / 60;
            seconds = (int)ellapsedTime % 60;

            //cap nhat thoi gian tren ui
            timeUI.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}
