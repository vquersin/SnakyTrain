using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TrainHead : MonoBehaviour
{
    [SerializeReference] private EatSomething m_Eat;
    public RSO_Train Speed;
    public InputActionReference Rotate;
    public GameObject Wagon;
    private float m_Spacing = 0.5f;
    public RSO_Train Train_Wagon;

    private Vector3 m_LastHeadPosition;
    private List<Vector3> m_PositionHistory = new List<Vector3>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        m_LastHeadPosition= transform.position;
        m_PositionHistory.Add(m_LastHeadPosition);
    }

    // Update is called once per frame
    private void Update()
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectible"))
        {
            if (m_Eat != null)
            {
                m_Eat.EatCollectible();
            }
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Wall"))
        {
            Debug.Log("Mort! Mort! Mort!");
            Destroy(transform.parent.gameObject);
        }
    }
}
