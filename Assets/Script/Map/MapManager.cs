using System.Collections;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    // Prefab Reference ...
    [SerializeField]private RSO_MapEntity m_Wall;
    [SerializeField] private RSO_MapEntity m_Collectible;
    public GameObject PeakPosTrigger;

    // Variables ...
    private int rangeMin = -28;
    private int rangeMax = 28;
    [SerializeField] private float TimerSpawn = 10f;

    // Event ...
    [SerializeField] private RSO_PositionMapValidity PositionValidity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // init first Wall et Collectibles .... Start Coroutine ....
        for(int i=0; i<5; i++)
        {
            PeakPosition(m_Wall);
        }
        PeakPosition(m_Collectible);
        StartCoroutine(SpawnWallTimer());
        StartCoroutine(SpawnCollectibleTimer());
    }

    private void OnEnable()
    {
        PositionValidity.ValidityPosition += SpawnerMapEntity;
    }

    private void OnDisable()
    {
        PositionValidity.ValidityPosition -= SpawnerMapEntity;
    }
    private void PeakPosition(RSO_MapEntity Entity)
    {
        int x = UnityEngine.Random.Range(rangeMin+1, rangeMax);
        int z = UnityEngine.Random.Range(rangeMin+1, rangeMax);
        Vector3 peakPos = new Vector3(x,1.5f,z);
        GameObject trigger = Instantiate(PeakPosTrigger, peakPos, Quaternion.identity);

        TriggPeaking triggerScript = trigger.GetComponent<TriggPeaking>();
        triggerScript.Initialize(Entity);
    }

    private void SpawnerMapEntity(bool ValiditySpawner,Vector3 validedPos, RSO_MapEntity entity)
    {
        if (!ValiditySpawner)
            return;

        if (entity == null)
        {
            Debug.LogError("❌ ENTITY EST NULL !");
            return;
        }

        if (entity.Entity == null)
        {
            Debug.LogError("❌ entity.Entity EST NULL !");
            return;
        }
        if (ValiditySpawner == true)
        {
            Debug.Log("Entity RSO : " + entity);
            Debug.Log("Prefab dans Entity : " + entity.Entity);
            // Instantiate l'argument donné selon coroutine
            Instantiate(entity.Entity, validedPos,Quaternion.identity);
        }
        else
        {
            PeakPosition(entity);
        }
    }
    private IEnumerator SpawnWallTimer()
    {
        yield return new WaitForSeconds(TimerSpawn);
        PeakPosition(m_Wall);
        StartCoroutine(SpawnWallTimer());
    }
    private IEnumerator SpawnCollectibleTimer()
    {
        yield return new WaitForSeconds(TimerSpawn);
        PeakPosition(m_Collectible);
        StartCoroutine(SpawnCollectibleTimer());
    }
}