using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace AsteroidsAssigment
{
    public class GameplayScreen : MonoBehaviour
    {
        private Player player;

        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text hpText;
        [SerializeField] private GameObject moveArea;


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

        private void UpdateUI()
        {
            if (GameManager.Instance == null)
            {
                scoreText.text = "Score: 0";
                return;
            }
            scoreText.text = $"Score: {GameManager.Instance.Score}";
            hpText.text = $"HP: {player.Health}";
        }
    }
}