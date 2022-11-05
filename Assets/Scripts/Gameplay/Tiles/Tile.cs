using System;
using Gameplay.Misc;
using UnityEngine;

namespace Gameplay.Tiles
{
    public class Tile : MonoBehaviour
    {
        public DestroyableObstacleHandler[] destroyableObstacleHandler;
        private void OnEnable()
        {
            foreach (DestroyableObstacleHandler obstacleHandler in destroyableObstacleHandler)
            {
                obstacleHandler.EnableBoulder();
            }
        }
    }
}