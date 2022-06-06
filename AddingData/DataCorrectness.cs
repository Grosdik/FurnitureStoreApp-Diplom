using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddingData
{
    public class DataCorrectness
    {
        public static bool Correctness (string Name, int Cost, int Quantity, int TypeOfFurniture, byte[] Image)
        {
            if (Name != null && Cost > 0 && Quantity >= 0 && TypeOfFurniture > 0 && Image != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}


