using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace AsteroidsAssigment
{
    /// <summary>
    /// Menu screen behaviour
    /// </summary>
    public class MenuScreen : MonoBehaviour
    {
        [Tooltip("Starts the game")] [SerializeField]
        private Button playButton;

        [Tooltip("Quits the app")] [SerializeField]
        private Button quitButton;

        /// <summary>
        /// Called when the play button is clicked
        /// </summary>
        private UnityEvent onPlayButtonClicked = new UnityEvent();

        private void Awake()
        {
            // Subscribe to the buttons
            playButton.onClick.AddListener(OnPlayButtonClicked);
            quitButton.onClick.AddListener(Quit);
        }

        /// <summary>
        /// Called when the play button is clicked
        /// </summary>
        private void OnPlayButtonClicked()
        {
            onPlayButtonClicked?.Invoke();
        }

        /// <summary>
        /// Called when the quit button is clicked
        /// </summary>
        private void Quit()
        {
            Application.Quit();
        }

        /// <summary>
        /// Subscribe to the play button click event
        /// </summary>
        /// <param name="action"></param>
        public void SubscribeToOnPlayButtonClicked(UnityAction action)
        {
            onPlayButtonClicked.AddListener(action);
        }

        /// <summary>
        /// Unsubscribe to the play button click event
        /// </summary>
        /// <param name="action"></param>
        public void UnsubscribeToOnPlayButtonClicked(UnityAction action)
        {
            onPlayButtonClicked.RemoveListener(action);
        }
    }
}