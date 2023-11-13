using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AsteroidsAssigment
{
    /// <summary>
    /// Handles the player behaviour, health and damage
    /// </summary>
    public class Player : MonoBehaviour, IDamageable
    {
        /// <summary>
        /// Player health
        /// </summary>
        private int health = 3;

        /// <summary>
        /// Player movement reference
        /// </summary>
        private PlayerMovement playerMovement;

        /// <summary>
        /// Player gun reference
        /// </summary>
        private Gun gun;

        /// <summary>
        /// Called when the player is destroyed
        /// </summary>
        private UnityEvent onDestroyed;

        /// <summary>
        /// Called when the player is hit
        /// </summary>
        private UnityEvent onHit;

        /// <summary>
        /// Player health
        /// </summary>
        public int Health => health;

        private void Awake()
        {
            // Get the references
            playerMovement = GetComponent<PlayerMovement>();
            gun = GetComponent<Gun>();
        }

        /// <summary>
        /// Subscribe to the destroyed event, which is called when the player is destroyed
        /// </summary>
        /// <param name="action"></param>
        public void SubscribeToOnDestroyed(UnityAction action)
        {
            onDestroyed ??= new UnityEvent();
            onDestroyed.AddListener(action);
        }

        /// <summary>
        /// Subscribe to the hit event, which is called when the player is hit
        /// </summary>
        /// <param name="action"></param>
        public void SubscribeToOnHit(UnityAction action)
        {
            onHit ??= new UnityEvent();
            onHit.AddListener(action);
        }

        /// <summary>
        /// Unsubscribe to the destroyed event, which is called when the player is destroyed
        /// </summary>
        /// <param name="action"></param>
        public void UnsubscribeToOnHit(UnityAction action)
        {
            onHit?.RemoveListener(action);
        }

        /// <summary>
        /// Unsubscribe to the hit event, which is called when the player is hit
        /// </summary>
        /// <param name="action"></param>
        public void UnsubscribeToOnDestroyed(UnityAction action)
        {
            onDestroyed?.RemoveListener(action);
        }

        /// <summary>
        /// Disable the player movement and gun
        /// </summary>
        public void Disable()
        {
            playerMovement.enabled = false;
            gun.enabled = false;
        }

        /// <summary>
        /// Enable the player movement and gun and reset the health
        /// </summary>
        public void Enable()
        {
            playerMovement.enabled = true;
            gun.enabled = true;
            health = 3;
        }

        /// <summary>
        /// Called when the player is hit, decrease the health and call the onHit event
        /// </summary>
        /// <param name="damage"></param>
        public void Hit(int damage = 1)
        {
            health -= damage;
            onHit?.Invoke();
            if (health <= 0)
                Destroy();
        }

        /// <summary>
        /// Call the onDestroyed event
        /// </summary>
        private void Destroy()
        {
            onDestroyed?.Invoke();
        }
    }
}