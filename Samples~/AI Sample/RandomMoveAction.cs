using System;
using UnityEngine;

namespace DRS.Core.AI
{
    [Serializable]
    public class RandomMoveAction : Action
    {
        public float Speed = 4f;

        private Vector3 _direction;

        public override Action DeepCopy()
        {
            RandomMoveAction copy = new RandomMoveAction();
            copy.Speed = Speed;
            return copy;
        }

        public override void OnEnter(StateGraph owningStateGraph)
        {
            _direction = new Vector3(UnityEngine.Random.Range(-1f, 1f), 0f, UnityEngine.Random.Range(-1f, 1f)).normalized;
        }

        public override void OnExit(StateGraph owningStateGraph)
        {
        }

        public override void OnFixedTick(StateGraph owningStateGraph)
        {
        }

        public override void OnTick(StateGraph owningStateGraph)
        {
            MyAIController myaic = (MyAIController)owningStateGraph.AIController;
            myaic.characterController.Move(_direction * Time.deltaTime * Speed);
        }
    }
}