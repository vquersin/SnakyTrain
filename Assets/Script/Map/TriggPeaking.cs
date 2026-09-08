using System;
using UnityEngine;

public class TriggPeaking : MonoBehaviour
{
    [SerializeField]private RSO_PositionMapValidity PositionValidity;
    [SerializeField] private LayerMask Obstacles;

    private RSO_MapEntity Entity;

    public void Initialize(RSO_MapEntity entity)
    {
        Entity = entity;
    }

    private void Start()
    {
        bool isOccupied = Physics.CheckBox(transform.position,transform.localScale / 2f,Quaternion.identity, Obstacles);
        Vector3 validedPos = transform.position;
        PositionValidity.Validity(!isOccupied, validedPos, Entity);
        Destroy(gameObject);
    }
}
