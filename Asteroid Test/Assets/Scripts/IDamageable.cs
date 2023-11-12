using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidsAssigment
{
    public interface IDamageable
    {
        void Hit(float damage = 1);
    }
}
