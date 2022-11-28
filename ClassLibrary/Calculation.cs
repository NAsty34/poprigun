using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WSUniversalLib
{
    public class Calculation
    {
        double[] kef_steck = new double[] { 1.1, 2.5, 8.43 };
        double[] mat_steck = new double[] { 1.003, 1.0012 };
        public int GetQuantityForProduct(int productType, int materialType, int count, float width, float height)
        {
            if (productType <= 0 || productType > 3 || materialType <= 0 || materialType > 2 || count<=0 || width<=0 || height<=0)
            {
                return -1;
            }

            double kef = kef_steck[productType - 1];
            double mat = mat_steck[materialType - 1];

            return (int)(height * width * kef * count * mat);

        }


    }
}
