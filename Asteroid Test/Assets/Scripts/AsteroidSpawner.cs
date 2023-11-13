using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidsAssigment
{
    /// <summary>
    /// Handles spawning of asteroids
    /// </summary>
    public class AsteroidSpawner : MonoBehaviour
    {
        /// <summary>
        /// Spawn timer till next spawn
        /// </summary>
        private float timer;

        /// <summary>
        /// Can spawn new asteroid
        /// </summary>
        private bool isSpawning;

        [Tooltip("How long to wait between spawns")] [SerializeField]
        private float timeBetweenSpawns = 1f;

        [Tooltip("The spawn point for the asteroids")] [SerializeField]
        private Transform spawnPoint;

        [Tooltip("The x bounds for the spawn point, the asteroid will spawn randomly between these bounds")]
        [SerializeField]
        private Vector2 spawnXBounds;

        /// <summary>
        /// Singleton instance
        /// </summary>
        public static AsteroidSpawner Instance;

        private void Awake()
        {
            SetSingleton();
        }

        private void Start()
        {
            AsteroidsObjectPoolManager.Instance.SubscribeToOnObjectReturnedToPool(StartSpawning);
        }

        private void Update()
        {
            HandleSpawnTimer();
        }

        /// <summary>
        /// Handle the spawn timer
        /// </summary>
        private void HandleSpawnTimer()
        {
            if (timer < timeBetweenSpawns)
                timer += Time.deltaTime;
            if (!isSpawning || timer < timeBetweenSpawns) return;
            timer = 0;
            isSpawning = true;
            Spawn();
        }

        /// <summary>
        /// Set the singleton instance or destroy the game object if one already exists
        /// </summary>
        private void SetSingleton()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        /// <summary>
        /// Start spawning asteroids if there are any available in the pool and the game is in gameplay state
        /// </summary>
        public void StartSpawning()
        {
            if (isSpawning || GameManager.Instance.CurrentGameState != GameManager.GameState.Gameplay) return;
            isSpawning = true;
        }

        /// <summary>
        /// Stop spawning asteroids
        /// </summary>
        public void StopSpawning()
        {
            isSpawning = false;
        }

        /// <summary>
        /// Spawn an asteroid from the pool at the spawn point with a random direction
        /// </summary>
        private void Spawn()
        {
            if (!AsteroidsObjectPoolManager.Instance.ObjectAvailableFromPool(out var availableObj))
            {
                isSpawning = false;
                return;
            }

            spawnPoint.position = new Vector2(UnityEngine.Random.Range(spawnXBounds.x, spawnXBounds.y),
                spawnPoint.position.y);
            availableObj.transform.position = spawnPoint.position;
            var direction = new Vector2(UnityEngine.Random.Range(-.5f, .5f), -1);
            availableObj.GetComponent<Asteroid>().Push(direction.normalized, 100f);
        }
    }
}