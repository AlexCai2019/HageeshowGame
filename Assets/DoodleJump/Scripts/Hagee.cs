using UnityEngine;

namespace Hageeshow.DoodleJump
{
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Hagee : MonoBehaviour, IGameState
    {
        [SerializeField]
        private float speed;
        [SerializeField]
        private float jumpForce;
        [SerializeField]
        private TitleText titleText;

        private Rigidbody2D rb2D;
        private float move;
        private float springJumpForce;

        private readonly Vector3 startPos = new(0.0F, GameManager.BOTTOM_Y + 2.0F, 0.0F);

        private float xMin;
        private float screenWidth;

        private void Awake()
        {
            rb2D = GetComponent<Rigidbody2D>();
            xMin = Camera.main.ScreenToWorldPoint(Vector3.zero).x;
            screenWidth = -xMin * 2.0F;
            springJumpForce = jumpForce * 1.5F;
        }

        public void GameStart()
        {
            rb2D.bodyType = RigidbodyType2D.Dynamic;
            transform.position = startPos;
        }

        public void Gaming()
        {
            move = Input.GetAxis("Horizontal");
            if (transform.position.y < Camera.main.ScreenToWorldPoint(Vector3.zero).y - 1.0F)
                GameManager.instance.GameEnd(false);
        }

        public void FixedGaming()
        {
            rb2D.velocity = new(move * speed, rb2D.velocity.y);
            //超出畫面會出現在另一邊
            if (transform.position.x < xMin)
                transform.position = new(transform.position.x + screenWidth, transform.position.y);
            else if (transform.position.x > -xMin)
                transform.position = new(transform.position.x - screenWidth, transform.position.y);
        }

        public void GameEnd(bool isWon)
        {
            rb2D.bodyType = RigidbodyType2D.Static; //避免繼續下落
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            //只偵測落到地上
            if (collision.GetContact(0).normal.y < 0)
                return;
            
            GameObject obj = collision.gameObject;
            float force;
            if (obj.CompareTag("Platform"))
                force = jumpForce;
            else if (obj.CompareTag("Spring"))
            {
                force = springJumpForce;
                obj.GetComponent<Animator>().SetBool("isCompress", true);
            }
            else
                return;

            if (rb2D.velocity.y < force)
                rb2D.velocity = new(rb2D.velocity.x, force);
            titleText.UpdateScore((int)(transform.position.y - GameManager.BOTTOM_Y));
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            GameObject obj = collision.gameObject;
            if (obj.CompareTag("Spring"))
                obj.GetComponent<Animator>().SetBool("isCompress", false);
        }
    }
}