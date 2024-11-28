using UnityEngine;

namespace Hageeshow.DoodleJump
{
    public class PlatformGenerator : MonoBehaviour, IGameState
    {
        [SerializeField]
        private GameObject platformPrefab;
        [SerializeField]
        private GameObject beforePlatform;

        private const uint PLATFORMS = 20U;
        private float topY;
        private readonly float minDistance = 1.0F;
        private float maxDistance;

        private readonly Platform[] platformsArray = new Platform[PLATFORMS];
        private uint index;

        public void GameStart()
        {
            beforePlatform.SetActive(false);

            topY = GameManager.BOTTOM_Y;
            Vector3 screenRange = Camera.main.ScreenToWorldPoint(Vector3.zero);
            maxDistance = 2.0F; //剛開始的時候距離短一點

            //把20個平台放到堆疊裡
            //最下面那個一定要在中心
            GameObject newPlatform;
            newPlatform = Instantiate(platformPrefab, new(0.0F, topY, 0.0F), platformPrefab.transform.rotation, transform);
            platformsArray[0] = newPlatform.GetComponent<Platform>();
            platformsArray[0].ResetPlatform(0); //第一格不能是移動
            topY += Random.Range(minDistance, maxDistance);
            for (uint p = 1U; p < PLATFORMS; p++, topY += Random.Range(minDistance, maxDistance))
            {
                newPlatform = Instantiate(platformPrefab, new(Random.Range(screenRange.x, -screenRange.x), topY, 0.0F), platformPrefab.transform.rotation, transform);
                platformsArray[p] = newPlatform.GetComponent<Platform>();
                platformsArray[p].ResetPlatform(0);
            }
            index = 0;
        }

        public void Gaming()
        {
            //把超出螢幕範圍的放到最上面
            Vector3 screenRange = Camera.main.ScreenToWorldPoint(Vector3.zero);
            while (true)
            {
                Platform lowestPlatform = platformsArray[index]; //index會維持在目前最低的
                if (lowestPlatform.transform.position.y > screenRange.y)
                    break;
                lowestPlatform.transform.position = new(Random.Range(screenRange.x, -screenRange.x), topY, 0.0F); //放到最上面
                lowestPlatform.ResetPlatform(maxDistance); //以maxDistance作為平台能否移動的機率
                topY += Random.Range(minDistance, maxDistance);

                index++;
                if (index == PLATFORMS)
                    index = 0U;
            }

            //距離設定
            if (maxDistance >= 3.0F)
                return;

            if (topY < GameManager.BOTTOM_Y + 50.0F)
                maxDistance = 2.0F;
            else if (topY < GameManager.BOTTOM_Y + 100.0F)
                maxDistance = 2.1F;
            else if (topY < GameManager.BOTTOM_Y + 150.0F)
                maxDistance = 2.2F;
            else if (topY < GameManager.BOTTOM_Y + 200.0F)
                maxDistance = 2.3F;
            else if (topY < GameManager.BOTTOM_Y + 250.0F)
                maxDistance = 2.4F;
            else if (topY < GameManager.BOTTOM_Y + 300.0F)
                maxDistance = 2.5F;
            else if (topY < GameManager.BOTTOM_Y + 350.0F)
                maxDistance = 2.6F;
            else if (topY < GameManager.BOTTOM_Y + 400.0F)
                maxDistance = 2.7F;
            else if (topY < GameManager.BOTTOM_Y + 450.0F)
                maxDistance = 2.8F;
            else if (topY < GameManager.BOTTOM_Y + 500.0F)
                maxDistance = 2.9F;
            else if (topY < GameManager.BOTTOM_Y + 550.0F)
                maxDistance = 3.0F;
        }
    }
}