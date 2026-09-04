using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class TrainHead : MonoBehaviour
{
    public RSO_Train Speed;
    public InputActionReference Rotate;
    public GameObject Wagon;
    private float m_Spacing = 0.5f;
    public RSO_Train Train_Wagon;

    private Vector3 m_LastHeadPosition;
    private List<Vector3> m_PositionHistory = new List<Vector3>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_LastHeadPosition= transform.position;
        m_PositionHistory.Add(m_LastHeadPosition);
    }

    // Update is called once per frame
    void Update()
    {
        // Mouvement ...
        float SpeedPercent = Mathf.InverseLerp(0f, 30f, Speed.m_Speed);
        float CurrentSpeedRotate = Speed.m_SpeedRotate.Evaluate(SpeedPercent) * 400f;
        transform.Translate(Vector3.forward * Speed.m_Speed * Time.deltaTime);
        Vector2 StickDirection = Rotate.action.ReadValue<Vector2>();
        Vector3 RotateDirection = new Vector3(0, StickDirection.x, 0);
        transform.Rotate(RotateDirection * CurrentSpeedRotate * Time.deltaTime);

        // Listage des positions ...
        if (Vector3.Distance(transform.position,m_LastHeadPosition)>= m_Spacing)
        {
            m_PositionHistory.Insert(0,transform.position);
            m_LastHeadPosition = transform.position;

            if(m_PositionHistory.Count > Train_Wagon.Nb_Wagon)
            {
                m_PositionHistory.RemoveAt(m_PositionHistory.Count -1);
            }
        }
    }
}
