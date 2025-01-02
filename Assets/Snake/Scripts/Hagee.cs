using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Hageeshow.Snake
{
    [RequireComponent(typeof(AudioSource))]
    public class Hagee : MonoBehaviour, IGameState, IEatFoodEvent
    {
        [SerializeField]
        private Transform segmentPrefab;
        [SerializeField]
        private Toggle canPassSelf;

        private Vector2 direction;
        private Vector3 spawnPos;

        private float tick;

        private AudioSource audioSource;

        private readonly List<Transform> segments = new();
        private readonly HashSet<Vector3> occupied = new();

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void GameStart()
        {
            for (int i = segments.Count - 1; i > 0; i--)
                Destroy(segments[i].gameObject);

            transform.position = Vector2.zero;
            direction = Vector2.right;
            spawnPos = Vector2.zero;
            segments.Clear();
            segments.Add(transform);
            tick = 0.0F;
        }

        public void Gaming()
        {
            //輸入 WASD或方向鍵
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                direction = Vector2.up;
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                direction = Vector2.left;
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                direction = Vector2.down;
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                direction = Vector2.right;
        }

        public void FixedGaming()
        {
            tick += Time.fixedDeltaTime;
            if (tick < 0.25F)
                return;

            tick = 0.0F;
            spawnPos = segments[^1].position; //segments[segments.Count - 1]
            for (int i = segments.Count - 1; i > 0; i--)
                segments[i].position = segments[i - 1].position; //跟著前一塊蛇

            transform.Translate(direction);
        }

        public void GameEnd(bool isWon)
        {
            if (!isWon)
                audioSource.Play();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            GameObject obj = collision.gameObject;
            if (obj.CompareTag("Border") || (obj.CompareTag("Segment") && !canPassSelf.isOn)) //撞到邊界或自己
                GameManager.instance.GameEnd(false);
            else if (obj.CompareTag("Food")) //吃到食物
                GameManager.instance.EatFood();
        }

        public void EatFood()
        {
            Transform newSegment = Instantiate(segmentPrefab);
            newSegment.position = spawnPos;
            segments.Add(newSegment);

            occupied.Clear();
            foreach (Transform segment in segments)
                occupied.Add(segment.position);
        }

        public HashSet<Vector3> GetOccupied() => occupied;
    }
}