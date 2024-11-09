using System.Collections.Generic;
using UnityEngine;

namespace Hageeshow.Minesweeper
{
    public class CardGenerator : MonoBehaviour, IGameState, IClickEvent
    {
        private const uint X = 15U;
        private const uint Y = 5U;
        private const uint MINES = 15U;

        [SerializeField]
        private GameObject cardPrefab;

        [SerializeField]
        private Sprite[] empty;
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
        private Sprite back;

        private readonly Sprite[][] cardSprites = new Sprite[9][];

        private readonly Card[,] map = new Card[Y, X];

        private bool hasFristClick = false;

        private void Awake()
        {
            cardSprites[0] = empty;
            cardSprites[1] = one;
            cardSprites[2] = two;
            cardSprites[3] = three;
            cardSprites[4] = four;
            cardSprites[5] = five;
            cardSprites[6] = six;
            cardSprites[7] = seven;
            cardSprites[8] = eight;
        }

        private void Start()
        {
            //建立所有的卡牌物件
            float xPos, yPos = 3.6F; //y起始3.5
            for (uint y = 0U; y < Y; y++)
            {
                xPos = -7.7F; //x起始7.
                for (uint x = 0U; x < X; x++)
                {
                    GameObject cardObject = Instantiate(cardPrefab, new(xPos, yPos), Quaternion.identity, transform);
                    map[y, x] = cardObject.GetComponent<Card>();
                    map[y, x].InitializeXY(x, y);
                    xPos += 1.1F;
                }
                yPos -= 1.6F;
            }
        }

        public void GameStart()
        {
            hasFristClick = false;
            //清空地圖
            for (uint y = 0U; y < Y; y++)
                for (uint x = 0U; x < X; x++)
                    map[y, x].ResetCard(back);
        }

        public void Gaming() {}

        public void ClickCard(uint x, uint y)
        {
            if (!hasFristClick) //第一次點擊
            {
                GenerateMap(x, y);
                hasFristClick = true;
            }

            Flip(x, y);
        }

        private void Flip(uint x, uint y)
        {
            Card card = map[y, x];
            if (!card.ForceFlip()) //已經翻過了
                return;

            if (card.val != 0)
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
                        candidates[i++] = point; //放置地雷
                }
            }

            //洗牌
            ShuffleCandidates(candidates);
            for (i = 0U; i < MINES; i++) //取前MINES項作為地雷
                map[candidates[i].y, candidates[i].x].SetVal(Card.MINE, mine);
            for (; i < candidates.Length; i++)//剩下的不是地雷
                SetMinesCount(candidates[i].x, candidates[i].y);
            foreach (Point p in avoidMines) //當初被避開的九宮格也要設定數字
                SetMinesCount(p.x, p.y);
        }

        private void SetMinesCount(uint x, uint y)
        {
            uint minesCount = 0U;
            foreach (Point p in Get9(x, y)) //九宮格內找有幾個地雷
                if (map[p.y, p.x].val == Card.MINE)
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

        private void ShuffleCandidates(Point[] candidates)
        {
            //材質陣列洗牌
            int len = candidates.Length;
            int lenSub1 = len - 1;
            for (int i = 0; i < lenSub1; i++)
            {
                int swap = Random.Range(i, len);
                if (swap != i)
                    (candidates[i], candidates[swap]) = (candidates[swap], candidates[i]);
            }
        }

        public void GameEnd(bool isWon)
        {
            //如果輸了 就展示地雷
            if (isWon)
                return;
            for (uint y = 0U; y < Y; y++)
                for (uint x = 0U; x < X; x++)
                    if (map[y, x].val == Card.MINE)
                        _ = map[y, x].ForceFlip();
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
                return (int)((x << 8) + y);
            }
        }
    }
}