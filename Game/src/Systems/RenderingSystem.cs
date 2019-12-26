using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Keikaku.Components;

namespace Keikaku.Systems
{
    public class RenderingSystem : BaseSystem
    {
        public RenderingSystem()
        {
            RequiredComponents.Add(ComponentCounter.getIndex(typeof(SpriteComponent)));
            RequiredComponents.Add(ComponentCounter.getIndex(typeof(Transform)));
        }

        // Need a way to distinguish which Component[] is which in order for them to be able to use
        // multiple components in the same system, Each System would need to have a list
        public override void LoadContent(ContentManager content)
        {
            foreach(Entity e in WorkingEntities)
            {
                SpriteComponent sprite = e.GetComponent<SpriteComponent>();
                sprite.Texture = content.Load<Texture2D>(sprite.TexturePath);
                sprite.uvRectangle = sprite.Texture.Bounds;
            }
        }

        public void UnloadContent(ContentManager content)
        {
            
        }

        public void Update(GameTime gameTime)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach(Entity e in WorkingEntities)
            {
                SpriteComponent sprite = e.GetComponent<SpriteComponent>();
                Transform transform = e.GetComponent<Transform>();

                /*
                AnimationComponent anim = e.GetComponent<AnimationComponent>();
                if(anim != null)
                { 
                    // Set bounding box based on current animation timing
                }
                */

                spriteBatch.Draw(sprite.Texture, transform.Position, sprite.uvRectangle, sprite.Color, 
                    transform.Rotation, Vector2.Zero, transform.Scale, SpriteEffects.None, 0.0f);

            }
        }
    }
}
