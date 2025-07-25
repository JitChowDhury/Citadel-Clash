using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    void Awake()
    {
        transform.Find("playBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            GameSceneManager.Load(GameSceneManager.Scene.GameScene);
        });
        transform.Find("quitBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
}
