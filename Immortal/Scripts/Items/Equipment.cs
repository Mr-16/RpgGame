using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.Items
{
    public class Equipment : ItemData
    {
        public Equipment(int id, string name, string description, ItemType type, int maxStack, string iconPath) : base(id, name, description, type, maxStack, iconPath)
        {
        }
    }
}
