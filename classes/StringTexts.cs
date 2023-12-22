using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JC_Click_V1.classes
{
    class StringTexts
    {
        private String defaultDscrpt = "Experience typing like never before with JC.Click! Dive into a world of curated sound effects tailored to blue, red, and brown switch keyboards, and refine your keystrokes for heightened productivity and concentration.";
        private String blueDscrpt = "Upgrade your typing experience with the blue switch keyboard! Hear the satisfying click, feel the tactile response, and boost your productivity. Try it for a typing sensation like no other!";
        private String redDscrpt = "Dominate your game with red switches – a smooth, linear design for precise gaming actions. Enjoy a quiet yet powerful typing experience, perfect for focused sessions and a competitive edge. Upgrade to red switches and elevate your gaming prowess!";
        private String brownDscrpt = "Unlock versatility with brown switches! Perfect for typing and gaming, they provide a tactile bump for feedback without the audible click, offering a satisfying and quiet experience. Elevate your typing and gaming with the best of both worlds – choose brown switches for a balanced and enjoyable keyboard experience.";

        public String getdefaultScrpt()
        {
            return defaultDscrpt;
        }

        public String getBlueDscrpt()
        {
            return blueDscrpt;
        }

        public String getRedDscrpt()
        {
            return redDscrpt;
        }

        public String getBrownDscrpt()
        {
            return brownDscrpt;
        }

    }
}
