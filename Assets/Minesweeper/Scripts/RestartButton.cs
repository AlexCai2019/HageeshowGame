using UnityEngine;
using UnityEngine.UI;

namespace Hageeshow.Minesweeper
{
    public class RestartButton : GenericButton, IGameState
    {
        [SerializeField]
        private Sprite smile;
        [SerializeField]
        private Sprite sunglasses;
        [SerializeField]
        private Sprite dizzy;

        private Image childImage;

        private void Awake()
        {
            childImage = transform.GetChild(0).GetComponent<Image>();
        }

        public override void OnClick()
        {
            GameManager.instance.RestartGame();
        }

        public void GameStart()
        {
            childImage.sprite = smile;
        }

        public void Gaming() {}

        public void GameEnd(bool isWon)
        {
            childImage.sprite = isWon ? sunglasses : dizzy;
        }
    }
}