using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;
using SharpDX.WIC;

using System;
using System.Drawing.Imaging;
using System.Xml.Serialization;

using GameLib.Enums;
using GameLib.Controller;
using GameLib.Transform;

namespace GameLib.Models
{
    [Serializable]
    public class GameObject : IGameObject
    {
        // outside params
        public int deep;
        public float Opacity = 1;
        public ObjTurn Turn;

        
        // dinamic params
        public int Speed;

        // inside params
        public string name;
        
        [XmlIgnore]
        [NonSerialized]
        public AnimationController animationController;


        public GameObject() 
        {
            initParams();
        }

        public GameObject(string name)
        {
            initParams(name:name);
        }
        public GameObject(string name, Size s)
        {
            initParams(null, s, name);
        }
        public GameObject(string name, Size s, Vector2 pos)
        {
            initParams(pos, s, name);
        }

        public void Move(Vector2 a) 
        {
            position ^= a;
            SetCollider();
        }

        public void SetCollider() 
        {
            Collider.Left   = position.X - Size.Width / 2;
            Collider.Right  = position.X + Size.Width / 2;
            Collider.Top    = position.Y - Size.Height / 2;
            Collider.Bottom = position.Y + Size.Height / 2;
        }

        public void initParams(Vector2 pos = null, Size s = null, string name = "gameObj") 
        {
            objectTag = Tag.HERO;
            if (s == null) s = new Size(100,100);
            Size = s;
            if (pos == null)
                this.position = new Vector2(0, 0);
            else
                this.position = pos;
            this.name = name;
            
            SetCollider();
            Speed = 5;
            visible = true;
        }
     
    }
}
