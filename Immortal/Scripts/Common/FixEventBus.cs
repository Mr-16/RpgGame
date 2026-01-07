using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.Common
{
    public class FixEventBus
    {
        private static FixEventBus instance = new FixEventBus();
        private FixEventBus() { }
        public static FixEventBus Instance() => instance;

        //穿戴了某装备
        //public event Action<EquipInstance> EquipmentEquipped;
        //public void PublishEquipmentEquipped(EquipInstance equipInst) => EquipmentEquipped?.Invoke(equipInst);
    
        
    }
}
