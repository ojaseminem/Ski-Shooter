﻿using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Gameplay.Misc
{
    public class TimeCalculator : MonoBehaviour
    {
        public static TimeCalculator instance;
        public static string userPlayTime;

        public TextMeshProUGUI timeCounter;
        private TimeSpan _timeSpan;

        public static bool timerGoing;

        [HideInInspector]
        public float elapsedTime;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            timeCounter.text = "Time: 00:00.0";
        }

        public void BeginTimer()
        {
            timerGoing = true;
            elapsedTime = 0f;

            StartCoroutine(UpdateTimer());
        }

        public void EndTimer()
        {
            timerGoing = false;
        }

        private IEnumerator UpdateTimer()
        {
            while (timerGoing)
            {
                elapsedTime += Time.deltaTime;
                _timeSpan = TimeSpan.FromSeconds(elapsedTime);
                string timePlayingText = _timeSpan.ToString("mm' : 'ss' . 'ff");
                timeCounter.text = timePlayingText;

                userPlayTime = _timeSpan.ToString("mm' : 'ss' . 'ff");

                yield return null;
            }
        }
    }
}