using UnityEngine;

namespace Hageeshow.Breakout
{
    public class GameManager : GenericGameManager, IHitBrickEvent
    {
        [SerializeField]
        private Board board;
        [SerializeField]
        private BrickGenerator brickGenerator;
        [SerializeField]
        private Hagee hagee;
        [SerializeField]
        private TitleText title;
        [SerializeField]
        private WinVideoControl winVideo;
        [SerializeField]
        private TimeText timeText;

        public static GameManager instance;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            gameObjects.Add(board);
            gameObjects.Add(brickGenerator);
            gameObjects.Add(hagee);
            gameObjects.Add(title);
            gameObjects.Add(winVideo);
            gameObjects.Add(timeText);
        }

        private void OnDestroy()
        {
            instance = null;
        }

        public void HitBrick()
        {
            brickGenerator.HitBrick();
        }
    }
}