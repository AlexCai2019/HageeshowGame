using UnityEngine;
using UnityEngine.UI;

namespace Hageeshow.DoodleJump
{
    public class TitleText : GenericText, IGameState
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
            myText.text = "按空白鍵重生";
            if (score > highestScore)
                highestScore = score;
            recordText.text = $"本次紀錄: {score} 最高紀錄: {highestScore}";
        }

        public void UpdateScore(int score)
        {
            if (score > this.score)
            {
                this.score = score;
                myText.text = score.ToString();
            }
        }
    }
}