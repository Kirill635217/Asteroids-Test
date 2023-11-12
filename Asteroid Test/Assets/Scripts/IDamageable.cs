using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidsAssigment
{
    public interface IDamageable
    {
        void Hit(int damage = 1);
    }
}
