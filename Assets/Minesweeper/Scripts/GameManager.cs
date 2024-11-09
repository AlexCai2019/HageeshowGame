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
        private MinesText minesText;
        [SerializeField]
        private CardGenerator cardGenerator;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            gameObjects.Add(restartButton);
            gameObjects.Add(timeText);
            gameObjects.Add(minesText);
            gameObjects.Add(cardGenerator);

            GameStart(); //一進入就開始
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

        public void UpdateFlag(bool flag)
        {
            if (isGaming)
            {
                if (flag)
                    minesText.Subtract();
                else
                    minesText.Add();
            }
        }

        public void RestartGame()
        {
            if (isGaming)
                GameEnd(false);
            GameStart();
        }
    }
}