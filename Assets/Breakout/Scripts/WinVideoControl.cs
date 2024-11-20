using UnityEngine.Video;

namespace Hageeshow.Breakout
{
    public class WinVideoControl : GenericVideoControl, IGameState
    {
        public void GameStart()
        {
            gameObject.SetActive(false);
        }

        public void GameEnd(bool isWon)
        {
            if (isWon)
            {
                gameObject.SetActive(true);
                PlayVideo();
            }
        }

        protected override void EndOfVideo(VideoPlayer source)
        {
            gameObject.SetActive(false);
        }
    }
}