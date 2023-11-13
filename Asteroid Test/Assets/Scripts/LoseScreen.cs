using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace AsteroidsAssigment
{
    public class LoseScreen : MonoBehaviour
    {
        [SerializeField] private Button restartButton;
        [SerializeField] private Button goToMenuButton;

        private UnityEvent onRestartButtonClicked = new UnityEvent();
        private UnityEvent onGoToMenuButtonClicked = new UnityEvent();

        private void Awake()
        {
            restartButton.onClick.AddListener(OnRestartButtonClicked);
            goToMenuButton.onClick.AddListener(OnGoToMenuButtonClicked);
        }

        private void OnRestartButtonClicked()
        {
            onRestartButtonClicked?.Invoke();
            gameObject.SetActive(false);
        }

        private void OnGoToMenuButtonClicked()
        {
            onGoToMenuButtonClicked?.Invoke();
            gameObject.SetActive(false);
        }

        public void SubscribeToOnRestartButtonClicked(UnityAction action)
        {
            onRestartButtonClicked.AddListener(action);
        }

        public void UnsubscribeToOnRestartButtonClicked(UnityAction action)
        {
            onRestartButtonClicked.RemoveListener(action);
        }

        public void SubscribeToOnGoToMenuButtonClicked(UnityAction action)
        {
            onGoToMenuButtonClicked.AddListener(action);
        }

        public void UnsubscribeToOnGoToMenuButtonClicked(UnityAction action)
        {
            onGoToMenuButtonClicked.RemoveListener(action);
        }
    }
}
