﻿using UnityEngine;

namespace Gameplay.Misc
{
    public class SkyboxRotator : MonoBehaviour
    {
        public float RotationPerSecond = 2;
        protected void Update()
        {
            RenderSettings.skybox.SetFloat("_Rotation", Time.time * RotationPerSecond);
        }
    }
}