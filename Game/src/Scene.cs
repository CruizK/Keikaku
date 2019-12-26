using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using Keikaku.Components;
using Keikaku.Systems;


namespace Keikaku
{
    public class Scene
    {

        /* Method A
        List<Entity> entities = new List<Entity>();

        // Method B
        Dictionary<Entity, List<Component>> mapping = new Dictionary<Entity, List<Component>>();
        */
        // Method C

        List<Entity> entities = new List<Entity>();

        //Fucking with sytems
        List<BaseSystem> systems = new List<BaseSystem>();

        public Scene()
        {
            
            Entity player = new Entity(0);
            player.AddComponent(new Transform { Position = new Vector2(0, 0) });
            player.AddComponent(new SpriteComponent { TexturePath = "sprite_sheet" });
            
            entities.Add(player);
            
            /* Method A
            Entity e = new Entity(1);
            e.AddComponent(new BoxCollider());
            entities.Add(e);
    
            // Method B
            mapping.Add(new Entity(11), new List<Component>() { new BoxCollider() });
            */
            // Method C

            // First You Add some components
            //AddComponentToDatabase(typeof(BoxCollider));
            //AddComponentToDatabase(typeof(SpriteRenderer));


            systems.Add(new RenderingSystem());

            // Every component probably needs some uint index to tell it "Hey, I'm on the 6th entity"
            // Since Entities are just uints in this situation, they don't need to know about anything atall


            //Fucking around
            /*
            BitArray bittss = new BitArray(32);
            bittss.Set(0, true);
            Console.WriteLine(comps.First().Key.Get(0));
            Console.WriteLine(comps[bittss]);
            */

            // Mapping of ClassTypes - BitMasks is the only way  I see this being done in a way that involves bitMasks
            // Should probably be stored in some sort of componentHandler
            // A class that handles us adding and deleting??? deleting will cause problems, maybe,
            // deleting will cause everything to shift over past the index of the current one deleted.
            // Or we could keep note of empty bit sets of deleted components and just do it that way
            // Console.WriteLine(comps[0].FirstOrDefault().GetType());

            UpdateSystems();
        }
        
        public void UpdateSystems()
        {
            foreach(BaseSystem system in systems)
            {
                system.WorkingEntities.Clear();
                foreach(Entity e in entities)
                {
                    bool isIn = true;
                    foreach(int i in system.RequiredComponents)
                    {
                        if(!e.GetComponentBits().Get(i))
                        {
                            isIn = false;
                            break;
                        }
                    }

                    if (isIn)
                        system.WorkingEntities.Add(e);
                }
            }
        }


        public void AddEntity(Entity e)
        {
            //entities.Add(e);
        }

        public void LoadContent(ContentManager content)
        {
            foreach( BaseSystem system in systems)
            {
                system.LoadContent(content);
            }
        }

        public void UnloadContent(ContentManager content)
        {

        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach( BaseSystem system in systems)
            {
                system.Draw(spriteBatch);
            }
        }
    }
}
