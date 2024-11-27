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

        private Image myImage;

        private void Awake()
        {
            myImage = GetComponent<Image>();
        }

        public override void OnClick()
        {
            GameManager.instance.RestartGame();
        }

        public void GameStart()
        {
            myImage.sprite = smile;
        }

        public void GameEnd(bool isWon)
        {
            myImage.sprite = isWon ? sunglasses : dizzy;
        }
    }
}