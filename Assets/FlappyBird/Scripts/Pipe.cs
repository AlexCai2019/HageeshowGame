using UnityEngine;

namespace Hageeshow.FlappyBird
{
    public class Pipe : MonoBehaviour
    {
        [SerializeField]
        private Sprite[] allCards;

        private PipeGenerator pipeGenerator;

        private Transform upper;
        private Transform lower;
        private SpriteRenderer upperRenderer;
        private SpriteRenderer lowerRenderer;

        private float screenTop;
        private float screenBottom;
        private float screenLeft;

        private void Awake()
        {
            pipeGenerator = transform.parent.GetComponent<PipeGenerator>();
            upper = transform.GetChild(0);
            lower = transform.GetChild(1);
            upperRenderer = upper.GetComponent<SpriteRenderer>();
            lowerRenderer = lower.GetComponent<SpriteRenderer>();

            Vector3 screenVector = Camera.main.ScreenToWorldPoint(Vector3.zero);
            screenTop = -screenVector.y + 0.1F;
            screenBottom = screenVector.y - 0.1F;
            screenLeft = screenVector.x - 0.6F;
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

            float upperCenter = (upperBottom + screenTop) * 0.5F;
            float lowerCenter = (screenBottom + lowerTop) * 0.5F;

            float upperLength = screenTop - upperBottom;
            float lowerLength = lowerTop - screenBottom;

            upper.localPosition = new(0.0F, upperCenter, 0.0F);
            upper.localScale = new(1.0F, upperLength * 0.5F, 1.0F);

            lower.localPosition = new(0.0F, lowerCenter, 0.0F);
            lower.localScale = new(1.0F, lowerLength * 0.5F, 1.0F);

            upperRenderer.sprite = allCards[Random.Range(0, allCards.Length)];
            lowerRenderer.sprite = allCards[Random.Range(0, allCards.Length)];
        }

        private void Update()
        {
            if (transform.position.x < screenLeft)
            {
                pipeGenerator.PipeReachedEnd(this);
                ResetPipe();
            }
            else
                transform.Translate(Time.deltaTime * -5.5F, 0.0F, 0.0F);
        }
    }
}