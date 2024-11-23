namespace Hageeshow
{
    public interface IGameState
    {
        void GameStart() { }
        void Gaming() { }
        void FixedGaming() { }
        void GameEnd(bool isWon) { }
    }
}