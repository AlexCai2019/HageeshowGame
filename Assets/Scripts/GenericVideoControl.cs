using UnityEngine;
using UnityEngine.Video;

namespace Hageeshow
{
    [RequireComponent(typeof(VideoPlayer))]
    [RequireComponent(typeof(AudioSource))]
    public abstract class GenericVideoControl : MonoBehaviour
    {
        protected VideoPlayer videoPlayer;
        protected AudioSource audioSource;

        private void Awake()
        {
            videoPlayer = GetComponent<VideoPlayer>();
            audioSource = GetComponent<AudioSource>();
            videoPlayer.loopPointReached += EndOfVideo;
        }

        protected abstract void EndOfVideo(VideoPlayer source);
    }
}