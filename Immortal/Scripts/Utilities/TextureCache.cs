using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.Utilities
{
    public class TextureCache
    {
        private Dictionary<string, Texture2D> pathTextureMap = new Dictionary<string, Texture2D>();
        private static TextureCache instance = new TextureCache();
        public static TextureCache Instance() => instance;
        private TextureCache() 
        {
            pathTextureMap.Add(GameConstants.CamouflageGreenHeadPath, GD.Load<Texture2D>(GameConstants.CamouflageGreenHeadPath));
            pathTextureMap.Add(GameConstants.CamouflageRedHeadPath, GD.Load<Texture2D>(GameConstants.CamouflageRedHeadPath));
            pathTextureMap.Add(GameConstants.Cavegirl2HeadPath, GD.Load<Texture2D>(GameConstants.Cavegirl2HeadPath));
        }
        public Texture2D GetTexture(string path)
        {
            return pathTextureMap[path];
        }
    }
}
