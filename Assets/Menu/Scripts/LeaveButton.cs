using UnityEngine;

namespace Hageeshow.Menu
{
    public class LeaveButton : GenericButton
    {
        public override void OnClick()
        {
            Application.Quit();
        }
    }
}