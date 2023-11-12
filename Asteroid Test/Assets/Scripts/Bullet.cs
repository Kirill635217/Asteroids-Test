using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidsAssigment
{
    public abstract class Bullet : MonoBehaviour
    {
        protected bool useFixedUpdate;

        [SerializeField] private float decayTime = 5f;
        [SerializeField] private LayerMask collisionMask;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer != collisionMask) return;
            if(other.TryGetComponent(out IDamageable damageable))
                damageable.Hit();
            OnHit();
        }

        private void Update()
        {
            if(!useFixedUpdate)
                Move();
            HandleDecay();
        }

        private void FixedUpdate()
        {
            if(useFixedUpdate)
                Move();
        }

        private void HandleDecay()
        {
            decayTime -= Time.deltaTime;
            if (decayTime <= 0)
                Destroy(gameObject);
        }

        protected abstract void Move();
        protected abstract void OnHit();
    }
}
