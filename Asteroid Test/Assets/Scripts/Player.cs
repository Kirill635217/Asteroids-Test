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

        private PlayerMovement playerMovement;
        private Gun gun;

        private UnityEvent onDestroyed;
        private UnityEvent onHit;

        public int Health => health;

        private void Awake()
        {
            playerMovement = GetComponent<PlayerMovement>();
            gun = GetComponent<Gun>();
        }

        public void SubscribeToOnDestroyed(UnityAction action)
        {
            onDestroyed ??= new UnityEvent();
            onDestroyed.AddListener(action);
        }

        public void SubscribeToOnHit(UnityAction action)
        {
            onHit ??= new UnityEvent();
            onHit.AddListener(action);
        }

        public void UnsubscribeToOnHit(UnityAction action)
        {
            onHit?.RemoveListener(action);
        }

        public void UnsubscribeToOnDestroyed(UnityAction action)
        {
            onDestroyed?.RemoveListener(action);
        }

        public void Disable()
        {
            playerMovement.enabled = false;
            gun.enabled = false;
        }

        public void Enable()
        {
            playerMovement.enabled = true;
            gun.enabled = true;
            health = 3;
        }

        public void Hit(int damage = 1)
        {
            health -= damage;
            onHit?.Invoke();
            if (health <= 0)
                Destroy();
        }

        private void Destroy()
        {
            onDestroyed?.Invoke();
        }
    }
}
