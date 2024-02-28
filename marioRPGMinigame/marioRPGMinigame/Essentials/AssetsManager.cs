using Microsoft.VisualBasic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection.Metadata;

namespace marioRPGMinigame.Essentials
{
    public class AssetsManager
    {

        public Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
        public Dictionary<string, SoundEffect> sounds = new Dictionary<string, SoundEffect>();
        public Dictionary<string, SpriteFont> fonts = new Dictionary<string, SpriteFont>();

        public AssetsManager(ContentManager Content)
        {

            LoadFromDir(textures, Content, "textures");
            LoadFromDir(sounds, Content, "sounds");
            LoadFromDir(fonts, Content, "spritefonts");




        }

        private void LoadFromDir<T>(Dictionary<string,T> dict, ContentManager Content, string subdirectory)
        {
            DirectoryInfo dir = new DirectoryInfo(Content.RootDirectory + "\\" + subdirectory);
            if (!dir.Exists)
                throw new DirectoryNotFoundException();
            FileInfo[] files = dir.GetFiles("*.*");

            foreach (FileInfo file in files)
            {
                string assetName = Path.GetFileNameWithoutExtension(file.Name);
                dict[assetName] = Content.Load<T>(subdirectory + "/" + assetName);
            }
        }


    }
}
