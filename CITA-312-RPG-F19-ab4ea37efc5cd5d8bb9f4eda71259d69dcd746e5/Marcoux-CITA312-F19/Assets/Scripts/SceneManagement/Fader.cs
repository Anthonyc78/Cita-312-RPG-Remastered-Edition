﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        CanvasGroup canvasGroup;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public IEnumerator FadeOut(float time)
        {
            while (canvasGroup.alpha < 1) // alpha is not 1
            {
                canvasGroup.alpha += Time.deltaTime / time; // moving the alpha towards 1
                yield return null;
            }
        }

        public IEnumerator FadeIn(float time)
        {
            while (canvasGroup.alpha > 0) // alpha is not 0
            {
                canvasGroup.alpha -= Time.deltaTime / time; // moving the alpha towards 0
                yield return null;
            }
        }

        public void FadeOutImmediate()
        {
            canvasGroup.alpha = 1;
        }
    }
}