using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Hageeshow.TicTacToe
{
    public class TicTacToeButton : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private Human human;

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
            GameManager.instance.OnValidClick();
        }

        internal State GetState() => state;
    }
}