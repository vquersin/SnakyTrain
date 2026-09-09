using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // Reference ...
    [SerializeField] private Transform TrainHeadPosition;
    // Variables ...
    [SerializeField] private float lookAhead = 5f;
    [SerializeField] private float HauteurCam = 30f;

    private Quaternion InitRotate;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Position Cam ...
        Vector3 CamPos = new Vector3(TrainHeadPosition.position.x + lookAhead, HauteurCam, TrainHeadPosition.position.z);
        transform.position = CamPos;
        InitRotate = transform.rotation; 
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (TrainHeadPosition != null)
        {
            // Position Cam ...
            Vector3 CamPos = new Vector3(TrainHeadPosition.position.x + lookAhead, HauteurCam, TrainHeadPosition.position.z);
            transform.position = CamPos;
        }
        else return;
    }
}
