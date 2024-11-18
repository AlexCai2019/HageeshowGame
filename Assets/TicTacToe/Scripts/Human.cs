namespace Hageeshow.TicTacToe
{
    public class Human : TicTacToePlayer
    {
        public override void GameStart()
        {
            isMyTurn = true;
        }

        public override void Gaming() { }
    }
}