using UnityEngine;
using UnityEngine.UI;

namespace Hageeshow.TicTacToe
{
    [RequireComponent(typeof(AudioSource))]
    public abstract class TicTacToePlayer : MonoBehaviour, IGameState
    {
        [SerializeField]
        internal Sprite mySprite;
        [SerializeField]
        internal State myState;
        [SerializeField]
        private Text winTimeText;
        private uint winTime = 0U;

        protected bool isMyTurn;

        [SerializeField]
        protected AudioClip[] clips;
        protected AudioSource audioSource;
        protected int clipsIndex = 0;

        [SerializeField]
        protected TicTacToePlayer opponent;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public abstract void GameStart();

        public abstract void Gaming();

        public void OnClick(TicTacToeButton button)
        {
            if (!isMyTurn || button.GetState() != State.EMPTY) //不是應該下的時候
                return;

            button.OnValidClick(this);

            audioSource.clip = clips[clipsIndex++];
            audioSource.Play();
            if (clipsIndex == clips.Length)
                clipsIndex = 0;

            isMyTurn = false;
            opponent.isMyTurn = true;

            GameManager.instance.OnValidClick();
        }

        public void GameEnd(bool isWon)
        {
            isMyTurn = false;
            if (isWon)
            {
                winTime++;
                winTimeText.text = winTime.ToString();
            }
        }
    }
}
