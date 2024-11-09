using UnityEngine;

namespace Hageeshow.FlappyBird
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(AudioSource))]
    public class Hagee : MonoBehaviour, IGameState
    {
        [SerializeField]
        private AudioClip rushClip;
        [SerializeField]
        private AudioClip metalClip;

        private Rigidbody2D rb2D;
        private AudioSource soundPlayer;

        private void Awake()
        {
            rb2D = GetComponent<Rigidbody2D>();
            soundPlayer = GetComponent<AudioSource>();
        }

        public void GameStart()
        {
            rb2D.bodyType = RigidbodyType2D.Dynamic;
            transform.position = Vector3.zero;

            soundPlayer.clip = rushClip;
            soundPlayer.Play();
        }

        public void Gaming()
        {
            if (Input.GetKeyUp(KeyCode.Space)) //按空白鍵
                rb2D.velocity = new(rb2D.velocity.x, 3.5F);
            if (transform.position.y < -6) //摔死
                GameManager.instance.GameEnd(false);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Pipe")) //是水管
                GameManager.instance.GameEnd(false);
            else if (collision.gameObject.CompareTag("PipeSpace")) //是中間的空隙
                GameManager.instance.PassPipe();
        }

        public void GameEnd(bool isWon)
        {
            rb2D.bodyType = RigidbodyType2D.Static;
            soundPlayer.clip = metalClip;
            soundPlayer.Play();
        }
    }
}