using UnityEngine;

namespace Hageeshow.Breakout
{
    [RequireComponent(typeof(AudioSource))]
    public class BrickGenerator : MonoBehaviour, IGameState, IHitBrickEvent
    {
        [SerializeField]
        private GameObject brickPrefab;
        [SerializeField]
        private Sprite[] allCards;

        private AudioSource soundPlayer;
        private uint remainBricks = 0U;

        private void Awake()
        {
            soundPlayer = GetComponent<AudioSource>();
        }

        private void Start()
        {
            //建立所有的磚塊
            float xPos, yPos = 2.562F; //y起始2.562
            for (uint y = 0U; y < 4U; y++)
            {
                xPos = -8.064F; //x起始-8.064
                for (uint x = 0U; x < 13U; x++)
                {
                    GameObject brickObject = Instantiate(brickPrefab, new(xPos, yPos), brickPrefab.transform.rotation, transform);
                    brickObject.GetComponent<SpriteRenderer>().sprite = allCards[y * 13 + x]; //現在就替換 讓遊戲開始前好看一點
                    xPos += 1.344F; //每直行x增加1.344
                }
                yPos -= 0.854F; //每橫列y減少0.854
            }
        }

        public void GameStart()
        {
            remainBricks = 13U * 4U;
        }

        public void Gaming()
        {
            if (remainBricks == 0)
                GameManager.instance.GameEnd(true); //沒有牌是active了
        }

        public void GameEnd(bool isWon)
        {
            foreach (Transform child in transform)
                child.gameObject.SetActive(true);
        }

        public void HitBrick()
        {
            remainBricks--;
            soundPlayer.Play();
        }
    }
}