using System.Collections;
using UnityEngine;

public class EnabledTrigger : MonoBehaviour
{

    private BoxCollider m_BoxCollider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_BoxCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void OnEnable()
    {
        StartCoroutine(WaitTrigger());
    }

    private IEnumerator WaitTrigger()
    {
        yield return new WaitForSeconds(2f);
        m_BoxCollider.isTrigger = true;
    }
}
