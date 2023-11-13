using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidsAssigment
{
    /// <summary>
    /// The default bullet behaviour
    /// </summary>
    public class DefaultBullet : Bullet
    {
        [Tooltip("The speed of the bullet")] [SerializeField]
        private float speed = 10f;

        protected override void Move()
        {
            transform.Translate(Vector3.up * (Time.deltaTime * speed));
        }

        protected override void OnHit()
        {
            Destroy(gameObject);
        }
    }
}