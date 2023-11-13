using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AsteroidsAssigment
{
    public class AsteroidsObjectPoolManager : MonoBehaviour
    {
        [SerializeField] private int poolSize = 10;
        [SerializeField] private GameObject objectPrefab;

        private UnityEvent onObjectReturnedToPool = new UnityEvent();
        private List<GameObject> objectPool;

        public static AsteroidsObjectPoolManager Instance;

        private void Awake()
        {
            SetSingleton();
            InitializeObjectPool();
        }

        private void SetSingleton()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

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

        public void ReturnObjectToPool(GameObject obj)
        {
            obj.SetActive(false);
            onObjectReturnedToPool.Invoke();
        }

        public void SubscribeToOnObjectReturnedToPool(UnityAction action)
        {
            onObjectReturnedToPool.AddListener(action);
        }

        public void UnsubscribeFromOnObjectReturnedToPool(UnityAction action)
        {
            onObjectReturnedToPool.RemoveListener(action);
        }
    }
}