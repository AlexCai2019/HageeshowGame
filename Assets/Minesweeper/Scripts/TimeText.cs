using UnityEngine;

namespace Hageeshow.Minesweeper
{
    public class TimeText : GenericText, IGameState
    {
        private uint time;
        private float tick;

        public void GameStart()
        {
            time = 0U;
        }

        public void Gaming()
        {
            tick += Time.deltaTime;
            if (tick < 1.0F)
                return;

            time++;
            tick -= 1.0F;
            myText.text = time.ToString();
        }

        public void GameEnd(bool isWon)
        {
        }
    }
}