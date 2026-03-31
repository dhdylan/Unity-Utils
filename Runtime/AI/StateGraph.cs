using System.Collections.Generic;

namespace DRS.Core.AI
{
    public class StateGraph
    {

        public State CurrentState { get { return _currentState; } private set { } }

        /// <summary>
        /// Use this property to get the reference to the AIController that owns this state graph. Cast it to the AIController
        /// subclass in order to get the specific properties and methods of that subclass. 
        /// This is primarily used by actions and conditions that need to interact with the AIController or other components on the same GameObject.
        /// </summary>
        public AIController AIController { get { return _owner; } private set { } }

        private State _currentState;
        private Dictionary<State, State> _clonedStateByOriginalState = new();
        private AIController _owner;

        public StateGraph(State startingState, AIController owner)
        {
            _currentState = startingState.DeepCopy(_clonedStateByOriginalState);
            _owner = owner;
        }

        public void Tick()
        {
            if (_currentState.Actions != null) // they can be null - this just means nothing happens.
            {
                foreach (var action in _currentState.Actions)
                    action.OnTick(this);
            }

            if (_currentState.Transitions != null)
            {
                foreach (var transition in _currentState.Transitions)
                {
                    bool success = true;

                    foreach (var condition in transition.Conditions)
                    {
                        if (!condition.Evaluate(this)) // make sure every condition for this transition has been satisfied
                        {
                            success = false;
                            break;
                        }
                    }

                    if (success)
                    {
                        foreach (var condition in transition.Conditions)
                            condition.Reset();
                        EnterState(transition.TransitionState);
                    }
                }
            }
        }

        public void FixedTick()
        {
            foreach (var action in _currentState.Actions)
                action.OnFixedTick(this);
        }

        private void EnterState(State state)
        {
            if (_currentState != null)
                foreach (var action in _currentState.Actions)
                    action.OnExit(this);

            _currentState = state;

            foreach (var action in _currentState.Actions)
                action.OnEnter(this);
        }
    }
}