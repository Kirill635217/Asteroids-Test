using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidsAssigment
{
    /// <summary>
    /// Manages the asteroid's behaviour
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class Asteroid : MonoBehaviour, IDamageable
    {
        /// <summary>
        /// How many times left to split before being completely destroyed
        /// </summary>
        private int splitsLeft;

        /// <summary>
        /// The tag of the bottom wall to destroy the asteroid when it hits it
        /// </summary>
        private const string WALL_TAG = "BottomWall";

        /// <summary>
        /// The tag of the player
        /// </summary>
        private const string PLAYER_TAG = "Player";

        /// <summary>
        /// Asteroid's rigidbody
        /// </summary>
        private Rigidbody2D rigidbody;

        [Tooltip("Asteroid's health, how many times it can be hit before being destroyed or split up")] [SerializeField]
        private int health = 1;

        [Tooltip("How many times the asteroid will split before being completely destroyed")] [SerializeField]
        private int splitAmount = 1;

        [Tooltip("How many pieces the asteroid will split into")] [SerializeField]
        private int splitPieces = 2;

        private void OnEnable()
        {
            transform.localScale = Vector3.one;
            splitsLeft = splitAmount;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag(WALL_TAG) || other.gameObject.CompareTag(PLAYER_TAG))
            {
                if (other.gameObject.TryGetComponent(out IDamageable damageable))
                    damageable.Hit();
                AsteroidsObjectPoolManager.Instance.ReturnObjectToPool(gameObject);
            }
        }

        /// <summary>
        /// Push the asteroid in a direction
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="force"></param>
        public void Push(Vector2 direction, float force)
        {
            if (rigidbody == null)
                rigidbody = GetComponent<Rigidbody2D>();
            rigidbody.AddForce(direction * force);
        }

        /// <summary>
        /// Set the amount of splits the asteroid will have
        /// </summary>
        /// <param name="splits"></param>
        public void SetSplits(int splits)
        {
            splitsLeft = splits;
        }

        /// <summary>
        /// Called when hit by a bullet. Decreases health and destroys the asteroid if health is 0 or less
        /// </summary>
        /// <param name="damage"></param>
        public void Hit(int damage = 1)
        {
            health -= damage;
            if (health <= 0)
                Destroy();
        }

        /// <summary>
        /// Destroys the asteroid and spawns new ones if splits are left
        /// </summary>
        private void Destroy()
        {
            if (splitsLeft <= 0)
            {
                AsteroidsObjectPoolManager.Instance.ReturnObjectToPool(gameObject);
                return;
            }

            for (int i = 0; i < splitPieces; i++)
            {
                var splitAsteroid = AsteroidsObjectPoolManager.Instance.GetObjectFromPool();
                splitAsteroid.transform.position = transform.position;
                splitAsteroid.transform.localScale = transform.localScale / 2;
                var asteroid = splitAsteroid.GetComponent<Asteroid>();
                asteroid.SetSplits(splitsLeft - 1);
                // Randomize the down direction a bit
                var direction = new Vector2(UnityEngine.Random.Range(-.5f, .5f), -1);
                asteroid.Push(direction.normalized, 100f);
            }

            AsteroidsObjectPoolManager.Instance.ReturnObjectToPool(gameObject);
        }
    }
}