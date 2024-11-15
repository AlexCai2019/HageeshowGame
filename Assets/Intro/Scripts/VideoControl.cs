using UnityEngine.SceneManagement;
using UnityEngine.Video;

namespace Hageeshow.Intro
{
    public class VideoControl : GenericVideoControl
    {
        private void Start()
        {
            videoPlayer.Play();
            audioSource.Play();
        }

        protected override void EndOfVideo(VideoPlayer source)
        {
            SceneManager.LoadScene("MenuScene");
        }
    }
}