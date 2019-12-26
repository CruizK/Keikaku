using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Keikaku.Components;

namespace Keikaku.Systems
{
    public class BaseSystem
    {

        // Maybe seperate systems into 3 Interfaces??
        // 1: IContentSystem - Load & Unload
        // 2: IUpdateSystem - Update
        // 3: IDrawSystem - Draw
        public List<int> RequiredComponents;
        public List<Entity> WorkingEntities;

        public BaseSystem()
        {
            RequiredComponents = new List<int>();
            WorkingEntities = new List<Entity>();
        }

        public virtual void LoadContent(ContentManager content)
        {

        }

        public virtual void UnloadContent(ContentManager content)
        {

        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
