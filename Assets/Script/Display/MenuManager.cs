using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_HStext;

    public void OnEnable()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            m_HStext.text = "HighScore: "+PlayerPrefs.GetInt("HighScore", 0).ToString();
        }
        else
        {
            m_HStext.text = "HighScore: 0";
        }
    }
    public void LoadGame()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
