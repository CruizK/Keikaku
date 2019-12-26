using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

using Keikaku.Components;

namespace Keikaku
{
    public class Entity
    {
        uint ID;
        Dictionary<Type, Component> components;
        BitArray bitArray;

        public Entity(uint ID)
        {
            components = new Dictionary<Type, Component>();
            bitArray = new BitArray(32);
            
            this.ID = ID;
        }

        public BitArray GetComponentBits()
        {
            return bitArray;
        }

        public T GetComponent<T>() where T : Component
        {
            if(!components.ContainsKey(typeof(T)))
            {
                Console.WriteLine("Component does not have queried component, returning null");
                return null;
            }
            return (T)components[typeof(T)];
        }

        public void AddComponent(Component c)
        {
            bitArray.Set(c.Index, true);
            components.Add(c.GetType(), c);
        }

        public void RemoveComponent<T>()
        {
            bitArray.Set(components[typeof(T)].Index, false);
            components.Remove(typeof(T));
        }
    }
}
