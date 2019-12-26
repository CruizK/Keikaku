using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keikaku.Components
{
    public class ComponentCounter
    {
        private static Dictionary<Type, int> componentIndexes = new Dictionary<Type, int>();
        private static int counter = 0;
        public static int getIndex(Type t)
        {
            if (!componentIndexes.ContainsKey(t))
            {
                Console.WriteLine("SETTING COMPONENT: <" + t.Name + "> TO INDEX: " + counter);
                componentIndexes[t] = counter;
                counter++;
            }

            return componentIndexes[t];
        }
    }

    public class Component
    {
        public int Index { get; private set; }
        public Component()
        {
            Index = ComponentCounter.getIndex(GetType());
        }
    }
}
