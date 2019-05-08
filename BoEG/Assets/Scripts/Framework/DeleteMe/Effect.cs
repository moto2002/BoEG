using UnityEngine;

namespace Modules
{
    public class Effect : IEffect
    {
        
        protected GameObject Source { get; private set; }
        protected GameObject Target { get; private set; }
        public virtual void Initialize(GameObject source, GameObject target)
        {
            Target = target;
        }

        public virtual void PreTick()
        {
        }

        public virtual void Tick()
        {
        }

        public virtual void PostTick()
        {
        }

        public virtual bool ShouldTerminate
        {
            get { return true; }
        }
        public virtual void Terminate()
        {
        }
    }
}