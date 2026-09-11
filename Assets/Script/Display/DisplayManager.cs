using TMPro;
using UnityEngine;

public class DisplayManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_TestKM;
    [SerializeField] private TextMeshProUGUI m_TextHS;
    [SerializeField] private RSO_Train m_Speed;
    [SerializeField] private RSO_HighScore m_HS;

    private void Start()
    {
        m_TestKM.text = m_Speed.m_Speed.ToString()+"0 Km/h";
        m_TextHS.text = "High Score: " + m_HS.HighScore.ToString();
    }

    private void Update()
    {
        m_TestKM.text = m_Speed.m_Speed.ToString() + "0 Km/h";
        m_TextHS.text = "High Score: " + m_HS.HighScore.ToString();
    }
}
