using UnityEngine;

namespace Hageeshow.Snake
{
    public class Food : MonoBehaviour, IGameState, IEatFoodEvent
    {
        [SerializeField]
        private Transform topBorder;
        [SerializeField]
        private Transform bottomBorder;
        [SerializeField]
        private Transform leftBorder;
        [SerializeField]
        private Transform rightBorder;

        public void GameStart()
        {
            gameObject.SetActive(true);
            RandomPosition();
        }

        private void RandomPosition()
        {
            float x = Random.Range(leftBorder.position.x + 1.0F, rightBorder.position.x - 1.0F);
            float y = Random.Range(bottomBorder.position.x + 0.3F, topBorder.position.x - 0.3F);
            transform.position = new(x, y);
        }

        public void EatFood()
        {
            RandomPosition();
        }
    }
}