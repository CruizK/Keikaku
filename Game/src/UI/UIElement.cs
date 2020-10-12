using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Keikaku.UI
{
    public class UIElement
    {

        public string Name;

        public Vector2 Position;
        public Vector2 Scale;
        public float Rotation;

        public Vector2 Size = Vector2.Zero;

        public Matrix Transform { get; set; }

        public List<UIElement> Children = new List<UIElement>();

        public UIElement Parent = null;

        public UIElement()
        {
            Position = Vector2.Zero;
            Scale = Vector2.One;
            Rotation = 0.0f;
            Transform = Matrix.Identity;
            Transform = CreateTransform();
        }

        public UIElement(Vector2 position, Vector2 size)
        {
            Position = position;
            Size = size;
        }

        public void SetPosition(float x, float y)
        {
            Position = new Vector2(x, y);
        }


        protected Vector2 GetWorldCoords()
        {
            Matrix transform = CreateTransform();
            UIElement parent = Parent;
            while(parent != null)
            {
                transform *= parent.CreateTransform();
                parent = parent.Parent;
            }

            return new Vector2(transform.Translation.X, transform.Translation.Y);
        }

        public void AddChild(UIElement child)
        {
            child.Parent = this;
            Transform = CreateTransform();
            Children.Add(child);
        }

        private Matrix CreateTransform()
        {
            return Matrix.CreateTranslation(new Vector3(Position.X, Position.Y, 1)) *
                Matrix.CreateScale(new Vector3(Scale.X, Scale.Y, 1)) *
                Matrix.CreateRotationZ(Rotation);
        }

        public virtual void Init()
        {
            foreach (UIElement child in Children)
            {
                child.Init();
            }
        }

        public virtual void LoadContent(ContentManager content)
        {
            foreach(UIElement child in Children)
            {
                child.LoadContent(content);
            }
        }

        public virtual void UnloadContent(ContentManager content)
        {
            foreach (UIElement child in Children)
            {
                child.UnloadContent(content);
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (UIElement child in Children)
            {
                child.Update(gameTime);
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

            foreach (UIElement child in Children)
            {
                child.Draw(spriteBatch);
            }
        }
    }
}
