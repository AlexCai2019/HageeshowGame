using UnityEngine;

namespace Hageeshow.Breakout
{
    public class GameManager : GenericGameManager, IHitBrickEvent, IDieEvent
    {
        [SerializeField]
        private Board board;
        [SerializeField]
        private BrickGenerator brickGenerator;
        [SerializeField]
        private Hagee hagee;
        [SerializeField]
        private Lifes lifes;
        [SerializeField]
        private TitleText title;
        [SerializeField]
        private WinVideoControl winVideo;
        [SerializeField]
        private TimeText timeText;

        public static GameManager instance;

        private readonly IDieEvent[] dieEventObjects = new IDieEvent[3];

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            gameObjects.Add(board);
            gameObjects.Add(brickGenerator);
            gameObjects.Add(hagee);
            gameObjects.Add(lifes);
            gameObjects.Add(title);
            gameObjects.Add(winVideo);
            gameObjects.Add(timeText);

            dieEventObjects[0] = board;
            dieEventObjects[1] = hagee;
            dieEventObjects[2] = lifes;
        }

        private void OnDestroy()
        {
            instance = null;
        }

        public void Dead()
        {
            foreach (IDieEvent obj in dieEventObjects)
                obj.Dead();
        }

        protected override bool StartCondition()
        {
            return Input.GetKeyUp(KeyCode.Space); //預設是按空白鍵
        }

        public void HitBrick()
        {
            brickGenerator.HitBrick();
        }
    }
}