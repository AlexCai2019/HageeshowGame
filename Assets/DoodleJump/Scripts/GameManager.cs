using UnityEngine;

namespace Hageeshow.DoodleJump
{
    public class GameManager : GenericGameManager, IHitPlatformEvent
    {
        public static GameManager instance;

        public const float BOTTOM_Y = -50000.0F;

        [SerializeField]
        private Hagee hagee;
        [SerializeField]
        private PlatformGenerator platformGenerator;
        [SerializeField]
        private CameraFollow cameraFollow;

        private readonly IHitPlatformEvent[] hitObjects = new IHitPlatformEvent[3];

        private void Awake()
        {
            instance = this;

            gameObjects.Add(hagee);
            gameObjects.Add(cameraFollow);
            gameObjects.Add(platformGenerator);

            hitObjects[0] = hagee;
            hitObjects[1] = cameraFollow;
            hitObjects[2] = platformGenerator;
        }

        private void OnDestroy()
        {
            instance = null;
        }

        public void HitPlatform()
        {
            if (isGaming)
                foreach (IHitPlatformEvent obj in hitObjects)
                    obj.HitPlatform();
        }

        protected override bool StartCondition()
        {
            return Input.GetKeyDown(KeyCode.Space);
        }
    }
}