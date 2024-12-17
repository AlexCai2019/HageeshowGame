using UnityEngine;

namespace Hageeshow.Snake
{
    public class GameManager : GenericGameManager
    {
        [SerializeField]
        private Hagee hagee;
        [SerializeField]
        private TitleText titleText;

        private void Awake()
        {
            gameObjects.Add(hagee);
            gameObjects.Add(titleText);
        }

        protected override bool StartCondition()
        {
            return Input.GetKeyDown(KeyCode.Space);
        }
    }
}