namespace Hageeshow.Breakout
{
    public class TitleText : GenericText, IGameState
    {
        public void GameStart()
        {
            myText.text = string.Empty;
        }

        public void Gaming() { }

        public void GameEnd(bool isWon)
        {
            myText.text = isWon ? "你贏了！" : "你死了！";
        }
    }
}