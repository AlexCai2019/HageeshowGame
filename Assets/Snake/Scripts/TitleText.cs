using UnityEngine;
using UnityEngine.UI;

namespace Hageeshow.Snake
{
    public class TitleText : GenericText, IGameState, IEatFoodEvent
    {
        [SerializeField]
        private Text recordText;

        private int score;
        private int highestScore = 0;

        public void GameStart()
        {
            score = 0;
            myText.text = "0";
            recordText.text = string.Empty;
        }

        public void GameEnd(bool isWon)
        {
            myText.text = isWon ? "你贏了！" : "按空白鍵重生";
            if (score > highestScore)
                highestScore = score;
            recordText.text = $"本次紀錄: {score} 最高紀錄: {highestScore}";
        }

        public void EatFood()
        {
            score++;
            myText.text = score.ToString();
        }
    }
}