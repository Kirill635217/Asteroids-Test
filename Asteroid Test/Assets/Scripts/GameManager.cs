using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidsAssigment
{
    /// <summary>
    /// Manage the game state, score, game values, etc.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        /// <summary>
        /// Screen width border position in world space
        /// </summary>
        private float screenBorderPosition;

        /// <summary>
        /// Screen width border position in world space
        /// </summary>
        public float ScreenBorderPosition => screenBorderPosition;

        /// <summary>
        /// GameManager singleton instance reference
        /// </summary>
        public static GameManager Instance;

        private void Awake()
        {
            SetSingleton();
            SetScreenBorderPosition();
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
    }
}
