using System;
using System.Collections.Generic;
using UnityEngine;

public class TrainManager : MonoBehaviour
{
    [SerializeReference] private EatSomething EatEvent;
    [SerializeReference] private TrainHead head;
    [SerializeReference] private RSO_Train TrainData;

    public List<Transform> List_Wagon = new List<Transform>();
    public int m_SpacingMultiplier = 5;

    private void OnEnable()
    {
        EatEvent.Eat += OnEat;
    }

    private void OnDisable()
    {
        EatEvent.Eat -= OnEat;
    }

    private void OnEat()
    {
        Debug.Log("J'ai mangé!");
        TrainData.Nb_Wagon++;
        TrainData.m_Speed++;
    }

    void LateUpdate()
    {
        
    }
}
