using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    public static GameOverUI Instance { get; private set; }
    void Awake()
    {
        Instance = this;
        transform.Find("retryButton").GetComponent<Button>().onClick.AddListener(() =>
        {

            Time.timeScale = 1;
            GameSceneManager.Load(GameSceneManager.Scene.GameScene);
        });

        transform.Find("mainMenuButton").GetComponent<Button>().onClick.AddListener(() =>
        {
            Time.timeScale = 1;
            GameSceneManager.Load(GameSceneManager.Scene.MainMenuScene);

        });

        Hide();

    }

    public void Show()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0;
        transform.Find("waveSurvivedText").GetComponent<TextMeshProUGUI>().SetText("You Survived " + EnemyWaveManager.Instance.GetWaveNumber() + " Waves!");
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
