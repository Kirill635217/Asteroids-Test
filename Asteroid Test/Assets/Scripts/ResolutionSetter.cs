using UnityEngine;

namespace AsteroidsAssigment
{
    public class ResolutionSetter : MonoBehaviour
    {
        [SerializeField] [Range(0.25f, 1)] float _resolutionPercentage = 0.75f;

        public static int width;
        public static int height;
        public static float ratio;
        private static bool Resized = false;

        // Start is called before the first frame update
        void Awake()
        {
            if (!Resized)
            {
                Resized = true;

                var w = Screen.width * _resolutionPercentage;
                var h = Screen.height * _resolutionPercentage;

                width = Mathf.RoundToInt(w);
                height = Mathf.RoundToInt(h);


                Debug.Log("Resizing from: " + Screen.width + " x " + Screen.height);
                Debug.Log("Resizing to: " + width + " x " + height);

                Screen.SetResolution(width, height, true);

#if UNITY_EDITOR
                //Need to
                width = Screen.width;
                height = Screen.height;
#endif

                ratio = w / h;
            }

            //    UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(1);
        }
    }
}