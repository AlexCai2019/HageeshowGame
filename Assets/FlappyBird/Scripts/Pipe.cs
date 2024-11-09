using UnityEngine;

namespace Hageeshow.FlappyBird
{
    [RequireComponent(typeof(Collider2D))]
    public class Pipe : MonoBehaviour
    {
        private const float SCREEN_TOP = 7.0F;
        private const float SCREEN_BOTTOM = -5.0F;

        private Transform upper;
        private Transform lower;

        private void Awake()
        {
            upper = transform.GetChild(0);
            lower = transform.GetChild(1);
        }

        private void Start()
        {
            float spaceCenter = Random.Range(-2.0F, 2.0F); //空間的中間
            float upperBottom = spaceCenter + 1.5F;
            float lowerTop = spaceCenter - 1.5F;

            float upperCenter = (upperBottom + SCREEN_TOP) * 0.5F;
            float lowerCenter = (lowerTop + SCREEN_BOTTOM) * 0.5F;

            float upperLength = SCREEN_TOP - upperBottom;
            float lowerLength = lowerTop - SCREEN_BOTTOM;

            upper.localPosition = new(0.0F, upperCenter, 0.0F);
            upper.localScale = new(1.0F, upperLength * 0.5F, 1.0F);

            lower.localPosition = new(0.0F, lowerCenter, 0.0F);
            lower.localScale = new(1.0F, lowerLength * 0.5F, 1.0F);
        }

        internal void SetSprite(Sprite upperSprite, Sprite lowerSprite)
        {
            upper.GetComponent<SpriteRenderer>().sprite = upperSprite;
            lower.GetComponent<SpriteRenderer>().sprite = lowerSprite;
        }

        private void FixedUpdate()
        {
            if (transform.position.x < -10)
                Destroy(gameObject);
            else
                transform.Translate(Time.deltaTime * -5.5F, 0.0F, 0.0F);
        }
    }
}