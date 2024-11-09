using UnityEngine;
using UnityEngine.UI;

namespace Hageeshow
{
    [RequireComponent(typeof(Text))]
    public class GenericText : MonoBehaviour
    {
        protected Text myText;

        private void Awake()
        {
            myText = GetComponent<Text>();
        }
    }
}