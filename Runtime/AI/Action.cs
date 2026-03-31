using System;

namespace DRS.Core.AI
{
    [Serializable]
    public abstract class Action
    {
        public abstract void OnEnter(StateGraph owningStateGraph);
        public abstract void OnTick(StateGraph owningStateGraph);
        public abstract void OnFixedTick(StateGraph owningStateGraph);
        public abstract void OnExit(StateGraph owningStateGraph);

        public abstract Action DeepCopy();
    }
}