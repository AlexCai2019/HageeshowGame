using UnityEngine;

namespace Hageeshow.FlappyBird
{
    [RequireComponent(typeof(AudioSource))]
    public class GameManager : GenericGameManager, IPassPipeEvent
    {
        public static GameManager instance;

        [SerializeField]
        private TitleText title;
        [SerializeField]
        private Hagee hagee;
        [SerializeField]
        private PipeGenerator pipeGenerator;

        private readonly IPassPipeEvent[] passObjects = new IPassPipeEvent[2];

        private AudioSource soundPlayer;
        private float endCooldown = 0;

        private void Awake()
        {
            instance = this;
            soundPlayer = GetComponent<AudioSource>();
        }

        private void Start()
        {
            gameObjects.Add(title);
            gameObjects.Add(hagee);
            gameObjects.Add(pipeGenerator);

            passObjects[0] = title;
            passObjects[1] = pipeGenerator;
        }

        private void OnDestroy()
        {
            instance = null;
        }

        public override void GameStart()
        {
            base.GameStart();
            soundPlayer.Play();
        }

        public void PassPipe()
        {
            foreach (IPassPipeEvent obj in passObjects)
                obj.PassPipe();
        }

        protected override bool StartCondition()
        {
            return Input.GetKeyUp(KeyCode.Space); //預設是按空白鍵
        }

        public override void GameEnd(bool isWon)
        {
            base.GameEnd(isWon);
            endCooldown = 0.5F;
        }

        public override void NotGaming()
        {
            if (endCooldown > 0)
                endCooldown -= Time.deltaTime;
            else
                base.NotGaming();
        }
    }
}