using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

namespace Hageeshow.Intro
{
    public class VideoControl : MonoBehaviour
    {
        private VideoPlayer videoPlayer;

        private void Awake()
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }

        private void Start()
        {
            videoPlayer.loopPointReached += EndOfVideo;
        }

        private void EndOfVideo(VideoPlayer source)
        {
            SceneManager.LoadScene("MenuScene");
        }
    }
}