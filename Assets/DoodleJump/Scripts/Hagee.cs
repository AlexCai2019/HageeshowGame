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
        private TitleText titleText;

        private Rigidbody2D rb2D;
        private float move;

        private readonly Vector3 startPos = new(0.0F, GameManager.BOTTOM_Y + 2.0F, 0.0F);

        private float xMin;
        private float screenWidth;

        private void Awake()
        {
            rb2D = GetComponent<Rigidbody2D>();
            xMin = Camera.main.ScreenToWorldPoint(Vector3.zero).x;
            screenWidth = -xMin * 2.0F;
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

        public void OnCollide(float bounceForce)
        {
            if (rb2D.velocity.y < bounceForce)
                rb2D.velocity = new(rb2D.velocity.x, bounceForce);
            titleText.UpdateScore((int)(transform.position.y - GameManager.BOTTOM_Y));
        }
    }
}