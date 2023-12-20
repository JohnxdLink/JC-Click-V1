using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace JC_Click_V1.classes
{
    class SoundEffects
    {
        SoundPlayer soundPlayer = new SoundPlayer();

        DateTime lastKeyPressTime = DateTime.MinValue;
        object lockObject = new object();

        public async void PlaySoundEffect(string fileName)
        {
            try
            {
                DateTime currentTime = DateTime.Now;
                TimeSpan elapsedSinceLastKeyPress = currentTime - lastKeyPressTime;

                // Adjust the threshold based on your preference
                TimeSpan threshold = TimeSpan.FromMilliseconds(100);

                if (elapsedSinceLastKeyPress >= threshold)
                {
                    string audioFilePath = $"C:\\Users\\John Christian\\Documents\\Visual Studio\\C# Development\\JC-Click-V1\\resources\\audio\\{fileName}";

                    // Use async/await to play the sound asynchronously
                    await Task.Run(() =>
                    {
                        try
                        {
                            soundPlayer.SoundLocation = audioFilePath;
                            soundPlayer.Load(); // Ensure the sound is loaded before playing
                            soundPlayer.Play();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error playing sound effect: {ex.Message}");
                        }
                    });

                    lastKeyPressTime = currentTime;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error playing sound effect: {ex.Message}");
            }
        }
    }
}
