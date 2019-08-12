using System;
using System.Collections.Generic;
using System.Linq;

namespace Moonhowl.Framework.Ecs {
    public class Entity {
        private readonly Dictionary<Type, IEntityComponent> _components  = new Dictionary<Type, IEntityComponent>();
        private readonly Dictionary<Type, EntityComponentState> _componentStates = new Dictionary<Type, EntityComponentState>();
        private readonly HashSet<IEntitySystem> _systems = new HashSet<IEntitySystem>(); 

        public Entity() { }

        public Entity(IEnumerable<IEntityComponent> components) {
            AddComponents(components);
        }

        public bool TryComponent<T>() where T: IEntityComponent {
            var (_, exists) = TryComponentExists<T>();

            return exists;
        }
        
        public bool TryComponent<T>(out T component) where T: IEntityComponent {
            var (localComponent, exists) = TryComponentExists<T>();

            component = localComponent;

            return exists;
        }

        public List<IEntityComponent> GetComponents() => _components.Values.ToList();
        
        public T GetComponent<T>() where T: IEntityComponent {
            var (component, _) = TryComponentExists<T>();

            return component;
        }
        
        public (T, bool) TryComponentExists<T>() where T: IEntityComponent {
            return !_components.TryGetValue(typeof(T), out var component) ?
                (default(T), false) : ((T) component, true);
        }
        
        public EntityComponentState GetComponentState<T>() where T: IEntityComponent {
            return _componentStates.TryGetValue(typeof(T), out var entityComponentState) ? 
                entityComponentState : 
                EntityComponentState.NotFound;
        }
        
        public void ClearComponentStates() => _componentStates.Clear();
        
        public Entity AddComponents(IEnumerable<IEntityComponent> components) {
            foreach (var component in components) {
                AddComponent(component);
            }

            return this;
        }

        public Entity AddComponent<T>(T component) where T: IEntityComponent {
            SetComponentState<T>(EntityComponentState.Added);
            if (_components.TryGetValue(typeof(T), out _)) {
                _components[typeof(T)] = component;

                return this;
            }
        
            _components.Add(typeof(T), component);            

            return this;
        }

        public Entity RemoveComponent<T>() where T: IEntityComponent {
            if (_components.Remove(typeof(T))) {
                SetComponentState<T>(EntityComponentState.Removed);                
            }

            return this;
        }
        
        private void SetComponentState<T>(EntityComponentState componentState) {
            if (_componentStates.TryGetValue(typeof(T), out _)) {
                _componentStates[typeof(T)] = componentState;
            } else {
                _componentStates.Add(typeof(T), componentState);
            }
        }

        public void AddSystem(IEntitySystem system) => _systems.Add(system);

        public void RemoveSystem(IEntitySystem system) => _systems.Remove(system);
    }
}
