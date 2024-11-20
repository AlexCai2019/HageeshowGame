using UnityEngine;

namespace Hageeshow.FlappyBird
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(AudioSource))]
    public class Hagee : MonoBehaviour, IGameState
    {
        private Rigidbody2D rb2D;
        private AudioSource soundPlayer;

        private float groundY;

        private bool jump;

        private void Awake()
        {
            rb2D = GetComponent<Rigidbody2D>();
            soundPlayer = GetComponent<AudioSource>();
            groundY = Camera.main.ScreenToWorldPoint(Vector3.zero).y - 1;
        }

        public void GameStart()
        {
            rb2D.bodyType = RigidbodyType2D.Dynamic;
            transform.position = Vector3.zero;
            jump = false;
        }

        public void Gaming()
        {
            if (Input.GetKeyUp(KeyCode.Space)) //按空白鍵
                jump = true;
            if (transform.position.y < groundY) //摔死
                GameManager.instance.GameEnd(false);
        }

        public void FixedGaming()
        {
            if (jump)
            {
                rb2D.velocity = new(rb2D.velocity.x, 3.5F);
                jump = false;
            }
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
            soundPlayer.Play();
        }
    }
}