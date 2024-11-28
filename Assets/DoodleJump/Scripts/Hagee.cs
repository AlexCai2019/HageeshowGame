using UnityEngine;

namespace Hageeshow.DoodleJump
{
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Hagee : MonoBehaviour, IGameState
    {
        [SerializeField]
        private float speed;

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
            transform.position = startPos;
        }

        public void Gaming()
        {
            move = Input.GetAxis("Horizontal");
        }

        public void FixedGaming()
        {
            rb2D.velocity = new(move * speed, rb2D.velocity.y);
            if (transform.position.x < xMin)
                transform.position = new(transform.position.x + screenWidth, transform.position.y);
            else if (transform.position.x > -xMin)
                transform.position = new(transform.position.x - screenWidth, transform.position.y);
        }
    }
}