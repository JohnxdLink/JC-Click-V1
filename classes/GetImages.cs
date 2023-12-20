using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JC_Click_V1.classes
{
    class GetImages
    {
        private String[] imagePath = { "ButtonSelectBlue.png", "ButtonSelectRed.png", "ButtonSelectBrown.png", "ButtonSelectDefault.png"};
        
        public String getBlueButton()
        {
            return "pack://application:,,,/resources/images/" + imagePath[0];
        }

        public String getRedButton()
        {
            return "pack://application:,,,/resources/images/" + imagePath[1];
        }

        public String getBrownButton()
        {
            return "pack://application:,,,/resources/images/" + imagePath[2];
        }

        public String getDefaultButton()
        {
            return "pack://application:,,,/resources/images/" + imagePath[3];
        }

    }
}
