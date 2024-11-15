using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Hageeshow.TicTacToe
{
    public class TicTacToeButton : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private Human human;
        [SerializeField]
        private Sprite cardSprite;

        private State state = State.EMPTY;

        private Image image;

        private void Awake()
        {
            image = GetComponent<Image>();
        }

        public void OnPointerClick(PointerEventData pointerEventData)
        {
            human.OnClick(this);
        }

        internal void OnValidClick(TicTacToePlayer player)
        {
            state = player.myState;
            image.sprite = player.mySprite;
        }

        public State GetState() => state;

        internal void ResetButton()
        {
            image.sprite = cardSprite;
            state = State.EMPTY;
        }
    }
}