using System.Collections.Generic;

namespace Entities.Systems
{
    //TODO: Finish the system
    public class EntitySystem<TSystemArgs>
    {
        protected readonly Dictionary<Entity, TSystemArgs> entityToSystemArgsLut;

        public EntitySystem()
        {
            entityToSystemArgsLut = new Dictionary<Entity, TSystemArgs>();
        }

        public void Subscribe(Entity entity, TSystemArgs component)
        {
            entityToSystemArgsLut.Add(entity, component);
        }

        public void Unsubscribe(Entity entity)
        {
            entityToSystemArgsLut.Remove(entity);
        }

        protected virtual void Update()
        {
        }

        protected virtual void FixedUpdate()
        {
        }
    }
}