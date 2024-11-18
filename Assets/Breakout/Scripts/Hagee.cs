using UnityEngine;

namespace Hageeshow.Breakout
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(AudioSource))]
    public class Hagee : MonoBehaviour, IGameState, IDieEvent
    {
        private Rigidbody2D rb2D;
        private AudioSource soundPlayer;

        private bool isDead;

        private void Awake()
        {
            rb2D = GetComponent<Rigidbody2D>();
            soundPlayer = GetComponent<AudioSource>();
        }

        public void GameStart()
        {
            rb2D.bodyType = RigidbodyType2D.Dynamic;
            rb2D.velocity = new(Random.Range(0, 2) == 0 ? -4.0F : 4.0F, 4.0F);
            isDead = false;
        }

        public void Gaming()
        {
            if (isDead)
            {
                if (Input.GetKey(KeyCode.Space)) //´_¬¡
                    GameStart();
                return;
            }

            //ºL¦º
            if (transform.position.y < -6.0F)
                GameManager.instance.Dead();
        }

        public void Dead()
        {
            soundPlayer.Play();
            rb2D.bodyType = RigidbodyType2D.Static;
            transform.position = new(0.0F, -3.0F, 0.0F);
            isDead = true;
        }

        public void GameEnd(bool isWon)
        {
            rb2D.bodyType = RigidbodyType2D.Static;
            transform.position = new(0.0F, -3.0F, 0.0F);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Brick"))
            {
                GameManager.instance.HitBrick();
                collision.gameObject.SetActive(false);
            }
        }
    }
}