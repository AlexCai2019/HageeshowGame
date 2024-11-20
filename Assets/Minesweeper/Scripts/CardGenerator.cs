using System.Collections.Generic;
using UnityEngine;

namespace Hageeshow.Minesweeper
{
    [RequireComponent(typeof(AudioSource))]
    public class CardGenerator : MonoBehaviour, IGameState, IClickEvent
    {
        private const uint X = 13U;
        private const uint Y = 6U;
        public const uint MINES_COUNT = 15U;
        private const uint COVERS_COUNT = X * Y - MINES_COUNT;

        [SerializeField]
        private GameObject cardPrefab;

        [SerializeField]
        private Sprite empty;
        [SerializeField]
        private Sprite[] one;
        [SerializeField]
        private Sprite[] two;
        [SerializeField]
        private Sprite[] three;
        [SerializeField]
        private Sprite[] four;
        [SerializeField]
        private Sprite[] five;
        [SerializeField]
        private Sprite[] six;
        [SerializeField]
        private Sprite[] seven;
        [SerializeField]
        private Sprite[] eight;
        [SerializeField]
        private Sprite mine;

        [SerializeField]
        private AudioClip flipClip;
        [SerializeField]
        private AudioClip dieClip;

        private AudioSource audioSource;

        private readonly Sprite[][] cardSprites = new Sprite[9][];
        private readonly Card[,] map = new Card[Y, X];
        private readonly List<Card> allMines = new();

        private uint coversCount;
        private bool hasFristClick = false;

        private void Awake()
        {
            cardSprites[0] = new Sprite[] { empty };
            cardSprites[1] = one;
            cardSprites[2] = two;
            cardSprites[3] = three;
            cardSprites[4] = four;
            cardSprites[5] = five;
            cardSprites[6] = six;
            cardSprites[7] = seven;
            cardSprites[8] = eight;

            audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            //建立所有的卡牌物件
            float xPos, yPos = 2.9F; //y起始2.9
            for (uint y = 0U; y < Y; y++)
            {
                xPos = -5.58F; //x起始-5.7
                for (uint x = 0U; x < X; x++)
                {
                    GameObject cardObject = Instantiate(cardPrefab, new(xPos, yPos), Quaternion.identity, transform);
                    map[y, x] = cardObject.GetComponent<Card>();
                    map[y, x].InitializeXY(x, y);
                    xPos += 0.93F; //每直行x增加0.93
                }
                yPos -= 1.4F; //每橫列y減少1.4
            }
        }

        public void GameStart()
        {
            hasFristClick = false;
            coversCount = COVERS_COUNT;
            audioSource.clip = flipClip;

            //清空地圖
            for (uint y = 0U; y < Y; y++)
                for (uint x = 0U; x < X; x++)
                    map[y, x].ResetCard();
        }

        public void ClickCard(uint x, uint y)
        {
            if (!hasFristClick) //第一次點擊
            {
                GenerateMap(x, y);
                hasFristClick = true;
            }

            Flip(x, y);
            audioSource.Play();
        }

        private void Flip(uint x, uint y)
        {
            Card card = map[y, x];
            if (!card.ForceFlip(true)) //已經翻過了
                return;

            if (card.GetVal() == Card.MINE) //開到地雷
            {
                GameManager.instance.GameEnd(false);
                return;
            }

            coversCount--; //減少蓋牌數
            if (coversCount == 0)
                GameManager.instance.GameEnd(true); //沒有牌可以翻了就勝利

            if (card.GetVal() != 0) //不是0 不用把九宮格都翻出來
                return;
            //0的話 就把九宮格都翻出來
            foreach (Point p in Get9(x, y))
                Flip(p.x, p.y); //遞迴
        }

        private void GenerateMap(uint CX, uint CY)
        {
            HashSet<Point> avoidMines = Get9(CX, CY);

            Point[] candidates = new Point[X * Y - avoidMines.Count]; //地雷候選點 避開第一下點的九宮格
            uint i = 0U;
            for (uint y = 0U; y < Y; y++)
            {
                for (uint x = 0U; x < X; x++)
                {
                    Point point = new(x, y);
                    if (!avoidMines.Contains(point)) //不是應該避免地雷的位置
                        candidates[i++] = point; //增加候選點
                }
            }

            //候選點洗牌
            int len = candidates.Length;
            int lenSub1 = len - 1;
            for (int index = 0; index < lenSub1; index++)
            {
                int swap = Random.Range(index, len);
                if (swap != index)
                    (candidates[index], candidates[swap]) = (candidates[swap], candidates[index]);
            }

            for (i = 0U; i < MINES_COUNT; i++) //取前MINES項作為地雷 並儲存到allMines裡
            {
                Card newMine = map[candidates[i].y, candidates[i].x];
                newMine.SetVal(Card.MINE, mine);
                allMines.Add(newMine);
            }
            for (; i < candidates.Length; i++) //剩下的不是地雷
                SetMinesCount(candidates[i].x, candidates[i].y);
            foreach (Point p in avoidMines) //當初被避開的九宮格也要設定數字
                SetMinesCount(p.x, p.y);
        }

        private void SetMinesCount(uint x, uint y)
        {
            uint minesCount = 0U;
            foreach (Point p in Get9(x, y)) //九宮格內找有幾個地雷
                if (map[p.y, p.x].GetVal() == Card.MINE)
                    minesCount++;
            Sprite[] thisCardSprites = cardSprites[minesCount];
            map[y, x].SetVal(minesCount, thisCardSprites[Random.Range(0, thisCardSprites.Length)]);
        }

        private HashSet<Point> Get9(uint CX, uint CY)
        {
            uint LX = CX > 0U ? CX - 1U : 0U; //左X
            uint UY = CY > 0U ? CY - 1U : 0U; //上Y
            uint RX = CX + 1U < X ? CX + 1U : X - 1U; //右X
            uint BY = CY + 1U < Y ? CY + 1U : Y - 1U; //下Y
            return new HashSet<Point>()
            {
                new(LX, UY),
                new(CX, UY),
                new(RX, UY),
                new(LX, CY),
                new(CX, CY),
                new(RX, CY),
                new(LX, BY),
                new(CX, BY),
                new(RX, BY),
            };
        }

        public void GameEnd(bool isWon)
        {
            if (isWon)
                return;

            //如果輸了 就展示地雷
            foreach (Card mine in allMines)
                _ = mine.ForceFlip(false);
            allMines.Clear();
            audioSource.clip = dieClip;
            audioSource.Play();
        }

        private readonly struct Point
        {
            internal readonly uint x, y;

            internal Point(uint x, uint y)
            {
                this.x = x;
                this.y = y;
            }

            public override bool Equals(object obj)
            {
                return obj is Point that && this.x == that.x && this.y == that.y;
            }

            public override int GetHashCode()
            {
                return (int)((x << 8) | y);
            }
        }
    }
}