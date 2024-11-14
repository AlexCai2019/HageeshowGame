using UnityEngine;

namespace Hageeshow.TicTacToe
{
    public class ArtificialIntelligence : TicTacToePlayer
    {
        private const uint CENTER = 4U; //陣列的中間
        private const uint LEFT_CORNER = 0U; //陣列的第一項

        private readonly int[,] tryAtLeftCorner = { { 1, 2 }, { 3, 6 }, { 4, 8 } }; //第一步下在左上角後 第二步可以下的位置
        private readonly int[,] tryAtCenter = { { 0, 8 }, { 1, 7 }, { 2, 6 }, { 3, 5 } }; //第一步下在中間後 第二步可以下的位置

        private uint round;

        private float tick;

        public override void GameStart()
        {
            round = 1U;
            isMyTurn = false;
        }

        public override void Gaming()
        {
            if (!isMyTurn)
                return;

            tick += Time.deltaTime;
            if (tick < 3.0F) //想三秒
                return;

            ThinkAMove();
            tick = 0.0F;
            isMyTurn = false;
        }

        private void ThinkAMove()
        {
            TicTacToeButton[] map = GameManager.instance.map;
            OnClick(round switch
            {
                1U => map[map[CENTER].GetState() == State.HAGEE ? LEFT_CORNER : CENTER], //搶占中間 不然就左上角
                2U => Round2(map),
                _ => Round3(map),
            });
            round++;
        }

        private TicTacToeButton Round2(TicTacToeButton[] map)
        {
            int first, second, third;
            State f, s, t;

            int[,] winning = GameManager.instance.winning;

            for (int i = 0, len = winning.GetLength(0); i < len; i++) //檢查人類是否即將連線 如果人類確實即將連線則阻止
            {
                f = map[first = winning[i, 0]].GetState(); //可能連線的第一格 同時將索引存進first中
                s = map[second = winning[i, 1]].GetState(); //可能連線的第二格 同時將索引存進second中
                t = map[third = winning[i, 2]].GetState(); //可能連線的第三格 同時將索引存進third中
                if (f == State.HAGEE && s == State.HAGEE && t == State.EMPTY) //[0]和[1]皆為Hagee
                    return map[third];
                if (f == State.HAGEE && t == State.HAGEE && s == State.EMPTY) //[0]和[2]皆為Hagee
                    return map[second];
                if (s == State.HAGEE && t == State.HAGEE && f == State.EMPTY) //[1]和[2]皆為Hagee
                    return map[first];
            }

            //確認人類沒有要連線後

            //找出和第一手鄰近 可連成一線 且都是空的兩格 隨機挑選一格落子
            //因為這是第二回合 人類只放了兩個 代表必定能找到一組空的
            int[,] possibleWays;
            if (map[LEFT_CORNER].GetState() == State.CHOCOLATE) //第一手下在左上角 只有玩家第一手下中間才有可能
                possibleWays = tryAtLeftCorner;
            else //如果不是下在左上角 那就肯定是下在中間了
                possibleWays = tryAtCenter;

            for (int i = 0, len = possibleWays.GetLength(0); i < len; i++)
                if (map[possibleWays[i, 0]].GetState() == State.EMPTY && map[possibleWays[i, 1]].GetState() == State.EMPTY)
                    return map[possibleWays[i, 1]]; //對第一手下左上角而言 搶角落比較有勝算

            //沒得下
            return RandomMove();
        }

        private TicTacToeButton Round3(TicTacToeButton[] map)
        {
            int first, second, third;
            State f, s, t;

            int[,] winning = GameManager.instance.winning;

            for (int i = 0, len = winning.GetLength(0); i < len; i++) //檢查自己是否即將連線 如果是則執行
            {
                f = map[first = winning[i, 0]].GetState(); //可能連線的第一格 同時將索引存進first中
                s = map[second = winning[i, 1]].GetState(); //可能連線的第二格 同時將索引存進second中
                t = map[third = winning[i, 2]].GetState(); //可能連線的第三格 同時將索引存進third中
                if (f == State.CHOCOLATE && s == State.CHOCOLATE && t == State.EMPTY) //[0]和[1]皆為Chocolate
                    return map[third];
                if (f == State.CHOCOLATE && t == State.CHOCOLATE && s == State.EMPTY) //[0]和[2]皆為Chocolate
                    return map[second];
                if (s == State.CHOCOLATE && t == State.CHOCOLATE && f == State.EMPTY) //[1]和[2]皆為Chocolate
                    return map[first];
            }

            for (int i = 0, len = winning.GetLength(0); i < len; i++) //檢查人類是否即將連線 如果人類確實即將連線則阻止
            {
                f = map[first = winning[i, 0]].GetState(); //可能連線的第一格 同時將索引存進first中
                s = map[second = winning[i, 1]].GetState(); //可能連線的第二格 同時將索引存進second中
                t = map[third = winning[i, 2]].GetState(); //可能連線的第三格 同時將索引存進third中
                if (f == State.HAGEE && s == State.HAGEE && t == State.EMPTY) //[0]和[1]皆為Hagee
                    return map[third];
                if (f == State.HAGEE && t == State.HAGEE && s == State.EMPTY) //[0]和[2]皆為Hagee
                    return map[second];
                if (s == State.HAGEE && t == State.HAGEE && f == State.EMPTY) //[1]和[2]皆為Hagee
                    return map[first];
            }

            //以上都不通過
            return RandomMove(); //就隨機走
        }

        private TicTacToeButton RandomMove()
        {
            TicTacToeButton[] notPlaced = new TicTacToeButton[GameManager.instance.GetSpaces()];
            uint i = 0U;
            foreach (TicTacToeButton button in GameManager.instance.map)
                if (button.GetState() == State.EMPTY)
                    notPlaced[i++] = button;
            return notPlaced[Random.Range(0, notPlaced.Length)];
        }
    }
}