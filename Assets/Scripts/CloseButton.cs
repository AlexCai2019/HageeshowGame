using UnityEngine.SceneManagement;

namespace Hageeshow
{
    public class CloseButton : GenericButton
    {
        public override void OnClick()
        {
            SceneManager.LoadScene("MenuScene");
        }
    }
}