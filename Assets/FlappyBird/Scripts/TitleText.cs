using UnityEngine;
using UnityEngine.UI;

namespace Hageeshow.FlappyBird
{
    public class TitleText : GenericText, IGameState, IPassPipeEvent
    {
        [SerializeField]
        private Text recordText;

        private uint pipes;
        private uint pipesRecord = 0;

        public void GameStart()
        {
            pipes = 0;
            myText.text = "0";
            recordText.text = string.Empty;
        }

        public void Gaming() {}

        public void PassPipe()
        {
            pipes++;
            myText.text = pipes.ToString();
        }

        public void GameEnd(bool isWon)
        {
            myText.text = "遊戲結束";
            if (pipes > pipesRecord)
                pipesRecord = pipes;
            recordText.text = $"本次紀錄: {pipes} 最高紀錄: {pipesRecord}";
        }
    }
}