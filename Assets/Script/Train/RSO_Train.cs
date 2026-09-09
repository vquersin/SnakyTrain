using UnityEngine;

[CreateAssetMenu(fileName = "Train", menuName = "TrainData")]
public class RSO_Train : ScriptableObject
{
    public float m_Speed = 3f;
    public AnimationCurve m_SpeedRotate;
    public int Nb_Wagon = 0;

    public void ResetToDefaults()
    {
        m_Speed = 6f;
        Nb_Wagon = 0;
    }
}
