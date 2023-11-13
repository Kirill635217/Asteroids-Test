using UnityEngine;

namespace AsteroidsAssigment
{
    /// <summary>
    /// Manages the resolution of the game
    /// </summary>
    public class ResolutionSetter : MonoBehaviour
    {
        [SerializeField] [Range(0.25f, 1)] float resolutionPercentage = 0.75f;

        /// <summary>
        /// The width of the game
        /// </summary>
        public static int width;

        /// <summary>
        /// The height of the game
        /// </summary>
        public static int height;

        /// <summary>
        /// Check if the game has been resized
        /// </summary>
        private static bool Resized;

        // Start is called before the first frame update
        void Awake()
        {
            if (!Resized)
            {
                Resized = true;

                var w = Screen.width * resolutionPercentage;
                var h = Screen.height * resolutionPercentage;

                width = Mathf.RoundToInt(w);
                height = Mathf.RoundToInt(h);


                Debug.Log("Resizing from: " + Screen.width + " x " + Screen.height);
                Debug.Log("Resizing to: " + width + " x " + height);

                Screen.SetResolution(width, height, true);

#if UNITY_EDITOR
                width = Screen.width;
                height = Screen.height;
#endif
            }
        }
    }
}