using Assets.Script.Audio;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TrainManager : MonoBehaviour
{
    [SerializeReference] private EatSomething EatEvent;
    [SerializeReference] private WagonSpeedMusic WagonSpeedMusicEvent;
    public GameObject Wagon;
    [SerializeReference] private TrainHead head;
    [SerializeReference] private RSO_Train TrainData;
    [SerializeReference] private AudioSource SwitchSpeedMode;
    [SerializeReference] private ParticleSystem LeftWheel;
    [SerializeReference] private ParticleSystem RightWheel;

    public List<Transform> List_Wagon = new List<Transform>();
    public int m_SpacingMultiplier = 4;
    private int SpeedModeNumber = 0;

    public TextMeshProUGUI TrainDisplay;

    private void Awake()
    {
        TrainDisplay.text = "0";
        SpeedModeNumber = UnityEngine.Random.Range(8,15);
    }

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
        TrainDisplay.text = TrainData.Nb_Wagon.ToString();
        NewInstantiateSegmentTrain();
        GetComponent<AudioSource>().Play();
        
        if (TrainData.Nb_Wagon == SpeedModeNumber)
        {
            SwitchSpeedMode.Play();
            WagonSpeedMusicEvent.SpeedWagonlimit();
            TrainData.m_Speed += 5;
            LeftWheel.Play();
            RightWheel.Play();
        }
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
        if(List_Wagon.Count == 0)
        {
            Quaternion RotateNewSegment = head.transform.rotation;
            GameObject newWagon = Instantiate(
            Wagon,
            spawnPos,
            RotateNewSegment
            );
            List_Wagon.Add(newWagon.transform);
        }
        else
        {
            Quaternion RotateNewSegment = List_Wagon[^1].rotation;
            GameObject newWagon = Instantiate(
                Wagon,
                spawnPos,
                RotateNewSegment
            );
            List_Wagon.Add(newWagon.transform);
        }


        
    }
}
