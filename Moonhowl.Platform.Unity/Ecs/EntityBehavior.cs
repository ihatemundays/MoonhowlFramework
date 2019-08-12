using Moonhowl.Framework.Ecs;
using Moonhowl.Platform.Unity.Utility;
using UnityEngine;

namespace Moonhowl.Platform.Unity.Ecs {
    [DisallowMultipleComponent]
    public class EntityBehavior: MonoBehaviour {
        [HideInInspector]
        public Entity entity = new Entity();

        public EntitySystem[] entitySystems;  
        private SystemMatcher<EntitySystem> _systemMatcher;

        protected void Start() {
            gameObject.SetEntity(entity); 
            _systemMatcher = new SystemMatcher<EntitySystem>(entitySystems);
        }
        
        protected async void Update() {
            await _systemMatcher.Match(entity);
        }
    }
}
