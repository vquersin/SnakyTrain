using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TrainHead : MonoBehaviour
{
    [SerializeReference] private EatSomething m_Eat;
    [SerializeReference] private IsDead m_Die;
    public RSO_Train Speed;
    public InputActionReference Rotate;
    private float m_Spacing = 0.5f;
    public RSO_Train Train_Wagon;

    public Vector3 m_LastHeadPosition;
    public List<Vector3> m_PositionHistory = new List<Vector3>();

    public TrainManager m_TrainManager;

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
        float distance = Vector3.Distance(transform.position, m_LastHeadPosition);

        if (distance >= m_Spacing)
        {
            int steps = Mathf.FloorToInt(distance / m_Spacing);

            Vector3 direction = (transform.position - m_LastHeadPosition).normalized;

            for (int i = 1; i <= steps; i++)
            {
                Vector3 newPosition = m_LastHeadPosition + direction * m_Spacing;

                m_PositionHistory.Insert(0, newPosition);

                m_LastHeadPosition = newPosition;
            }

            int maxHistory = (Train_Wagon.Nb_Wagon + 1) * m_TrainManager.m_SpacingMultiplier;

            while (m_PositionHistory.Count > maxHistory)
            {
                m_PositionHistory.RemoveAt(m_PositionHistory.Count - 1);
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
            if (m_Die != null)
            {
                m_Die.JustDie();
            }
            SceneManager.LoadScene("GameScene");
        }
    }
}
