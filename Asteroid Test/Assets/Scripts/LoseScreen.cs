using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace AsteroidsAssigment
{
    /// <summary>
    /// Lose screen behaviour
    /// </summary>
    public class LoseScreen : MonoBehaviour
    {
        [Tooltip("Restarts the game")] [SerializeField]
        private Button restartButton;

        [Tooltip("Goes to the menu")] [SerializeField]
        private Button goToMenuButton;

        [Tooltip("The score text")] [SerializeField]
        private TMP_Text scoreText;

        /// <summary>
        /// Called when the restart button is clicked
        /// </summary>
        private UnityEvent onRestartButtonClicked = new UnityEvent();

        /// <summary>
        /// Called when the go to menu button is clicked
        /// </summary>
        private UnityEvent onGoToMenuButtonClicked = new UnityEvent();

        private void Awake()
        {
            // Subscribe to the buttons
            restartButton.onClick.AddListener(OnRestartButtonClicked);
            goToMenuButton.onClick.AddListener(OnGoToMenuButtonClicked);
        }

        private void OnEnable()
        {
            // Update the score text
            if (GameManager.Instance == null)
                return;
            scoreText.text = $"Score: {GameManager.Instance.Score}";
        }

        /// <summary>
        /// Called when the restart button is clicked
        /// </summary>
        private void OnRestartButtonClicked()
        {
            onRestartButtonClicked?.Invoke();
        }

        /// <summary>
        /// Called when the go to menu button is clicked
        /// </summary>
        private void OnGoToMenuButtonClicked()
        {
            onGoToMenuButtonClicked?.Invoke();
        }

        /// <summary>
        /// Subscribe to the on restart button clicked event, called when the restart button is clicked
        /// </summary>
        /// <param name="action"></param>
        public void SubscribeToOnRestartButtonClicked(UnityAction action)
        {
            onRestartButtonClicked.AddListener(action);
        }

        /// <summary>
        /// Unsubscribe to the on restart button clicked event
        /// </summary>
        /// <param name="action"></param>
        public void UnsubscribeToOnRestartButtonClicked(UnityAction action)
        {
            onRestartButtonClicked.RemoveListener(action);
        }

        /// <summary>
        /// Subscribe to the on go to menu button clicked event, called when the go to menu button is clicked
        /// </summary>
        /// <param name="action"></param>
        public void SubscribeToOnGoToMenuButtonClicked(UnityAction action)
        {
            onGoToMenuButtonClicked.AddListener(action);
        }

        /// <summary>
        /// Unsubscribe to the on go to menu button clicked event
        /// </summary>
        /// <param name="action"></param>
        public void UnsubscribeToOnGoToMenuButtonClicked(UnityAction action)
        {
            onGoToMenuButtonClicked.RemoveListener(action);
        }
    }
}