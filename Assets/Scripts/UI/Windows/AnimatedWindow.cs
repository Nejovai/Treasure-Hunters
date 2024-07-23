using UnityEngine;

namespace PixelCrew.UI
{
    public class AnimatedWindow : MonoBehaviour
    {
        private Animator _anim;
        private static readonly int Show = Animator.StringToHash("Show");
        private static readonly int Hide = Animator.StringToHash("Hide");

        protected virtual void Start ()
        {
            _anim = GetComponent<Animator>();
            _anim.SetTrigger(Show);
        }

        public void Close()
        {
            _anim.SetTrigger(Hide);
        }

        public virtual void OnCloseAnimationComplete()
        {
            Destroy(gameObject);
        }
    }
}
