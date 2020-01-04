using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameLib;
using GameLib.Models;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.DXGI;
using GameLib.Enums;

namespace Game
{
    public partial class wform : Form
    {
        LoadTag tag_load;
        GameLib.Win_GameApi launcher;
        My2dGame my2D;
        Settings settings;

        public wform()
        {
            InitializeComponent();
        }

        private void wform_Load(object sender, EventArgs e)
        {
            SaveBtn.Enabled = false;
            SetLevel.Enabled = false;
            LevelID_2.Enabled = false;
            LevelID.Enabled = false;

            launcher = new GameLib.Win_GameApi();
            my2D = new My2dGame();
            settings = new Settings();
            
            launcher.core = my2D;
            launcher.Settings = settings;

        }

        private void RunBtn_Click(object sender, EventArgs e)
        {
            LevelID.Enabled = true;
            SetLevel.Enabled = true;
            launcher.Run();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            SaveBtn.Enabled = false;
            LevelID_2.Enabled = false;
            launcher.Save((int)LevelID_2.Value);
        }

        private void LoadBtn_Click(object sender, EventArgs e)
        {
            LevelsBox.Items.Clear();
            DirectoryInfo dir = new DirectoryInfo(Strings.LevelsPath);
            FileInfo[] files = dir.GetFiles();
            MessageBox.Show($"Доступно {files.Length} файл(ов/а)", "Выбор уровня");
            LevelsBox.Items.AddRange(files);
            tag_load = LoadTag.Level;
        }

        private void LevelsBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SaveBtn.Enabled = true;
            LevelID_2.Enabled = true;
            FileInfo file = (FileInfo)LevelsBox.SelectedItem;
            launcher.LoadLevel(file.Name, tag_load);
        }

        private void ShowSavedGame_btn_Click(object sender, EventArgs e)
        {
            LevelsBox.Items.Clear();
            DirectoryInfo dir = new DirectoryInfo(Strings.SavePath);
            FileInfo[] files = dir.GetFiles();
            MessageBox.Show($"Доступно {files.Length} файл(ов/а)", "Выбор сохраненной игры");
            LevelsBox.Items.AddRange(files);
            tag_load = LoadTag.Saving;
        }

        private void SetLevel_Click(object sender, EventArgs e)
        {
            LevelID.Enabled = false;
            SetLevel.Enabled = false;
            launcher.GenerateLevel((int)LevelID.Value);
            this.Close();
        }
    }
}
