namespace Hageeshow.Minesweeper
{
    public class MinesText : GenericText, IGameState
    {
        private int minesCount;

        public void GameStart()
        {
            SetCount((int)CardGenerator.MINES_COUNT);
        }

        internal void Add() => SetCount(minesCount + 1);

        internal void Subtract() => SetCount(minesCount - 1);

        private void SetCount(int count)
        {
            minesCount = count;
            myText.text = count.ToString();
        }
    }
}