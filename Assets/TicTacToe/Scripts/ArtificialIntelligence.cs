using UnityEngine;
using UnityEngine.UI;

namespace Hageeshow.TicTacToe
{
    public class ArtificialIntelligence : TicTacToePlayer
    {
        [SerializeField]
        private Dropdown difficulty;

        private uint round;
        private float tick;

        private readonly EasyBrain easyBrain = new();
        private readonly NormalBrain normalBrain = new();
        private readonly HardBrain hardBrain = new();

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
            if (tick < 2.5F) //·Q2.5¬í
                return;

            AIBrain brain = difficulty.value switch
            {
                0 => easyBrain,
                1 => normalBrain,
                _ => hardBrain
            };

            OnClick(brain.Move(GameManager.instance.map, round));

            round++;
            tick = 0.0F;
            isMyTurn = false;
        }
    }
}