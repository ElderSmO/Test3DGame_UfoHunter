using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.GameControl
{
    /// <summary>
    /// Класс модификация игры
    /// </summary>
    public class Mode
    {
        public int counLowLvltUfo {  get; set; }
        public int countMiddleLvlUfo { get; set; }
        public int countHardLvlUfo { get; set; }
        public int gameSeconds { get; set; }
        public int countUfoForLose { get; set; }

    }
}
