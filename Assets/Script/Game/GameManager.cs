using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private RSO_Train TrainData;
    
    // Update is called once per frame
    void OnEnable()
    {
        TrainData.ResetToDefaults();
    }
}
