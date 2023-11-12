using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AsteroidsAssigment
{
    public class Player : MonoBehaviour, IDamageable
    {
        private int health = 3;

        private UnityEvent OnDestroyed;

        private void Awake()
        {

        }

        public void SubscribeToOnDestroyed(UnityAction action)
        {
            OnDestroyed ??= new UnityEvent();
            OnDestroyed.AddListener(action);
        }

        public void UnsubscribeToOnDestroyed(UnityAction action)
        {
            OnDestroyed?.RemoveListener(action);
        }

        public void Hit(int damage = 1)
        {
            health -= damage;
            if (health <= 0)
                Destroy();
        }

        private void Destroy()
        {
            OnDestroyed?.Invoke();
        }
    }
}
