using Moonhowl.Framework.Ecs;
using Moonhowl.Platform.Unity.Utility;
using UnityEngine;

namespace Moonhowl.Platform.Unity.Ecs {
    [DisallowMultipleComponent]
    public class EntityBehavior<T>: MonoBehaviour where T: EntitySystemBehavior {
        [HideInInspector]
        public Entity entity = new Entity();

        public EntitySystem<T>[] entitySystems;  
        private SystemMatcher<EntitySystem<T>> _systemMatcher;

        protected void Start() {
            gameObject.SetEntity(entity); 
            _systemMatcher = new SystemMatcher<EntitySystem<T>>(entitySystems);
        }
        
        protected async void Update() {
            await _systemMatcher.Match(entity);
        }
    }
}
