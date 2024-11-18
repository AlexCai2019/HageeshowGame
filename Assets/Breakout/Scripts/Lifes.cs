using UnityEngine;

namespace Hageeshow.Breakout
{
    public class Lifes : MonoBehaviour, IGameState, IDieEvent
    {
        private int lifes;

        public void GameStart()
        {
            foreach (Transform child in transform)
                child.gameObject.SetActive(true);
            lifes = transform.childCount;
        }

        public void Dead()
        {
            lifes--;
            transform.GetChild(lifes).gameObject.SetActive(false);
            if (lifes == 0)
                GameManager.instance.GameEnd(false);
        }
    }
}