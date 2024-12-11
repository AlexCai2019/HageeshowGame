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

        private int stage;

        private void Awake()
        {
            Vector3 spawnPos = new(0.0F, GameManager.BOTTOM_Y, 0.0F);
            for (uint p = 0U; p < PLATFORMS; p++)
            {
                GameObject newPlatform = Instantiate(platformPrefab, spawnPos, platformPrefab.transform.rotation, transform);
                platformsArray[p] = newPlatform.GetComponent<Platform>();
                platformsArray[p].ResetPlatform(0); //一開始的不要有移動
            }
        }

        public void GameStart()
        {
            beforePlatform.SetActive(false);

            topY = GameManager.BOTTOM_Y;
            Vector3 screenRange = Camera.main.ScreenToWorldPoint(Vector3.zero);
            maxDistance = 2.0F; //剛開始的時候距離短一點

            //最下面那個一定要在中心
            platformsArray[0].transform.position = new(0.0F, topY, 0.0F);
            platformsArray[0].ResetPlatform(0); //一開始的不要有移動
            topY += Random.Range(minDistance, maxDistance);
            for (uint p = 1U; p < PLATFORMS; p++, topY += Random.Range(minDistance, maxDistance))
            {
                platformsArray[p].transform.position = new(Random.Range(screenRange.x, -screenRange.x), topY, 0.0F);
                platformsArray[p].ResetPlatform(0);
            }
            index = 0;

            stage = 0; //階段0
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
                lowestPlatform.ResetPlatform(stage); //以stage作為平台能否移動的機率 最低0 最高9
                topY += Random.Range(minDistance, maxDistance);

                index++;
                if (index == PLATFORMS)
                    index = 0U;
            }

            //距離設定
            if (stage == 9)
                return;

            /*if (topY < GameManager.BOTTOM_Y + 50.0F)
                stage = 0;
            else if (topY < GameManager.BOTTOM_Y + 100.0F)
                stage = 1;
            else if (topY < GameManager.BOTTOM_Y + 150.0F)
                stage = 2;
            else if (topY < GameManager.BOTTOM_Y + 200.0F)
                stage = 3;
            else if (topY < GameManager.BOTTOM_Y + 250.0F)
                stage = 4;
            else if (topY < GameManager.BOTTOM_Y + 300.0F)
                stage = 5;
            else if (topY < GameManager.BOTTOM_Y + 350.0F)
                stage = 6;
            else if (topY < GameManager.BOTTOM_Y + 400.0F)
                stage = 7;
            else if (topY < GameManager.BOTTOM_Y + 450.0F)
                stage = 8;
            else
                stage = 9;*/
            stage = System.Math.Min((int)((topY - GameManager.BOTTOM_Y) / 50), 9);

            maxDistance = 2.0F + stage / 10.0F;
        }
    }
}