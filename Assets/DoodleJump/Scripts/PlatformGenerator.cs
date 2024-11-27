using System.Collections.Generic;
using UnityEngine;

namespace Hageeshow.DoodleJump
{
    public class PlatformGenerator : MonoBehaviour, IGameState, IHitPlatformEvent
    {
        [SerializeField]
        private GameObject platformPrefab;
        [SerializeField]
        private GameObject beforePlatform;

        private const uint PLATFORMS = 20U;
        private float topY;
        private float bottomY;

        private readonly Stack<GameObject> platformsStack = new();

        public void GameStart()
        {
            beforePlatform.SetActive(false);

            topY = bottomY = GameManager.BOTTOM_Y;
            Vector3 screenRange = Camera.main.ScreenToWorldPoint(Vector3.zero);

            //把20個平台放到堆疊裡
            //最下面那個一定要在中心
            platformsStack.Push(Instantiate(platformPrefab, new(0.0F, topY++, 0.0F), platformPrefab.transform.rotation, transform.parent));
            for (uint p = 1U; p < PLATFORMS; p++)
                platformsStack.Push(Instantiate(platformPrefab, new(Random.Range(screenRange.x, -screenRange.x), topY++, 0.0F), platformPrefab.transform.rotation, transform.parent));
        }

        public void HitPlatform()
        {
            Debug.Log($"{topY} {bottomY} {Camera.main.ScreenToWorldPoint(Vector3.zero)}");
            for (Vector3 screenRange = Camera.main.ScreenToWorldPoint(Vector3.zero); bottomY < screenRange.y; topY++, bottomY++) //清除超出畫面的平台
            {
                GameObject lowestPlatform = platformsStack.Pop();
                lowestPlatform.transform.position = new(Random.Range(screenRange.x, -screenRange.x), topY, 0.0F); //放到最上面
                platformsStack.Push(lowestPlatform);
            }
        }
    }
}