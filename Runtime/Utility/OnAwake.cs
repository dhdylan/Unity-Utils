using UnityEngine;
using UnityEngine.Events;

namespace Core.Utility
{
    class OnAwake : MonoBehaviour
    {
        [SerializeField] private UnityEvent onAwake = default;
        private void Awake()
        {
            onAwake?.Invoke();
        }
    }
}
