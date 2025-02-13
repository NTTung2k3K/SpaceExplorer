using UnityEngine;
using System.Collections;

public class BackgroundSwitcher : MonoBehaviour
{
    public GameObject backgroundOld;
    public GameObject backgroundNew;
    public float switchInterval = 60f;

    private bool isUsingOld = true; // Ban đầu dùng background cũ

    void Start()
    {
        // Đảm bảo trạng thái ban đầu
        backgroundOld.SetActive(true);
        backgroundNew.SetActive(false);

        // Bắt đầu coroutine để chuyển background liên tục
        StartCoroutine(SwitchBackgroundRoutine());
    }

    IEnumerator SwitchBackgroundRoutine()
    {
        while (true)
        {
            // Chờ switchInterval giây (1 phút)
            yield return new WaitForSeconds(switchInterval);

            // Đảo trạng thái
            isUsingOld = !isUsingOld;

            // Bật/tắt gameobject tương ứng
            backgroundOld.SetActive(isUsingOld);
            backgroundNew.SetActive(!isUsingOld);
        }
    }
}
