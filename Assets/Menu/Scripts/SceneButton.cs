using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Hageeshow.Menu
{
    public class SceneButton : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private string sceneName;

        public void OnPointerClick(PointerEventData pointerEventData)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}