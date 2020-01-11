using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Keikaku.UI
{
    public class VerticalContainer : UIElement
    {

        public int Spacing;

        public VerticalContainer()
        {

        }

        public override void Init()
        {
            base.Init();
            int perSpacing = 0;
            foreach(UIElement child in Children)
            {
                child.SetPosition(new Vector2(child.Position.X, child.Position.Y + perSpacing));
                perSpacing += (int)child.Size.Y + Spacing;
            }
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
        }
    }
}
