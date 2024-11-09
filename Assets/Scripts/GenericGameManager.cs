using System.Collections.Generic;
using UnityEngine;

namespace Hageeshow
{
    public class GenericGameManager : MonoBehaviour, IGameState
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

        protected virtual bool StartCondition()
        {
            return Input.GetKeyUp(KeyCode.Space); //預設是按空白鍵
        }

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