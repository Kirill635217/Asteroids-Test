using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidsAssigment
{
    /// <summary>
    /// Handles the bullet movement and collision
    /// </summary>
    public abstract class Bullet : MonoBehaviour
    {
        /// <summary>
        /// Use FixedUpdate instead of Update for movement
        /// </summary>
        protected bool useFixedUpdate;

        [Tooltip("How long the bullet will last before being destroyed")] [SerializeField]
        private float decayTime = 5f;

        [Tooltip("Collision mask for the bullet to collide with")] [SerializeField]
        private LayerMask collisionMask;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!IsLayerInMask(other.gameObject.layer)) return;
            if (other.TryGetComponent(out IDamageable damageable))
                damageable.Hit();
            OnHit();
        }

        /// <summary>
        /// Is the layer in the collision mask
        /// </summary>
        /// <param name="layer"></param>
        /// <returns></returns>
        private bool IsLayerInMask(int layer)
        {
            return (collisionMask.value & (1 << layer)) != 0;
        }

        private void Update()
        {
            if (!useFixedUpdate)
                Move();
            HandleDecay();
        }

        private void FixedUpdate()
        {
            if (useFixedUpdate)
                Move();
        }

        /// <summary>
        /// Handle the decay of the bullet
        /// </summary>
        private void HandleDecay()
        {
            decayTime -= Time.deltaTime;
            if (decayTime <= 0)
                Destroy(gameObject);
        }

        /// <summary>
        /// Move the bullet
        /// </summary>
        protected abstract void Move();

        /// <summary>
        /// Called when the bullet hits something
        /// </summary>
        protected abstract void OnHit();
    }
}