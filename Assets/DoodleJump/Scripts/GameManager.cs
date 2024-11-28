using UnityEngine;

namespace Hageeshow.DoodleJump
{
    public class GameManager : GenericGameManager
    {
        public static GameManager instance;

        public const float BOTTOM_Y = -50000.0F;

        [SerializeField]
        private Hagee hagee;
        [SerializeField]
        private PlatformGenerator platformGenerator;
        [SerializeField]
        private CameraFollow cameraFollow;

        private void Awake()
        {
            instance = this;

            gameObjects.Add(hagee);
            gameObjects.Add(cameraFollow);
            gameObjects.Add(platformGenerator);
        }

        private void OnDestroy()
        {
            instance = null;
        }

        protected override bool StartCondition()
        {
            return Input.GetKeyDown(KeyCode.Space);
        }
    }
}