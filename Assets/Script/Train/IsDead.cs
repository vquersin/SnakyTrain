using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Dead", menuName = "DieEvent")]
    public class IsDead : ScriptableObject
    {
        public event Action Die;
        public void JustDie()
        {
            Die?.Invoke();
        }
    }
