using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TrainManager : MonoBehaviour
{
    [SerializeReference]private TrainHead head;
    public List<Transform> List_Wagon = new List<Transform>();
    public int m_SpacingMultiplier = 5;

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }


    void LateUpdate()
    {
        
    }
}
