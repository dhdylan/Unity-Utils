using System;
using UnityEngine;
using System.Collections.Generic;

namespace DRS.Core.AI
{
    [Serializable]
    public class Transition
    {
        public State TransitionState;
        [SerializeReference, SubclassSelector]
        public List<Condition> Conditions;

        public Transition DeepCopy(Dictionary<State, State> clonedStatesByOriginalState)
        {
            Transition copy = new Transition();
            copy.Conditions = new List<Condition>();
            copy.TransitionState = TransitionState.DeepCopy(clonedStatesByOriginalState);

            foreach(var condition in Conditions)
            {
                copy.Conditions.Add(condition.DeepCopy());
            }

            return copy;
        }
    }
}