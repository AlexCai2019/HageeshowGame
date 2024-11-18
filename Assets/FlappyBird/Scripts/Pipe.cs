using UnityEngine;

namespace Hageeshow.FlappyBird
{
    public class Pipe : MonoBehaviour
    {
        [SerializeField]
        private Sprite[] allCards;

        private const float SCREEN_TOP = 7.0F;
        private const float SCREEN_BOTTOM = -5.0F;

        private PipeGenerator pipeGenerator;

        private Transform upper;
        private Transform lower;
        private SpriteRenderer upperRenderer;
        private SpriteRenderer lowerRenderer;

        private void Awake()
        {
            pipeGenerator = transform.parent.GetComponent<PipeGenerator>();
            upper = transform.GetChild(0);
            lower = transform.GetChild(1);
            upperRenderer = upper.GetComponent<SpriteRenderer>();
            lowerRenderer = lower.GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            ResetPipe();
        }

        private void ResetPipe()
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

            upperRenderer.sprite = allCards[Random.Range(0, allCards.Length)];
            lowerRenderer.sprite = allCards[Random.Range(0, allCards.Length)];
        }

        private void Update()
        {
            if (transform.position.x < -10)
            {
                pipeGenerator.PipeReachedEnd(this);
                ResetPipe();
            }
            else
                transform.Translate(Time.deltaTime * -5.5F, 0.0F, 0.0F);
        }
    }
}