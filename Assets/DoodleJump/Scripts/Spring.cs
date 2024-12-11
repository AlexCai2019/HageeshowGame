using UnityEngine;

namespace Hageeshow.DoodleJump
{
    [RequireComponent(typeof(Animator))]
    public class Spring : GenericPlatform
    {
        private Animator animator;

        protected override void Awake()
        {
            base.Awake();
            animator = GetComponent<Animator>();
        }

        protected override void ValidCollide()
        {
            base.ValidCollide();
            animator.SetBool("isCompress", true);
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            animator.SetBool("isCompress", false);
        }
    }
}
