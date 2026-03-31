using System.Collections.Generic;
using UnityEngine;

namespace DRS.Core.AI
{
    [CreateAssetMenu(fileName = "AI_State", menuName = "AI/State", order = 0)]
    public class State : ScriptableObject
    {
        public string StateName;
        [SerializeReference, SubclassSelector]
        public List<Action> Actions;
        public List<Transition> Transitions;

        public State DeepCopy(Dictionary<State, State> clonedStatesByOriginalState)
        {
            State clone;

            if(clonedStatesByOriginalState.ContainsKey(this))
            {
                clone = clonedStatesByOriginalState[this];
            }
            else
            {clone = ScriptableObject.CreateInstance<State>();
                clonedStatesByOriginalState[this] = clone; // need to addd to dictionary immediately so it can be seen when recursively calling from transition
                clone.StateName = StateName;

                clone.Actions = new List<Action>();
                foreach (var action in Actions)
                {
                    clone.Actions.Add(action.DeepCopy());
                }

                clone.Transitions = new List<Transition>();
                foreach (var transition in Transitions)
                {
                    clone.Transitions.Add(transition.DeepCopy(clonedStatesByOriginalState));
                }
            }

            return clone;
        }
    }
}