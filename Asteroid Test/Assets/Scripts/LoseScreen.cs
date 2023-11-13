using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace AsteroidsAssigment
{
    public class LoseScreen : MonoBehaviour
    {
        [SerializeField] private Button restartButton;
        [SerializeField] private Button goToMenuButton;
        [SerializeField] private TMP_Text scoreText;

        private UnityEvent onRestartButtonClicked = new UnityEvent();
        private UnityEvent onGoToMenuButtonClicked = new UnityEvent();

        private void Awake()
        {
            restartButton.onClick.AddListener(OnRestartButtonClicked);
            goToMenuButton.onClick.AddListener(OnGoToMenuButtonClicked);
        }

        private void OnEnable()
        {
            if(GameManager.Instance == null)
                return;
            scoreText.text = $"Score: {GameManager.Instance.Score}";
        }

        private void OnRestartButtonClicked()
        {
            onRestartButtonClicked?.Invoke();
        }

        private void OnGoToMenuButtonClicked()
        {
            onGoToMenuButtonClicked?.Invoke();
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
