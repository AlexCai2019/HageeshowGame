namespace Hageeshow
{
    public interface IGameState
    {
        void GameStart();
        void Gaming();
        void GameEnd(bool isWon);
    }
}