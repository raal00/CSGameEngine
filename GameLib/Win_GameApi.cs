using System;
using System.Xml.Serialization;
using System.IO;

using GameLib.Core;
using GameLib.Models;
using GameLib;
using GameLib.Enums;
using GameLib.Interaction;
using GameLib.Params;

namespace GameLib
{
    public class Win_GameApi : IGameApi
    {
        public WinCore core;
        public Settings Settings;
        public Level Level;

        public bool IsRunning = true;
        public bool GenLvl = true;
        public Win_GameApi()
        {
        }

        public void Exit()
        {
           IsRunning = false;
        }

        public void Pause()
        {
            core.Pause();
        }

        /// <summary>
        /// save game
        /// </summary>
        public void Save(int SaveId)
        {
            if (core != null && core.Level != null)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Level));
                using (var fileStream = new FileStream(Strings.SavePath + $"{core.Level.LevelId}.xml", FileMode.OpenOrCreate)) 
                {
                    serializer.Serialize(fileStream, core.Level);
                    // show message
                }
                MapValues.mapMatrix.SaveMTRX(MapValues.MatrWidth, MapValues.MatrHeight, Strings.SavePath + "matrix" + SaveId + ".txt");
                Console.WriteLine($"Матрица {MapValues.MatrWidth}x{MapValues.MatrHeight} уровня сохранена");
            }
        }

        /// <summary>
        /// save level
        /// </summary>
        /// <param name="levelId"></param>
        public void GenerateLevel(int levelId)
        {
            if (core != null && core.Level != null)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Level));
                using (var fileStream = new FileStream(Strings.LevelsPath + $"{levelId}.xml", FileMode.OpenOrCreate))
                {
                    serializer.Serialize(fileStream, core.Level);
                }
                MapValues.mapMatrix.SaveMTRX(MapValues.MatrWidth, MapValues.MatrHeight, Strings.LevelsPath + "matrix" + levelId + ".txt");
                Console.WriteLine($"Матрица {MapValues.MatrWidth}x{MapValues.MatrHeight} уровня сохранена");
            }
        }

        public void LoadLevel(string fname, LoadTag tag) 
        {
            string fullpath;
            string fullpath_matrix;
            string matrixName = "matrix" + fname.Substring(0, fname.IndexOf('.')) + ".txt";

            if (!fname.Contains(".xml")) 
            {
                return;
            }
            switch (tag) 
            {
                case LoadTag.Level:
                    fullpath = Strings.LevelsPath + fname;
                    fullpath_matrix = Strings.LevelsPath + matrixName;
                    break;
                case LoadTag.Saving:
                    fullpath = Strings.SavePath + fname;
                    fullpath_matrix = Strings.SavePath + matrixName;
                    break;
                default:
                    return;
            }

            if (!(File.Exists(fullpath))) 
            {
                return;
            }
            XmlSerializer serializer = new XmlSerializer(typeof(Level));
            using (var fileStream = new FileStream(fullpath, FileMode.OpenOrCreate))
            {
                Level curLevel = (Level)serializer.Deserialize(fileStream);
                Level = curLevel;
                // show message
            }
            core.OnRealMatrix = true;
            core.MatrixPath = fullpath_matrix;
            GenLvl = false;
            Run($"|Имя игры| Уровень {Level.LevelId}");
        }

        public void Run(string formName = "|Имя игры|")
        {
            if (!IsRunning) return;
            if (Level == null)
            {
                Level = new Level();
                Level.LevelId = 0;
                Level.ObjectsOnScene = new System.Collections.Generic.List<GameObject>();
                Level.WallsOnScene = new System.Collections.Generic.List<Barrier>();
            }
            if (core == null) core = new WinCore();
            core.Level = Level;
            if (Settings == null) Settings = new Settings();
            core.initForm(formName);
            core.Debug = GenLvl;
            core.InitObjects();
            Console.WriteLine($"Матрица уровня {MapValues.MatrWidth}x{MapValues.MatrHeight}");
            core.InitBinds();
            core.ShowForm();
        }
    }
}
