using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace DRS.Core.Utility
{
    class OnStart : MonoBehaviour
    {
        [SerializeField] private UnityEvent onStart = default;
        [SerializeField] private float delay = 0f;

        private void Awake()
        {
            StartCoroutine(DelayCoroutine());
        }

        private IEnumerator DelayCoroutine()
        {
            yield return new WaitForSeconds(delay);

            onStart?.Invoke();
        }
    }
}
