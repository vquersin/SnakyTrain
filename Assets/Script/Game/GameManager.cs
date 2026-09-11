using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private RSO_Train TrainData;
    [SerializeField] private RSO_HighScore m_HS;
    [SerializeField] private IsDead DieEvent;

    // Update is called once per frame
    void OnEnable()
    {
        TrainData.ResetToDefaults();
        Application.targetFrameRate = 60;
        //m_HS.HighScore = 
    }
}
