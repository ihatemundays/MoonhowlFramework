using System;
using System.Collections.Generic;
using System.Linq;

namespace Moonhowl.Framework.Ecs {
    public class Entity {
        private readonly Dictionary<Type, IEntityComponent> _components  = new Dictionary<Type, IEntityComponent>();
        private Dictionary<Type, EntityComponentState> _componentStates = new Dictionary<Type, EntityComponentState>();

        public Entity() { }

        public Entity(List<IEntityComponent> components) {
            AddComponents(components);
        }

        public bool TryComponent<T>(out T component) where T: IEntityComponent {
            component = GetComponent<T>();
            
            return component != null;
        }

        public List<IEntityComponent> GetComponents() {
            return _components.Values.ToList();
        }
        
        public T GetComponent<T>() where T: IEntityComponent {
            if (!_components.TryGetValue(typeof(T), out var component)) {
                return default(T);
            }

            return (T) component;
        }
        
        public EntityComponentState CheckComponent<T>() where T: IEntityComponent {
            return _componentStates.TryGetValue(typeof(T), out var entityComponentState) ? 
                entityComponentState : 
                EntityComponentState.NotFound;
        }
        
        public void ClearComponentStates() {
            _componentStates.Clear();            
        }
        
        public Entity AddComponents(List<IEntityComponent> components) {
            components.ForEach(component => AddComponent(component));

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
        
        private void SetComponentState<T>(EntityComponentState entityComponentState) {
            if (_componentStates.TryGetValue(typeof(T), out _)) {
                _componentStates[typeof(T)] = entityComponentState;
            } else {
                _componentStates.Add(typeof(T), entityComponentState);
            }
        }       
    }
}
