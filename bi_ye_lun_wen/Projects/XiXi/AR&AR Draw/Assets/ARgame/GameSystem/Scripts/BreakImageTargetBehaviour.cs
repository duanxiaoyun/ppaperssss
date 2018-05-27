namespace EasyAR
{
    public class BreakImageTargetBehaviour : ImageTargetBehaviour
    {
        public BreakImageTarget target;

        protected override void Awake()
        {
            base.Awake();
            if(target==null)
                target = GetComponent<BreakImageTarget>();
            target.Init(this);
        }
    }
}