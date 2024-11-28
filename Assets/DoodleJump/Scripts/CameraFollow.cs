using UnityEngine;

namespace Hageeshow.DoodleJump
{
    public class CameraFollow : MonoBehaviour, IGameState
    {
        [SerializeField]
        private Transform hageeTransform;

        private Vector3 newPos = new();

        public void GameStart()
        {
            transform.position = newPos = new(0.0F, GameManager.BOTTOM_Y, -10.0F);
        }

        public void Gaming()
        {
            if (transform.position.y < hageeTransform.position.y)
            {
                newPos.y = hageeTransform.position.y;
                transform.position = newPos;
            }
        }
    }
}