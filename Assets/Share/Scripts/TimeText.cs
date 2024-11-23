using UnityEngine;
using UnityEngine.UI;

namespace Hageeshow
{
    [RequireComponent(typeof(Text))]
    public class TimeText : GenericText, IGameState
    {
        [SerializeField]
        private Text recordText;

        private uint recordTime = uint.MaxValue;
        private uint time;
        private float tick;

        public void GameStart()
        {
            tick = 0.0F;
            time = 0U;
            myText.text = "0";
            recordText.text = string.Empty;
        }

        public void Gaming()
        {
            tick += Time.deltaTime;
            if (tick < 1.0F)
                return;

            time++;
            tick--;
            myText.text = time.ToString();
        }

        public void GameEnd(bool isWon)
        {
            if (isWon)
            {
                if (time < recordTime)
                    recordTime = time;
                recordText.text = $"本次紀錄\n{time}\n最快紀錄\n{recordTime}";
            }
        }
    }
}