using UnityEngine;

namespace Hageeshow.Minesweeper
{
    public class Card : MonoBehaviour, IClickEvent
    {
        public const uint MINE = 9;

        private uint x;
        private uint y;
        private uint val = 0U; //初始化為0
        private CardState state = CardState.COVER;

        private SpriteRenderer spriteRenderer;
        private Sprite valSprite;

        private GameObject flagObject;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            flagObject = transform.GetChild(0).gameObject;
        }

        private void OnMouseOver()
        {
            if (Input.GetMouseButtonUp(0)) //左鍵
                ClickCard(x, y);
            else if (Input.GetMouseButtonUp(1)) //右鍵
                UpdateFlag();
        }

        internal void InitializeXY(uint x, uint y)
        {
            this.x = x;
            this.y = y;
        }

        internal void SetVal(uint val, Sprite valSprite)
        {
            this.val = val;
            this.valSprite = valSprite;
        }

        internal uint GetVal() => val;

        internal void ResetCard(Sprite backSprite)
        {
            val = 0U;
            spriteRenderer.sprite = valSprite = backSprite;
            state = CardState.COVER;
            flagObject.SetActive(false);
        }

        public void ClickCard(uint x, uint y)
        {
            if (state == CardState.COVER) //蓋著的 且沒有旗子
                GameManager.instance.ClickCard(x, y);
        }

        internal bool ForceFlip(bool disableFlag)
        {
            if (state == CardState.OPEN)
                return false; //已經開過了

            //強制開啟
            state = CardState.OPEN;
            if (disableFlag)
                flagObject.SetActive(false);

            spriteRenderer.sprite = valSprite;
            return true;
        }

        public void UpdateFlag()
        {
            if (state == CardState.COVER)
            {
                flagObject.SetActive(true);
                state = CardState.FLAG;
                GameManager.instance.UpdateFlag(true);
            }
            else if (state == CardState.FLAG)
            {
                flagObject.SetActive(false);
                state = CardState.COVER;
                GameManager.instance.UpdateFlag(false);
            }
        }
    }

    enum CardState
    {
        COVER,
        OPEN,
        FLAG
    }
}