using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private RSO_Train TrainData;
    [SerializeField] private RSO_HighScore m_HS;
    [SerializeField] private IsDead DieEvent;

    public InputActionReference QuitGame;

    // Update is called once per frame
    void OnEnable()
    {
        TrainData.ResetToDefaults();
        Application.targetFrameRate = 60;
        DieEvent.Die += OnDie;
        QuitGame.action.performed += OnQuitGame;
        if (PlayerPrefs.HasKey("HighScore"))
        {
            m_HS.HighScore = PlayerPrefs.GetInt("HighScore", 0);
        }
        else
        {
            Debug.Log("Première partie !");
        }
    }
    private void OnDisable()
    {
        DieEvent.Die -= OnDie;
        QuitGame.action.performed -= OnQuitGame;
    }

    private void OnQuitGame(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene("Menu");
    }

    private void OnDie()
    {
        SaveHighScore(TrainData.Nb_Wagon);
        SceneManager.LoadScene("GameScene");
    }
    public void SaveHighScore(int score)
    {
        int currentHighScore = PlayerPrefs.GetInt("HighScore", 0);

        if (score > currentHighScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
        }
    }
}
