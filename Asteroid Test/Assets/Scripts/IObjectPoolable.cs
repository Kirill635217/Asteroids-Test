using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidsAssigment
{
    public interface IObjectPoolable
    {
        GameObject GetPrefab();
        void Destroy();
    }
}
