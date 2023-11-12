using UnityEngine;

namespace AsteroidsAssigment
{
    public class Gun : MonoBehaviour
    {
        private float timer;

        [SerializeField] private float timeBetweenShots = 1;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform bulletSpawnPoint;

        private void Update()
        {
            HandleShooting();
        }

        private void HandleShooting()
        {
            if(timer < timeBetweenShots)
            {
                timer += Time.deltaTime;
                return;
            }

            if (!CanShoot()) return;
            timer = 0;
            Shoot();
        }

        private bool CanShoot()
        {
#if UNITY_EDITOR
            return Input.GetAxis("Fire1") > 0;
#else
            return Input.touchCount > 0;
#endif
        }

        private void Shoot()
        {
            Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        }
    }
}
