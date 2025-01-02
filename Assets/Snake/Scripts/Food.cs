using System.Collections.Generic;
using UnityEngine;

namespace Hageeshow.Snake
{
    [RequireComponent(typeof(AudioSource))]
    public class Food : MonoBehaviour, IGameState, IEatFoodEvent
    {
        [SerializeField]
        private int maxX;
        [SerializeField]
        private int maxY;
        [SerializeField]
        private int minX;
        [SerializeField]
        private int minY;

        [SerializeField]
        private Hagee hagee;

        private AudioSource audioSource;

        private readonly Vector3 startPos = new(5.0F, 0.0F);

        private readonly List<Vector3> possiblePosition = new();

        private void Awake()
        {
            for (int x = minX; x <= maxX; x++)
                for (int y = minY; y <= maxY; y++)
                    possiblePosition.Add(new(x, y));
            audioSource = GetComponent<AudioSource>();
        }

        public void GameStart()
        {
            transform.position = startPos;
        }

        private void RandomPosition()
        {
            List<Vector3> list = possiblePosition;
            int len = list.Count;

            //洗牌
            for (int i = 0; i < len; i++)
            {
                int j = Random.Range(i, len);
                if (i != j)
                    (list[i], list[j]) = (list[j], list[i]);
            }

            HashSet<Vector3> occupied = hagee.GetOccupied();
            foreach (Vector3 pos in list)
            {
                if (occupied.Contains(pos)) //已經被蛇佔位了
                    continue;
                transform.position = pos;
                return;
            }

            GameManager.instance.GameEnd(true);
        }

        public void EatFood()
        {
            RandomPosition();
            audioSource.Play();
        }
    }
}