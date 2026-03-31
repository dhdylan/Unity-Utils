using System;

namespace DRS.Core.AI
{
    [Serializable]
    public abstract class Condition
    {
        public bool Negate = false;

        public abstract bool Evaluate(StateGraph owningStateGraph);
        public abstract void Reset();

        public abstract Condition DeepCopy();
    }
}