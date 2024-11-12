using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hageeshow.TicTacToe
{
    public class Human : TicTacToePlayer
    {
        public override void GameStart()
        {
            isMyTurn = true;
        }

        public override void Gaming() { }

        public override void GameEnd(bool isWon)
        {
            isMyTurn = false;
        }
    }
}