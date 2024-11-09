using UnityEngine;

namespace Hageeshow.FlappyBird
{
    [RequireComponent(typeof(AudioSource))]
    public class PipeGenerator : MonoBehaviour, IGameState, IPassPipeEvent
    {
        [SerializeField]
        private GameObject pipePrefab;
        [SerializeField]
        private Sprite[] allCards;

        private AudioSource soundPlayer;

        private float time;
        private const float GENERATE_PERIOD = 1.0F;

        private void Awake()
        {
            soundPlayer = GetComponent<AudioSource>();
        }

        public void GameStart()
        {
            time = 0.0F;
            for (int i = transform.childCount - 1; i >= 0; i--)
                Destroy(transform.GetChild(i).gameObject);
        }

        public void Gaming()
        {
            //一秒執行一次
            time += Time.deltaTime;
            if (time < GENERATE_PERIOD)
                return;

            GameObject pipe = Instantiate(pipePrefab, transform.position, Quaternion.identity, transform);
            pipe.GetComponent<Pipe>().SetSprite(allCards[Random.Range(0, allCards.Length)], allCards[Random.Range(0, allCards.Length)]); //隨機選擇材質
            time = 0.0F;
        }

        public void PassPipe()
        {
            soundPlayer.Play();
        }

        public void GameEnd(bool isWon)
        {
            foreach (Pipe pipe in GetComponentsInChildren<Pipe>())
                pipe.enabled = false; //停止所有的水管
        }
    }
}