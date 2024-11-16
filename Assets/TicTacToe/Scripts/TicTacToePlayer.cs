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
        private Text resultText;
        private uint winTime = 0U;
        private uint tieTime = 0U;

        protected bool isMyTurn;

        [SerializeField]
        private AudioClip[] clips;
        private AudioSource audioSource;
        private int clipsIndex = 0;

        [SerializeField]
        private TicTacToePlayer opponent;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public virtual void GameStart() { }

        public virtual void Gaming() { }

        public virtual void FixedGaming() { }

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

        public void Tie()
        {
            tieTime++;
            GameEnd(false);
        }

        public virtual void GameEnd(bool isWon)
        {
            isMyTurn = false;
            if (isWon)
                winTime++;
            resultText.text = $"{winTime} / {tieTime}";
        }
    }
}
