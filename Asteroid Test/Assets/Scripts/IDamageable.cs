using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidsAssigment
{
    /// <summary>
    /// Interface for damageable objects
    /// </summary>
    public interface IDamageable
    {
        /// <summary>
        /// Handles the hit
        /// </summary>
        /// <param name="damage">Damage from the hit</param>
        void Hit(int damage = 1);
    }
}