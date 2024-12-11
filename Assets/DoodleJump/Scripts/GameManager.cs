using UnityEngine;

namespace Hageeshow.DoodleJump
{
    public class GameManager : GenericGameManager
    {
        public static GameManager instance;

        public const float BOTTOM_Y = -30000.0F;

        [SerializeField]
        private Hagee hagee;
        [SerializeField]
        private PlatformGenerator platformGenerator;
        [SerializeField]
        private CameraFollow cameraFollow;
        [SerializeField]
        private TitleText titleText;

        private void Awake()
        {
            instance = this;

            gameObjects.Add(hagee);
            gameObjects.Add(cameraFollow);
            gameObjects.Add(platformGenerator);
            gameObjects.Add(titleText);
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