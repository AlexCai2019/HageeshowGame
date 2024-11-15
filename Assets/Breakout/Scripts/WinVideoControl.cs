using UnityEngine.Video;

namespace Hageeshow.Breakout
{
    public class WinVideoControl : GenericVideoControl, IGameState
    {
        public void GameStart() { }

        public void Gaming() { }

        public void GameEnd(bool isWon)
        {
            if (!isWon)
                return;

            gameObject.SetActive(true);
            videoPlayer.Play();
            audioSource.Play();
        }

        protected override void EndOfVideo(VideoPlayer source)
        {
            gameObject.SetActive(false);
        }
    }
}