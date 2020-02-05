using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;
using SharpDX.WIC;

using System;
using System.Drawing.Imaging;
using System.Xml.Serialization;

using GameLib.Enums;
using GameLib.Core;
using GameLib.Controller;
using GameLib.Transform;
using System.Text;
using GameLib.Params;

namespace GameLib.Models
{
    [Serializable]
    public class GameObject : IGameObject
    {
        // outside params
        public int deep;
        public float Opacity = 1;
        public ObjTurn Turn;
        public Vector2 MoveVec;

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
            bool free = true;
            String Anim = "Run";

            if (a.X == 0 && a.Y == 0) return;

            // check collision 
            if (a.X != 0)
            {
                if (a.X > 0)
                {
                    if (Collider.Right + Speed >= MapValues.MatrWidth) return;
                    for (int i = (int)Collider.Top + 10; i < (int)Collider.Bottom - 10; i++)
                    {
                        if (MapValues.mapMatrix[i, (int)(Collider.Right + Speed)] == 1)
                        {
                            free = false;
                            break;
                        }
                    }
                    Anim += "R";
                }
                else 
                {
                    if (Collider.Left - Speed <= 0) return;
                    for (int i = (int)Collider.Top + 10; i < (int)Collider.Bottom - 10; i++)
                    {
                        if (MapValues.mapMatrix[i, (int)(Collider.Left - Speed)] == 1)
                        {
                            free = false;
                            break;
                        }
                    }
                    Anim += "L"; 
                }
            }
            if (a.Y != 0)
            {
                if (a.Y > 0)
                {
                    if (Collider.Top - Speed <= 0) return;
                    for (int i = (int)Collider.Left + 10; i < (int)Collider.Right - 10; i++)
                    {
                        if (MapValues.mapMatrix[(int)(Collider.Top + Speed), i] == 1)
                        {
                            free = false;
                            break;
                        }
                    }
                    Anim += "T";
                }
                else 
                {
                    if (Collider.Top + Speed >= MapValues.MatrHeight) return;
                    for (int i = (int)Collider.Left + 10; i < (int)Collider.Right - 10; i++)
                    {
                        if (MapValues.mapMatrix[(int)(Collider.Bottom - Speed), i] == 1)
                        {
                            free = false;
                            break;
                        }
                    }
                    Anim += "B"; 
                }
            }
            if (free)
            {
                animationController.AnimationRun(Anim, true, 200);
                position ^= a;
                SetCollider();
            }
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
            MoveVec = new Vector2(0, 0);
            SetCollider();
            Speed = 5;
            visible = true;
        }
     
    }
}
