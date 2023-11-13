using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace AsteroidsAssigment
{
    /// <summary>
    /// Manage the game state, score, game values, etc.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        public enum GameState
        {
            Menu,
            Gameplay,
            LoseScreen,
        }

        /// <summary>
        /// Screen width border position in world space
        /// </summary>
        private float screenBorderPosition;

        /// <summary>
        /// How many asteroids have been destroyed
        /// </summary>
        private int score;

        /// <summary>
        /// Current game state
        /// </summary>
        private GameState gameState = GameState.Menu;

        /// <summary>
        /// Player reference
        /// </summary>
        private Player player;

        private UnityEvent onAsteroidDestroyed = new UnityEvent();

        [SerializeField] private MenuScreen menuScreen;
        [SerializeField] private GameplayScreen gameplayScreen;
        [SerializeField] private LoseScreen loseScreen;

        /// <summary>
        /// Screen width border position in world space
        /// </summary>
        public float ScreenBorderPosition => screenBorderPosition;

        /// <summary>
        /// How many asteroids have been destroyed
        /// </summary>
        public int Score => score;

        /// <summary>
        /// Current game state
        /// </summary>
        public GameState CurrentGameState => gameState;

        /// <summary>
        /// GameManager singleton instance reference
        /// </summary>
        public static GameManager Instance;

        private void Awake()
        {
            SetSingleton();
            SetScreenBorderPosition();
            UpdateUI();
            player = FindObjectOfType<Player>();
            player.Disable();
        }

        private void UpdateUI()
        {
            switch (gameState)
            {
                case GameState.Menu:
                    menuScreen.gameObject.SetActive(true);
                    loseScreen.gameObject.SetActive(false);
                    gameplayScreen.gameObject.SetActive(false);
                    menuScreen.SubscribeToOnPlayButtonClicked(StartGame);
                    loseScreen.UnsubscribeToOnRestartButtonClicked(StartGame);
                    loseScreen.UnsubscribeToOnGoToMenuButtonClicked(OnGoToMenu);
                    break;
                case GameState.Gameplay:
                    menuScreen.gameObject.SetActive(false);
                    loseScreen.gameObject.SetActive(false);
                    gameplayScreen.gameObject.SetActive(true);
                    menuScreen.UnsubscribeToOnPlayButtonClicked(StartGame);
                    loseScreen.UnsubscribeToOnRestartButtonClicked(StartGame);
                    loseScreen.UnsubscribeToOnGoToMenuButtonClicked(OnGoToMenu);
                    break;
                case GameState.LoseScreen:
                    menuScreen.gameObject.SetActive(false);
                    loseScreen.gameObject.SetActive(true);
                    gameplayScreen.gameObject.SetActive(false);
                    menuScreen.UnsubscribeToOnPlayButtonClicked(StartGame);
                    loseScreen.SubscribeToOnRestartButtonClicked(StartGame);
                    loseScreen.SubscribeToOnGoToMenuButtonClicked(OnGoToMenu);
                    break;
            }
        }

        /// <summary>
        /// Set this object as singleton
        /// </summary>
        private void SetSingleton()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }

        /// <summary>
        /// Set the screen width border position in world space
        /// </summary>
        private void SetScreenBorderPosition()
        {
            float screenWidth = Screen.width;
            Vector3 screenPos = new Vector2(screenWidth, Screen.height / 2);
            if (Camera.main == null)
            {
                Debug.LogError("No main camera found.");
                return;
            }
            screenBorderPosition = Camera.main.ScreenToWorldPoint(screenPos).x;
        }

        private void OnPlayerDestroyed()
        {
            gameState = GameState.LoseScreen;
            player.UnsubscribeToOnDestroyed(OnPlayerDestroyed);
            player.Disable();
            AsteroidSpawner.Instance.StopSpawning();
            AsteroidsObjectPoolManager.Instance.UnsubscribeFromOnObjectReturnedToPool(AsteroidDestroyed);
            UpdateUI();
        }

        private void StartGame()
        {
            gameState = GameState.Gameplay;
            AsteroidSpawner.Instance.StartSpawning();
            AsteroidsObjectPoolManager.Instance.ReturnAllObjectsToPool();
            AsteroidsObjectPoolManager.Instance.SubscribeToOnObjectReturnedToPool(AsteroidDestroyed);
            player.SubscribeToOnDestroyed(OnPlayerDestroyed);
            player.Enable();
            score = 0;
            UpdateUI();
        }

        private void OnGoToMenu()
        {
            gameState = GameState.Menu;
            UpdateUI();
        }

        private void AsteroidDestroyed()
        {
            score++;
            onAsteroidDestroyed?.Invoke();
        }

        public void SubscribeToOnAsteroidDestroyed(UnityAction action)
        {
            onAsteroidDestroyed.AddListener(action);
        }

        public void UnsubscribeToOnAsteroidDestroyed(UnityAction action)
        {
            onAsteroidDestroyed.RemoveListener(action);
        }
    }
}
