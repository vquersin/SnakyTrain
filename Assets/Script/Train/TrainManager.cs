using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrainManager : MonoBehaviour
{
    [SerializeReference] private EatSomething EatEvent;
    public GameObject Wagon;
    [SerializeReference] private TrainHead head;
    [SerializeReference] private RSO_Train TrainData;

    public List<Transform> List_Wagon = new List<Transform>();
    public int m_SpacingMultiplier = 4;

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
        NewInstantiateSegmentTrain();
    }

    private void LateUpdate()
    {
        for (int i = 0; i < List_Wagon.Count; i++)
        {
            int index = (i + 1) * m_SpacingMultiplier;

            if (index < head.m_PositionHistory.Count)
            {
                List_Wagon[i].position = Vector3.MoveTowards(List_Wagon[i].position, head.m_PositionHistory[index], TrainData.m_Speed * Time.deltaTime);
                float SpeedPercent = Mathf.InverseLerp(0f, 30f, TrainData.m_Speed);
                float CurrentSpeedRotate = TrainData.m_SpeedRotate.Evaluate(SpeedPercent) * 400f;
                if (i == 0)
                {
                    //cible = tête

                    Vector3 lastPos = head.m_PositionHistory[0] - List_Wagon[i].position;
                    Quaternion TargetRotation = Quaternion.LookRotation(lastPos);
                    Quaternion newRotate = Quaternion.RotateTowards(List_Wagon[i].rotation, TargetRotation, CurrentSpeedRotate * Time.deltaTime);
                    List_Wagon[i].rotation = newRotate;
                }
                else
                {
                    //cible = List_Wagon[i - 1]
                    
                    Vector3 lastPos =  List_Wagon[i-1].position - List_Wagon[i].position;
                    Quaternion TargetRotation = Quaternion.LookRotation(lastPos);
                    Quaternion newRotate = Quaternion.RotateTowards(List_Wagon[i].rotation,TargetRotation, CurrentSpeedRotate * Time.deltaTime);
                    List_Wagon[i].rotation = newRotate;
                }
            }
        }
    }

    private void NewInstantiateSegmentTrain()
    {
        int targetIndex = TrainData.Nb_Wagon * m_SpacingMultiplier;

        if (targetIndex >= head.m_PositionHistory.Count)
        {
            targetIndex = head.m_PositionHistory.Count - 1;
        }

        Vector3 spawnPos = head.m_PositionHistory[targetIndex];

        GameObject newWagon = Instantiate(
            Wagon,
            spawnPos,
            Quaternion.identity
        );

        List_Wagon.Add(newWagon.transform);
    }
}
