using UnityEngine;

namespace Hageeshow.TicTacToe
{
    public abstract class AIBrain
    {
        public TicTacToeButton Move(TicTacToeButton[] map, uint round)
        {
            return round switch
            {
                1U => Round1(map),
                2U => Round2(map),
                _ => Round3(map),
            };
        }

        protected abstract TicTacToeButton Round1(TicTacToeButton[] map);

        protected abstract TicTacToeButton Round2(TicTacToeButton[] map);

        protected abstract TicTacToeButton Round3(TicTacToeButton[] map);

        protected TicTacToeButton RandomMove()
        {
            TicTacToeButton[] notPlaced = new TicTacToeButton[GameManager.instance.GetSpaces()]; //獲得目前還沒被占的點位
            uint i = 0U;
            foreach (TicTacToeButton button in GameManager.instance.map) //檢索地圖
                if (button.GetState() == State.EMPTY) //確認沒有被占
                    notPlaced[i++] = button;
            return notPlaced[Random.Range(0, notPlaced.Length)]; //從沒有被占的點位中隨機選一個
        }
    }

    internal class EasyBrain : AIBrain
    {
        private readonly uint[,] tryAtLeftCorner =
        {
            { GameManager.UP, GameManager.RIGHT_UP },
            { GameManager.LEFT, GameManager.LEFT_DOWN },
            { GameManager.CENTER, GameManager.RIGHT_DOWN }
        }; //第一步下在左上角後 第二步可以下的位置

        private readonly uint[,] tryAtCenter =
        {
            { GameManager.LEFT_UP, GameManager.RIGHT_DOWN },
            { GameManager.UP, GameManager.DOWN },
            { GameManager.RIGHT_UP, GameManager.LEFT_DOWN },
            { GameManager.LEFT, GameManager.RIGHT }
        }; //第一步下在中間後 第二步可以下的位置

        protected override TicTacToeButton Round1(TicTacToeButton[] map)
        {
            return map[map[GameManager.CENTER].GetState() == State.HAGEE ? GameManager.LEFT_UP : GameManager.CENTER]; //搶占中間 不然就左上角
        }

        protected override TicTacToeButton Round2(TicTacToeButton[] map)
        {
            //找出和第一手鄰近 可連成一線 且都是空的兩格 隨機挑選一格落子
            //因為這是第二回合 人類只放了兩個 代表必定能找到一組空的
            uint[,] possibleWays;
            if (map[GameManager.LEFT_UP].GetState() == State.CHOCOLATE) //第一手下在左上角 只有玩家第一手下中間才有可能
                possibleWays = tryAtLeftCorner;
            else //如果不是下在左上角 那就肯定是下在中間了
                possibleWays = tryAtCenter;

            //避免每次都下在同樣的地方 被人類發現規律導致必勝
            int len = possibleWays.GetLength(0), index = Random.Range(0, len), index2 = index;
            do
            {
                if (map[possibleWays[index, 0]].GetState() == State.EMPTY && map[possibleWays[index, 1]].GetState() == State.EMPTY)
                    return map[possibleWays[index, Random.Range(0, 2)]]; //0或1

                index++;
                if (index == len)
                    index = 0;
            } while (index != index2);

            //沒得下(雖然不太可能)
            return RandomMove();
        }

        protected override TicTacToeButton Round3(TicTacToeButton[] map)
        {
            return RandomMove();
        }
    }

    internal class NormalBrain : EasyBrain
    {
        protected override TicTacToeButton Round2(TicTacToeButton[] map)
        {
            uint first, second, third;
            State f, s, t;

            uint[,] winning = GameManager.instance.winning;

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
            return base.Round2(map);
        }
    }

    internal class HardBrain : NormalBrain
    {
        private readonly uint[] sides = { GameManager.UP, GameManager.LEFT, GameManager.RIGHT, GameManager.DOWN };

        protected override TicTacToeButton Round2(TicTacToeButton[] map)
        {
            if (map[GameManager.CENTER].GetState() == State.HAGEE) //第一手下在左上角 只有玩家第一手下中間才有可能
                return base.Round2(map); //過濾掉這種情境

            //以下的內容 都是第一手下在中間 表示玩家可能下在側邊或角落

            //如果人類的這兩手下成這樣
            /*
             * |O| | |    | | |O|
             * | |X| | or | |X| |
             * | | |O|    |O| | |
             */
            //代表人類在第三手很有可能會透過下角落 創造兩個活路
            //此時AI下角落就輸了 因此一定要下側邊
            if ((map[GameManager.LEFT_UP].GetState() == State.CHOCOLATE && map[GameManager.RIGHT_DOWN].GetState() == State.CHOCOLATE) ||
                (map[GameManager.RIGHT_UP].GetState() == State.CHOCOLATE && map[GameManager.LEFT_DOWN].GetState() == State.CHOCOLATE))
                return map[sides[Random.Range(0, sides.Length)]];

            //如果不是下成那樣的話
            return base.Round2(map);
        }

        protected override TicTacToeButton Round3(TicTacToeButton[] map)
        {
            uint first, second, third;
            State f, s, t;

            uint[,] winning = GameManager.instance.winning;

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
            return base.Round3(map);
        }
    }
}