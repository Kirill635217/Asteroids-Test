using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace AsteroidsAssigment
{
    /// <summary>
    /// The gameplay screen behaviour
    /// </summary>
    public class GameplayScreen : MonoBehaviour
    {
        /// <summary>
        /// Player reference
        /// </summary>
        private Player player;

        [Tooltip("The score text")] [SerializeField]
        private TMP_Text scoreText;

        [Tooltip("The hp text of the player")] [SerializeField]
        private TMP_Text hpText;

        [Tooltip("The move area for the player, used only in editor")] [SerializeField]
        private GameObject moveArea;


        private void OnEnable()
        {
            if (player == null)
            {
                player = FindObjectOfType<Player>();
                player.SubscribeToOnHit(UpdateUI);
            }

            UpdateUI();
#if !UNITY_EDITOR
            moveArea.SetActive(false);
#endif
        }

        private void Start()
        {
            GameManager.Instance.SubscribeToOnAsteroidDestroyed(UpdateUI);
        }

        /// <summary>
        /// Update the UI with the current score and hp
        /// </summary>
        private void UpdateUI()
        {
            hpText.text = $"HP: {player.Health}";
            if (GameManager.Instance == null)
            {
                scoreText.text = "Score: 0";
                return;
            }

            scoreText.text = $"Score: {GameManager.Instance.Score}";
        }
    }
}