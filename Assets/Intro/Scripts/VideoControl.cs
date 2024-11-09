using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

namespace Hageeshow.Intro
{
    public class VideoControl : MonoBehaviour
    {
        private VideoPlayer videoPlayer;
        private AudioSource audioSource;

        private void Awake()
        {
            videoPlayer = GetComponent<VideoPlayer>();
            audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            videoPlayer.loopPointReached += EndOfVideo;
            videoPlayer.Play();
            audioSource.Play();
        }

        private void EndOfVideo(VideoPlayer source)
        {
            SceneManager.LoadScene("MenuScene");
        }
    }
}