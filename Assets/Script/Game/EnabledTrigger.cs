using System.Collections;
using UnityEngine;

public class EnabledTrigger : MonoBehaviour
{

    private BoxCollider m_BoxCollider;
    private float TimerDestroy = 80f;
    private Animator m_Animator;
    private void Awake()
    {
        m_BoxCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    private void OnEnable()
    {
        m_Animator = GetComponentInChildren<Animator>(true);
        TimerDestroy = Random.Range(60f, 120f);


        StartCoroutine(WaitTrigger());
        StartCoroutine(WaitDestroy());
    }

    private IEnumerator WaitTrigger()
    {
        yield return new WaitForSeconds(2f);
        m_BoxCollider.isTrigger = true;
    }

    private IEnumerator WaitDestroy()
    {
        yield return new WaitForSeconds(TimerDestroy - 2f);

        if (m_Animator == null)
        {
            yield break;
        }

        m_Animator.Play("WallDisappear", 0, 0f);

        yield return new WaitForSeconds(2f);

        Destroy(gameObject);
    }
}
