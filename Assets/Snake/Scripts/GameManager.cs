using UnityEngine;

namespace Hageeshow.Snake
{
    public class GameManager : GenericGameManager, IEatFoodEvent
    {
        public static GameManager instance;

        [SerializeField]
        private Hagee hagee;
        [SerializeField]
        private Food food;
        [SerializeField]
        private TitleText titleText;

        private readonly IEatFoodEvent[] eatObjects = new IEatFoodEvent[2];

        private void Awake()
        {
            instance = this;
            
            gameObjects.Add(hagee);
            gameObjects.Add(food);
            gameObjects.Add(titleText);

            eatObjects[0] = hagee;
            eatObjects[1] = food;
        }

        private void OnDestroy()
        {
            instance = null;
        }

        protected override bool StartCondition()
        {
            return Input.GetKeyDown(KeyCode.Space);
        }

        public void EatFood()
        {
            foreach (IEatFoodEvent obj in eatObjects)
                obj.EatFood();
        }
    }
}