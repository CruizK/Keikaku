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

        public Vector2 Position { get; private set; }
        public Vector2 Scale { get; private set; }
        public float Rotation { get; private set; }

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

        public virtual void SetPosition(Vector2 position)
        {
            Position = position;
            Transform = CreateTransform();
            PercalateTransform();
        }

        public void SetScale(Vector2 scale)
        {
            Scale = scale;
            PercalateTransform();
        }

        public void SetRotation(float rotation)
        {
            Rotation = rotation;
            PercalateTransform();
        }

        private void PercalateTransform()
        {
            
            foreach(UIElement child in Children)
            {
                Console.WriteLine("Percalating From {0}, To {1}", Name, child.Name);
                child.ApplyTransform(Transform);
            }
        }

        protected Vector2 GetLocalPos()
        {
            return new Vector2(Transform.Translation.X, Transform.Translation.Y);
        }

        public void AddChild(UIElement child)
        {
            child.Parent = this;
            Children.Add(child);
        }

        private Matrix CreateTransform()
        {
            return Matrix.CreateTranslation(new Vector3(Position.X, Position.Y, 1)) *
                Matrix.CreateScale(new Vector3(Scale.X, Scale.Y, 1)) *
                Matrix.CreateRotationZ(Rotation);
        }

        public void ApplyTransform(Matrix transform)
        {
            Transform = CreateTransform() * transform;
            PercalateTransform();
        }

        public virtual void Init()
        {
            foreach (UIElement child in Children)
            {
                child.Init();
            }

            PercalateTransform();
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
