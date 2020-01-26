using GameLib.Enums;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace GameLib.Models
{
    [Serializable]
    public class Level
    {
        [XmlArray("GameObjectList"), XmlArrayItem(typeof(GameObject), ElementName = "GameObject")]
        public List<GameObject> ObjectsOnScene;

        [XmlArray("WallsList"), XmlArrayItem(typeof(Barrier), ElementName = "Barrier")]
        public List<Barrier> WallsOnScene;

        [XmlIgnore]
        [NonSerialized]
        public bool DeleteStatus = false;
        [XmlIgnore]
        [NonSerialized]
        public IGameObject ForDeleting;

        public Barrier BackGround;

        public int LevelId { get; set; }

        public void AddObject(IGameObject gameObj, ObjConstructorTag constructorTag) 
        {
            if (constructorTag == ObjConstructorTag.GameObj)
                ObjectsOnScene.Add((GameObject)gameObj);
            else if (constructorTag == ObjConstructorTag.Barrier)
                WallsOnScene.Add((Barrier)gameObj);
        }
     
    }
}
