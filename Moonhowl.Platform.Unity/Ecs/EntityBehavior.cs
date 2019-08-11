using Moonhowl.Framework.Ecs;
using Moonhowl.Platform.Unity.Utility;
using UnityEngine;

namespace Moonhowl.Platform.Unity.Ecs {
    [DisallowMultipleComponent]
    public class EntityBehavior<T>: MonoBehaviour where T: EntitySystemBehavior {
        [HideInInspector]
        public Entity entity = new Entity();
        private readonly SystemMatcher<EntitySystem<T>> _systemMatcher = new SystemMatcher<EntitySystem<T>>();

        protected void Start() {
            gameObject.SetEntity(entity);    
        }
        
        protected async void Update() {
            await _systemMatcher.MatchSystems(entity);
        }
    }
}
