using System;
using UnityEngine;

[CreateAssetMenu]
public class RSO_PositionMapValidity : ScriptableObject
{
    public event Action<bool,Vector3,RSO_MapEntity> ValidityPosition;

    public void Validity(bool valided, Vector3 validedPos, RSO_MapEntity entity)
    {
        ValidityPosition?.Invoke(valided, validedPos, entity);
    }
}