using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.SpeechRecognition;
using Windows.Storage;

namespace Selfie_App
{
    class Utils
    {
        public static async void LoadVoiceCommandDefinition()
        {
            try
            {
                Uri uri = new Uri("ms-appx:///voicecommands.xml", UriKind.Absolute);
                StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(uri);
                await VoiceCommandManager.InstallCommandSetsFromStorageFileAsync(file);
            }
            catch (Exception e)
            {

            }
        }
    }
}
