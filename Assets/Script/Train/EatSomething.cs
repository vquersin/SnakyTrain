using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Eat", menuName = "EatEvent")]
public class EatSomething : ScriptableObject
    {
        public event Action Eat;
        public void EatCollectible()
        {
            Eat?.Invoke();
        }
    }