using UnityEngine;
using UnityEngine.UI;

namespace Hageeshow.Breakout
{
    public class Lifes : MonoBehaviour, IGameState, IDieEvent
    {
        private int lifes;
        private Image[] childImages;
        private readonly Color someTransparent = new(1.0F, 1.0F, 1.0F, 0.3F);

        private void Awake()
        {
            childImages = GetComponentsInChildren<Image>();
        }

        public void GameStart()
        {
            foreach (Image image in childImages)
                image.color = Color.white;
            lifes = transform.childCount;
        }

        public void Dead()
        {
            lifes--;
            childImages[lifes].color = someTransparent;
            if (lifes == 0)
                GameManager.instance.GameEnd(false);
        }
    }
}