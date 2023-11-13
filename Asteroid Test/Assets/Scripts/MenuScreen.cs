using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace AsteroidsAssigment
{
    public class MenuScreen : MonoBehaviour
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Button quitButton;

        private UnityEvent onPlayButtonClicked = new UnityEvent();

        private void Awake()
        {
            playButton.onClick.AddListener(OnPlayButtonClicked);
            quitButton.onClick.AddListener(Quit);
        }

        private void OnPlayButtonClicked()
        {
            onPlayButtonClicked?.Invoke();
            gameObject.SetActive(false);
        }

        private void Quit()
        {
            Application.Quit();
        }

        public void SubscribeToOnPlayButtonClicked(UnityAction action)
        {
            onPlayButtonClicked.AddListener(action);
        }

        public void UnsubscribeToOnPlayButtonClicked(UnityAction action)
        {
            onPlayButtonClicked.RemoveListener(action);
        }
    }
}
