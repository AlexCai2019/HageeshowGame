using UnityEngine;

namespace Hageeshow.Breakout
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class Board : MonoBehaviour, IGameState, IDieEvent
    {
        [SerializeField]
        private float speed;

        private float move;

        private BoxCollider2D boxCollider2D;
        private float halfColliderWidth;
        private float quarterColliderHeight;

        private void Awake()
        {
            boxCollider2D = GetComponent<BoxCollider2D>();
            halfColliderWidth = boxCollider2D.bounds.size.x * 0.5F;
            quarterColliderHeight = boxCollider2D.bounds.size.y * 0.25F;
        }

        public void Gaming()
        {
            move = Input.GetAxis("Horizontal");
        }

        public void FixedGaming()
        {
            transform.Translate(move * speed, 0.0F, 0.0F);
        }

        public void Dead()
        {
            move = 0.0F;
            transform.position = new(0.0F, -4.0F, 0.0F);
        }

        public void GameEnd(bool isWon)
        {
            transform.position = new(0.0F, -4.0F, 0.0F);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Vector2 collidePos = collision.GetContact(0).point;
            if (collidePos.y < transform.position.y + quarterColliderHeight)
                return;

            if (!collision.gameObject.TryGetComponent(out Hagee hagee)) //不是西瓜
                return;
            Rigidbody2D hageeRB2D = hagee.GetComponent<Rigidbody2D>();

            float offset = transform.position.x - collidePos.x;
            float currentAngle = Vector2.SignedAngle(Vector2.up, hageeRB2D.velocity);
            float bounceAngle = (offset / halfColliderWidth) * 75;
            float newAngle = Mathf.Clamp(currentAngle + bounceAngle, -75, 75);

            hageeRB2D.velocity = Quaternion.AngleAxis(newAngle, Vector3.forward) * Vector2.up * hageeRB2D.velocity.magnitude;
        }
    }
}