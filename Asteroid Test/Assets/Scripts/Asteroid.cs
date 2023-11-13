using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidsAssigment
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Asteroid : MonoBehaviour, IDamageable
    {
        private int splitsLeft;

        private const string WALL_TAG = "BottomWall";
        private const string PLAYER_TAG = "Player";
        private Rigidbody2D rigidbody;

        [SerializeField] private int health = 1;
        [Tooltip("How many times the asteroid will split before being completely destroyed")]
        [SerializeField] private int splitAmount = 1;
        [Tooltip("How many pieces the asteroid will split into")]
        [SerializeField] private int splitPieces = 2;

        private void OnEnable()
        {
            transform.localScale = Vector3.one;
            splitsLeft = splitAmount;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag(WALL_TAG) || other.gameObject.CompareTag(PLAYER_TAG))
            {
                if(other.gameObject.TryGetComponent(out IDamageable damageable))
                    damageable.Hit();
                AsteroidsObjectPoolManager.Instance.ReturnObjectToPool(gameObject);
            }
        }

        public void Push(Vector2 direction, float force)
        {
            if (rigidbody == null)
                rigidbody = GetComponent<Rigidbody2D>();
            rigidbody.AddForce(direction * force);
        }

        public void SetSplits(int splits)
        {
            splitsLeft = splits;
        }

        public void Hit(int damage = 1)
        {
            health -= damage;
            if (health <= 0)
                Destroy();
        }

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
                var direction = new Vector2(UnityEngine.Random.Range(-.5f, .5f), -1);
                asteroid.Push(direction.normalized, 100f);
            }
            AsteroidsObjectPoolManager.Instance.ReturnObjectToPool(gameObject);
        }
    }
}