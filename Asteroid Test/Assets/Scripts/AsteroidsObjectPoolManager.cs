using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AsteroidsAssigment
{
    /// <summary>
    /// Manages the object pool for the asteroids
    /// </summary>
    public class AsteroidsObjectPoolManager : MonoBehaviour
    {
        [Tooltip("How many objects should be created in the pool initially")] [SerializeField]
        private int poolSize = 10;

        [Tooltip("The prefab to be used for the pool")] [SerializeField]
        private GameObject objectPrefab;

        /// <summary>
        /// Called when an object is returned to the pool
        /// </summary>
        private UnityEvent onObjectReturnedToPool = new UnityEvent();

        /// <summary>
        /// The pool of objects
        /// </summary>
        private List<GameObject> objectPool;

        /// <summary>
        /// Singleton instance
        /// </summary>
        public static AsteroidsObjectPoolManager Instance;

        private void Awake()
        {
            SetSingleton();
            InitializeObjectPool();
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
        /// Initialize the object pool with the specified amount of objects
        /// </summary>
        void InitializeObjectPool()
        {
            objectPool = new List<GameObject>();

            for (int i = 0; i < poolSize; i++)
            {
                var obj = Instantiate(objectPrefab);
                obj.SetActive(false);
                objectPool.Add(obj);
            }
        }

        /// <summary>
        /// Get an already created object from the pool or create a new one if none are available and return it
        /// </summary>
        /// <returns>Object from pool</returns>
        public GameObject GetObjectFromPool()
        {
            foreach (var obj in objectPool)
            {
                if (obj.activeInHierarchy) continue;
                obj.SetActive(true);
                return obj;
            }

            // If no inactive object is found, create a new one
            var newObj = Instantiate(objectPrefab);
            objectPool.Add(newObj);
            return newObj;
        }

        /// <summary>
        /// Check if there is an available object in the pool and return it
        /// </summary>
        /// <param name="availableObj"></param>
        /// <returns></returns>
        public bool ObjectAvailableFromPool(out GameObject availableObj)
        {
            availableObj = null;
            foreach (var obj in objectPool)
            {
                if (obj.activeInHierarchy) continue;
                obj.SetActive(true);
                availableObj = obj;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Return an object to the pool, disabling it
        /// </summary>
        /// <param name="obj"></param>
        public void ReturnObjectToPool(GameObject obj)
        {
            obj.SetActive(false);
            onObjectReturnedToPool.Invoke();
        }

        /// <summary>
        /// Return all objects to the pool, disabling them
        /// </summary>
        public void ReturnAllObjectsToPool()
        {
            foreach (var obj in objectPool)
            {
                obj.SetActive(false);
            }
        }

        /// <summary>
        /// Subscribe to the onObjectReturnedToPool event, called when an object is returned to the pool
        /// </summary>
        /// <param name="action"></param>
        public void SubscribeToOnObjectReturnedToPool(UnityAction action)
        {
            onObjectReturnedToPool.AddListener(action);
        }

        /// <summary>
        /// Unsubscribe from the onObjectReturnedToPool event
        /// </summary>
        /// <param name="action"></param>
        public void UnsubscribeFromOnObjectReturnedToPool(UnityAction action)
        {
            onObjectReturnedToPool.RemoveListener(action);
        }
    }
}