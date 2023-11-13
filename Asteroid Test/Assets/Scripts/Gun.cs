using UnityEngine;

namespace AsteroidsAssigment
{
    /// <summary>
    /// Handles the shooting
    /// </summary>
    public class Gun : MonoBehaviour
    {
        /// <summary>
        /// Shooting timer
        /// </summary>
        private float timer;

        [Tooltip("How long to wait between shots")] [SerializeField]
        private float timeBetweenShots = 1;

        [Tooltip("The bullet prefab to shoot")] [SerializeField]
        private GameObject bulletPrefab;

        [Tooltip("The bullet spawn point")] [SerializeField]
        private Transform bulletSpawnPoint;

        private void Update()
        {
            HandleShooting();
        }

        /// <summary>
        /// Handle the shooting timer
        /// </summary>
        private void HandleShooting()
        {
            if (timer < timeBetweenShots)
            {
                timer += Time.deltaTime;
                return;
            }

            if (!CanShoot()) return;
            timer = 0;
            Shoot();
        }

        /// <summary>
        /// Check if the player can shoot
        /// </summary>
        /// <returns>True if player's touching the screen</returns>
        private bool CanShoot()
        {
#if UNITY_EDITOR
            return Input.GetAxis("Fire1") > 0;
#else
            return Input.touchCount > 0;
#endif
        }

        /// <summary>
        /// Shoot a bullet
        /// </summary>
        private void Shoot()
        {
            Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        }
    }
}