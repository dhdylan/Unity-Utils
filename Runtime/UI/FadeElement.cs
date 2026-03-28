using UnityEngine;
using System.Collections;

namespace Core.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class FadeElement : MonoBehaviour
    {
        [SerializeField]
        private float fadeTime = 0.6f;

        private CanvasGroup canvasGroup;
        private Coroutine currentCoroutine;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void FadeTransparent()
        {
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }

            currentCoroutine = StartCoroutine(FadeTransparentCoroutine());
        }

        public void FadeOpaque()
        {
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }

            gameObject.SetActive(true);
            currentCoroutine = StartCoroutine(FadeOpaqueCoroutine());
        }

        private IEnumerator FadeTransparentCoroutine()
        {
            float originalAlpha = canvasGroup.alpha;
            float timer = 0f;
            while (timer < fadeTime)
            {
                canvasGroup.alpha = Mathf.Lerp(originalAlpha, 0f, timer / fadeTime);
                timer += Time.deltaTime;
                yield return null;
            }
            canvasGroup.alpha = 0f;
            currentCoroutine = null;
            gameObject.SetActive(false);
        }

        private IEnumerator FadeOpaqueCoroutine()
        {
            float originalAlpha = canvasGroup.alpha;
            float timer = 0f;
            while (timer < fadeTime)
            {
                canvasGroup.alpha = Mathf.Lerp(originalAlpha, 1f, timer / fadeTime);
                timer += Time.deltaTime;
                yield return null;
            }
            canvasGroup.alpha = 1f;
            currentCoroutine = null;
        }
    }
}