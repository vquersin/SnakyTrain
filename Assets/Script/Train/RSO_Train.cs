using UnityEngine;

[CreateAssetMenu(fileName = "Train", menuName = "TrainData")]
public class RSO_Train : ScriptableObject
{
    public float m_Speed = 3f;
    public AnimationCurve m_SpeedRotate;
    public int Nb_Wagon = 0;
}
