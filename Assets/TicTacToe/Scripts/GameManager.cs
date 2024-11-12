using UnityEngine;

namespace Hageeshow.TicTacToe
{
    public class GameManager : GenericGameManager
    {
        public static GameManager instance;

        [SerializeField]
        private Human human;
        [SerializeField]
        private ArtificialIntelligence ai;
        public TicTacToeButton[] map;

        private uint spaces;

        public readonly int[,] winning =
        {
            {0, 1, 2}, {3, 4, 5}, {6, 7, 8},  // ¾î¦C
	    	{0, 3, 6}, {1, 4, 7}, {2, 5, 8},  // ª½¦æ
	    	{0, 4, 8}, {2, 4, 6}   // ±×½u
	    };

        private void Awake()
        {
            instance = this;

            gameObjects.Add(human);
            gameObjects.Add(ai);
        }

        private void OnDestroy()
        {
            instance = null;
        }

        public uint GetSpaces() => spaces;

        public void OnValidClick() => spaces--;
    }

    public enum State
    {
        EMPTY,
        HAGEE,
        CHOCOLATE
    }
}