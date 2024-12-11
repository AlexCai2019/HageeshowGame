using UnityEngine;

namespace Hageeshow.DoodleJump
{
    [RequireComponent(typeof(AudioSource))]
    public class GenericPlatform : MonoBehaviour
    {
        [SerializeField]
        private float bounceForce;

        private Hagee hagee;
        private AudioSource audioSource;

        protected virtual void Awake()
        {
            hagee = GameObject.Find("Hagee").GetComponent<Hagee>();
            audioSource = GetComponent<AudioSource>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            //只偵測落到地上
            if (collision.GetContact(0).normal.y < 0)
                ValidCollide();
        }

        protected virtual void ValidCollide()
        {
            hagee.OnCollide(bounceForce);
            audioSource.Play();
        }
    }
}
