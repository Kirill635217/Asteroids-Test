using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidsAssigment
{
    public class DefaultBullet : Bullet
    {
        [SerializeField] private float speed = 10f;

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
