using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace AsteroidsAssigment
{
    /// <summary>
    /// Handles the player movement
    /// </summary>
    public class PlayerMovement : MonoBehaviour
    {
        /// <summary>
        /// How many fingers are touching the screen
        /// </summary>
        private int touchCount;

        /// <summary>
        /// Previous touch position for calculating delta
        /// </summary>
        private Vector2 previousTouchPos;
        /// <summary>
        /// Pointer event data for calculating delta when using mouse
        /// </summary>
        private PointerEventData pointerEventData;
        /// <summary>
        /// New position to move to
        /// </summary>
        private Vector2 targetMovePosition;

        [Tooltip("Player move speed in units per second")]
        [SerializeField] private float playerMoveSpeed = 10;
        [Tooltip("Player acceleration speed in units per second")]
        [SerializeField] private float accelerationSpeed = 10;

        private void Awake()
        {
            targetMovePosition = transform.position;
        }

        private void Update()
        {
#if !UNITY_EDITOR
            CheckTouchDrag();
#endif
        }

        /// <summary>
        /// For testing in unity editor using mouse
        /// </summary>
        /// <param name="eventData"></param>
        public void OnMouseDrag(BaseEventData eventData)
        {
#if UNITY_EDITOR
            pointerEventData = (PointerEventData)eventData;

            //Need to make resolution undependant as screen resolutions will vary
            var delta = pointerEventData.delta;
            delta /= ResolutionSetter.height;
            if(delta.x > 0.01f || delta.x < -0.01f)
                Move(delta);
#endif
        }

        /// <summary>
        /// Handles touch drag
        /// </summary>
        private void CheckTouchDrag()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touchCount == 0)
                {
                    previousTouchPos = touch.position;
                }

                var delta = touch.position - previousTouchPos;
                Move(delta);

                // previousTouchPos is only for holding
                previousTouchPos = touch.position;
            }

            touchCount = Input.touchCount;
        }

        void Move(Vector2 delta)
        {
            targetMovePosition.x += Mathf.Abs(playerMoveSpeed) * delta.x;
            targetMovePosition.x = Mathf.Clamp(targetMovePosition.x, -GameManager.Instance.ScreenBorderPosition,
                GameManager.Instance.ScreenBorderPosition);
            transform.position =
                Vector2.Lerp(transform.position, targetMovePosition, Time.deltaTime * accelerationSpeed);
        }
    }
}