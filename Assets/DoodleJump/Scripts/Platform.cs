using UnityEngine;

namespace Hageeshow.DoodleJump
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Platform : MonoBehaviour
    {
        [SerializeField]
        private float speed;
        [SerializeField]
        private float jumpForce;
        [SerializeField]
        private Sprite[] allCards;

        private bool isMove = false;

        private float leftX;
        private float rightX;
        private float direction;

        private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            Vector3 screenRange = Camera.main.ScreenToWorldPoint(Vector3.zero);
            leftX = screenRange.x;
            rightX = -screenRange.x;
            direction = Random.Range(0, 2) == 0 ? speed : -speed;
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void FixedUpdate()
        {
            if (!isMove)
                return;

            transform.Translate(new(direction, 0.0F, 0.0F), transform.parent);
            if (transform.localPosition.x <= leftX || transform.localPosition.x >= rightX) //碰到牆壁
                direction = -direction; //方向相反
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            //從上面來
            if (collision.GetContact(0).normal.y < 0 && collision.relativeVelocity.y < jumpForce)
            {
                Rigidbody2D rb2D = collision.gameObject.GetComponent<Rigidbody2D>();
                rb2D.velocity = new(rb2D.velocity.x, jumpForce);
            }
        }

        public void ResetPlatform(float movingChance)
        {
            isMove = Random.Range(0.0F, movingChance) > 1.5F;
            spriteRenderer.sprite = allCards[Random.Range(0, allCards.Length)];
        }
    }
}