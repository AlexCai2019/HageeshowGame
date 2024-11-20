using UnityEngine;

namespace Hageeshow.FlappyBird
{
    [RequireComponent(typeof(AudioSource))]
    public class PipeGenerator : MonoBehaviour, IGameState, IPassPipeEvent
    {
        [SerializeField]
        private GameObject pipePrefab;

        private AudioSource soundPlayer;

        private const int GENERATE_PREFABS = 5;
        private const float PIPES_DISTANCE = 5.505F;

        private int leadingPipe;
        private readonly Pipe[] pipes = new Pipe[GENERATE_PREFABS];

        private void Awake()
        {
            soundPlayer = GetComponent<AudioSource>();
        }

        private void Start()
        {
            for (int i = 0; i < GENERATE_PREFABS; i++)
                pipes[i] = Instantiate(pipePrefab, transform.position, Quaternion.identity, transform).GetComponent<Pipe>();
        }

        public void GameStart()
        {
            leadingPipe = 0; //領頭的
            Vector3 pos = transform.position;
            foreach (Pipe pipe in pipes)
            {
                pipe.enabled = true;
                pipe.transform.localPosition = pos;
                pos.x += PIPES_DISTANCE;
            }
        }

        public void PassPipe()
        {
            soundPlayer.Play();
        }

        public void PipeReachedEnd(Pipe pipe)
        {
            int leadingPipeSub1 = leadingPipe - 1; //0為首9為尾 1為首0為尾 2為首1為尾
            Vector3 lastPipePos = transform.GetChild(leadingPipeSub1 < 0 ? GENERATE_PREFABS - 1 : leadingPipeSub1).localPosition;
            lastPipePos.x += PIPES_DISTANCE;
            pipe.transform.localPosition = lastPipePos;

            leadingPipe++;
            if (leadingPipe == GENERATE_PREFABS)
                leadingPipe = 0;
        }

        public void GameEnd(bool isWon)
        {
            foreach (Pipe pipe in pipes)
                pipe.enabled = false; //停止所有的水管
        }
    }
}