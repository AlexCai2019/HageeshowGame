using UnityEngine;

namespace Hageeshow.Snake
{
    public class Hagee : MonoBehaviour, IGameState, IEatFoodEvent
    {
        [SerializeField]
        private float speed;

        private Vector2 direction;
        private Vector2 up;
        private Vector2 down;
        private Vector2 left;
        private Vector2 right;

        private void Awake()
        {
            up = new(0, speed);
            down = new(0, -speed);
            left = new(-speed, 0);
            right = new(speed, 0);
        }

        public void GameStart()
        {
            transform.position = Vector2.zero;
            direction = Random.Range(0, 2) == 0 ? left : right;
        }

        public void Gaming()
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                direction = up;
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                direction = left;
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                direction = down;
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                direction = right;
        }

        public void FixedGaming()
        {
            transform.Translate(direction);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            GameObject obj = collision.gameObject;
            if (obj.CompareTag("Border"))
                GameManager.instance.GameEnd(false);
            else if (obj.CompareTag("Food"))
                GameManager.instance.EatFood();
        }

        public void EatFood()
        { }
    }
}