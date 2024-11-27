using UnityEngine;

namespace Hageeshow.DoodleJump
{
    public class CameraFollow : MonoBehaviour, IGameState, IHitPlatformEvent
    {
        [SerializeField]
        private Transform hageeTransform;

        private readonly Vector3 tickMove = new(0.0F, 0.01F, 0.0F);
        private float highestY;

        public void GameStart()
        {
            transform.position = new(0.0F, GameManager.BOTTOM_Y, -10.0F);
            highestY = GameManager.BOTTOM_Y;
        }

        public void Gaming()
        {
            if (transform.position.y < highestY)
                transform.Translate(tickMove);
        }

        public void HitPlatform()
        {
            float hageeY = hageeTransform.position.y;
            if (hageeY > highestY)
                highestY = hageeY;
        }
    }
}