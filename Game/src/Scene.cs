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


namespace Keikaku
{
    public class Scene
    {

        List<Entity> entities = new List<Entity>();


        public Scene()
        {
            
        }

        public void AddGameObject(Entity e)
        {
            entities.Add(e);
        }

        public void LoadContent(ContentManager content)
        {

        }

        public void UnloadContent(ContentManager content)
        {

        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
 
        }
    }
}
