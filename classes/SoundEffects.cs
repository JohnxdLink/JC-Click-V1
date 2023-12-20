using System;
using System.IO;
using System.Media;
using System.Threading.Tasks;

namespace JC_Click_V1.classes
{
    class SoundEffects
    {
        SoundPlayer soundPlayer = new SoundPlayer();

        DateTime lastKeyPressTime = DateTime.MinValue;

        public async void PlaySoundEffect(string fileName)
        {
            try
            {
                DateTime currentTime = DateTime.Now;
                TimeSpan elapsedSinceLastKeyPress = currentTime - lastKeyPressTime;

                // N: Adjust the threshold based on your preference
                TimeSpan threshold = TimeSpan.FromMilliseconds(100);

                if (elapsedSinceLastKeyPress >= threshold)
                {
                    // N: Get the directory where the application is running
                    string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

                    // N: Navigate up to the project root from the "classes" folder
                    string projectRoot = Path.GetFullPath(Path.Combine(baseDirectory, "..", ".."));

                    // N: Construct the relative path to the audio file
                    string audioFilePath = Path.Combine(projectRoot, "resources", "audio", fileName);

                    // N: Use async/await to play the sound asynchronously
                    await Task.Run(() =>
                    {
                        try
                        {
                            soundPlayer.SoundLocation = audioFilePath;
                            soundPlayer.Load(); // N: Ensure the sound is loaded before playing
                            soundPlayer.Play();
                        }
                        catch (Exception ex)
                        {
                            // N: Log the error to a text file in the "resources/errorlogs" folder
                            LogError(ex, projectRoot);
                        }
                    });

                    lastKeyPressTime = currentTime;
                }
            }
            catch (Exception ex)
            {
                // N: Log the error to a text file in the "resources/errorlogs" folder
                LogError(ex, AppDomain.CurrentDomain.BaseDirectory);
            }
        }

        private void LogError(Exception ex, string projectRoot)
        {
            try
            {
                // N: Create the "errorlogs" folder if it doesn't exist
                string errorLogsFolder = Path.Combine(projectRoot, "resources", "errorlogs");
                Directory.CreateDirectory(errorLogsFolder);

                // N: Create a unique filename based on the current timestamp and the hash code of the exception
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string hashCode = ex.GetHashCode().ToString("X");
                string errorLogFileName = $"errorlog_{timestamp}_{hashCode}.txt";

                // N: Construct the full path to the error log file
                string errorLogFilePath = Path.Combine(errorLogsFolder, errorLogFileName);

                // N: Write the error details to the error log file
                File.WriteAllText(errorLogFilePath, $"Timestamp: {timestamp}\nError: {ex.ToString()}");
            }
            catch (Exception logEx)
            {
                // N: If an error occurs while trying to log, output to the console
                Console.WriteLine($"Error logging to error log file: {logEx.Message}");
            }
        }
    }
}
