using UnityEngine;

namespace Hageeshow.DoodleJump
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Platform : GenericPlatform
    {
        [SerializeField]
        private float baseSpeed;
        [SerializeField]
        private Sprite[] allCards;

        private bool isMove = false;

        private float leftX;
        private float rightX;
        private float direction;

        private SpriteRenderer spriteRenderer;
        private GameObject spring;

        protected override void Awake()
        {
            base.Awake();
            Vector3 screenRange = Camera.main.ScreenToWorldPoint(Vector3.zero);
            leftX = screenRange.x;
            rightX = -screenRange.x;
            direction = Random.Range(0, 2) == 0 ? baseSpeed : -baseSpeed;
            spriteRenderer = GetComponent<SpriteRenderer>();
            spring = transform.GetChild(0).gameObject;
        }

        private void FixedUpdate()
        {
            if (!isMove)
                return;

            transform.Translate(new(direction, 0.0F, 0.0F), transform.parent);
            if (transform.localPosition.x <= leftX || transform.localPosition.x >= rightX) //碰到牆壁
                direction = -direction; //方向相反
        }

        public void ResetPlatform(int movingChance)
        {
            isMove = Random.Range(0, 10) < movingChance;
            spring.SetActive(Random.Range(0, 10) == 0);
            spriteRenderer.sprite = allCards[Random.Range(0, allCards.Length)];

            float speed = (baseSpeed + movingChance * 0.005F);
            direction = direction > 0 ? speed : -speed;
        }
    }
}