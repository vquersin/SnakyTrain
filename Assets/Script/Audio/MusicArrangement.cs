using Assets.Script.Audio;
using System.Collections;
using UnityEngine;

public class MusicArrangement : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource remixSource;


    [SerializeField] private float transitionDuration = 2f;

    [SerializeField] private RSO_Train TrainData;
    [SerializeReference] private WagonSpeedMusic WagonSpeedMusicEvent;

    private Coroutine transitionCoroutine;

    private void Awake()
    {

        
        musicSource.volume = 0.8f;
        musicSource.Play();

        remixSource.loop = musicSource.loop;
        remixSource.playOnAwake = false;
        remixSource.volume = 0f;
    }

    private void OnEnable()
    {
        WagonSpeedMusicEvent.SpeedMusic += WagonSpeedMusicEvent_SpeedMusic;
    }

    private void OnDisable()
    {
        WagonSpeedMusicEvent.SpeedMusic -= WagonSpeedMusicEvent_SpeedMusic;
    }

    private void WagonSpeedMusicEvent_SpeedMusic()
    {
        if (transitionCoroutine != null)
            StopCoroutine(transitionCoroutine);

        transitionCoroutine = StartCoroutine(SwitchMusic());
        Debug.Log("MusicSwitch");
    }

    private IEnumerator SwitchMusic()
    {
        remixSource.volume = 0f;
        remixSource.Play();

        float startVolume = musicSource.volume;

        float timer = 0f;

        while (timer < transitionDuration)
        {
            timer += Time.deltaTime;

            float t = timer / transitionDuration;

            musicSource.volume = Mathf.Lerp(startVolume, 0f, t);
            remixSource.volume = Mathf.Lerp(0f, startVolume, t);

            yield return null;
        }

        musicSource.Stop();
        musicSource.volume = startVolume;

        transitionCoroutine = null;
    }
}