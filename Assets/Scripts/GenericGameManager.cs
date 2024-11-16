using System.Collections.Generic;
using UnityEngine;

namespace Hageeshow
{
    public abstract class GenericGameManager : MonoBehaviour, IGameState
    {
        protected bool isGaming = false;
        protected readonly List<IGameState> gameObjects = new();

        private void Update()
        {
            if (isGaming)
                Gaming();
            else
                NotGaming();
        }

        private void FixedUpdate()
        {
            if (isGaming)
                FixedGaming();
        }

        protected abstract bool StartCondition();

        public virtual void GameStart()
        {
            isGaming = true;
            foreach (IGameState obj in gameObjects)
                obj.GameStart();
        }

        public virtual void Gaming()
        {
            foreach (IGameState obj in gameObjects)
                obj.Gaming();
        }

        public virtual void FixedGaming()
        {
            foreach (IGameState obj in gameObjects)
                obj.FixedGaming();
        }

        public virtual void GameEnd(bool isWon)
        {
            isGaming = false;
            foreach (IGameState obj in gameObjects)
                obj.GameEnd(isWon);
        }

        public virtual void NotGaming()
        {
            if (StartCondition())
                GameStart();
        }
    }
}