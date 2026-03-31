using UnityEngine;

namespace DRS.Core.AI
{
    /// <summary>
    /// This class is intended to be subclassed for each specific AI type in the game.
    /// It is not uncommon to only need one AIController subclass for an entire project.
    /// It contains a reference to the root state of the state graph, and it creates a state graph on start.
    /// The state graph that is created is defined by every possible transition the root state can make,
    /// every transition those states can make, and so on.
    /// The state graph will then tick every frame and fixed update, and handles transitions internally.
    /// </summary>
    public abstract class AIController : MonoBehaviour
    {
        public State CurrentState { get; private set; }

        [SerializeField]
        private State _rootState;

        private StateGraph _stateGraph;

        protected virtual void Start()
        {
            _stateGraph = new StateGraph(_rootState, this);
        }

        protected virtual void Update()
        {
            _stateGraph.Tick();
        }

        protected virtual void FixedUpdate()
        {
            _stateGraph.FixedTick();
        }
    }
}