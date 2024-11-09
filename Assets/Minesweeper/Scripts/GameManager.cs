using UnityEngine;

namespace Hageeshow.Minesweeper
{
    public class GameManager : GenericGameManager, IClickEvent
    {
        public static GameManager instance;

        [SerializeField]
        private RestartButton restartButton;
        [SerializeField]
        private TimeText timeText;
        [SerializeField]
        private CardGenerator cardGenerator;

        private void Awake()
        {
            instance = this;

            gameObjects.Add(restartButton);
            gameObjects.Add(timeText);
            gameObjects.Add(cardGenerator);

            isGaming = true; //一進入就開始
        }

        private void OnDestroy()
        {
            instance = null;
        }

        protected override bool StartCondition()
        {
            return false; //結束後 等待RestartButton呼叫
        }

        public void ClickCard(uint x, uint y)
        {
            if (isGaming)
                cardGenerator.ClickCard(x, y);
        }

        public void RestartGame()
        {
            if (isGaming)
                GameEnd(false);
            GameStart();
        }
    }
}