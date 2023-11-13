using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidsAssigment
{
    public class AsteroidSpawner : MonoBehaviour
    {
        private float timer;
        private bool isSpawning;

        [SerializeField] private float timeBetweenSpawns = 1f;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private Vector2 spawnXBounds;

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

        private void HandleSpawnTimer()
        {
            if(timer < timeBetweenSpawns)
                timer += Time.deltaTime;
            if (!isSpawning || timer < timeBetweenSpawns) return;
            timer = 0;
            isSpawning = true;
            Spawn();
        }

        private void SetSingleton()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        public void StartSpawning()
        {
            if(isSpawning || GameManager.Instance.CurrentGameState != GameManager.GameState.Gameplay) return;
            isSpawning = true;
        }

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