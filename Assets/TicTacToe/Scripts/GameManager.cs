using UnityEngine;

namespace Hageeshow.TicTacToe
{
    public class GameManager : GenericGameManager
    {
        public static GameManager instance;

        [SerializeField]
        private Human human;
        [SerializeField]
        private ArtificialIntelligence ai;
        public TicTacToeButton[] map;

        private uint spaces;

        private bool isTie;

        public readonly int[,] winning =
        {
            {0, 1, 2}, {3, 4, 5}, {6, 7, 8}, //橫列
	    	{0, 3, 6}, {1, 4, 7}, {2, 5, 8}, //直行
	    	{0, 4, 8}, {2, 4, 6} //斜線
	    };

        private void Awake()
        {
            instance = this;

            gameObjects.Add(human);
            gameObjects.Add(ai);
        }

        private void OnDestroy()
        {
            instance = null;
        }

        private void Start()
        {
            GameStart();
        }

        public override void GameStart()
        {
            base.GameStart();

            foreach (TicTacToeButton button in map)
                button.ResetMe();

            spaces = 3U * 3U;
            isTie = false;
        }

        public uint GetSpaces() => spaces;

        public void OnValidClick()
        {
            for (int i = 0, len = winning.GetLength(0); i < len; i++)
            {
                State x = map[winning[i, 0]].GetState();
                State y = map[winning[i, 1]].GetState();
                State z = map[winning[i, 2]].GetState();

                if (x == State.HAGEE && y == State.HAGEE && z == State.HAGEE)
                {
                    GameEnd(true);
                    return;
                }

                if (x == State.CHOCOLATE && y == State.CHOCOLATE && z == State.CHOCOLATE)
                {
                    GameEnd(false);
                    return;
                }
            }

            spaces--;
            if (spaces == 0)
            {
                isTie = true;
                GameEnd(false);
                return;
            }
        }

        public override void GameEnd(bool isWon)
        {
            isGaming = false;
            if (isTie)
            {
                human.Tie();
                ai.Tie();
            }
            else
            {
                human.GameEnd(isWon);
                ai.GameEnd(!isWon);
            }

            Invoke(nameof(GameStart), 2.5F);
        }

        protected override bool StartCondition() => false; //遊戲不依靠玩家輸入進行
    }

    public enum State
    {
        EMPTY,
        HAGEE,
        CHOCOLATE
    }
}