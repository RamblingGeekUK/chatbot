using DSharpPlus.EventArgs;
using Serilog;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using Vector;
using Encoder = System.Drawing.Imaging.Encoder;

namespace ChatBot.Base
{
    public class CommandAnnounce : CommandBase
    {
        public CommandAnnounce(TwitchClient client)
            : base(client)
        {
        }

        public async void Execute (string message, OnRaidNotificationArgs e)
        {
            //this.MessageChat(e.Channel, message);
            await this.Vector(message);
        }
        public async void Execute(string message, OnWhisperReceivedArgs e)
        {
            //this.MessageChat(e.WhisperMessage.Message, message);
            await this.Vector(message);
        }
        public async void Execute(string message, OnMessageReceivedArgs e)
        {
            //this.MessageChat(e.ChatMessage.Channel, message);
          
            await this.Vector(message);
        }

        public async void Execute(string message)
        {
            //this.MessageChat(e.ChatMessage.Channel, message);

            await this.Vector(message);
        }

        public async void Execute(string message, OnChatCommandReceivedArgs e)
        {
            //this.MessageChat(e.Command.ChatMessage.BotUsername, message);
            await this.Vector(message);
        }

        public async void Execute(string message, OnJoinedChannelArgs e)
        {
                //this.MessageChate.Channel, message)
               
                await this.Vector(message);
        }

        internal void Execute(string v, MessageCreateEventArgs e)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Vector(string message)
        {
            try
            {
                Robot robot = new Robot();
                await robot.GrantApiAccessAsync(Settings.Vector_Name, Settings.Vector_IP, Settings.Vector_Serial, Settings.Vector_Username, Settings.Vector_Password);
                await robot.ConnectAsync(Settings.Vector_Name);

                BatteryState x = await robot.GetBatteryStateAsync();

                //await robot.World.AddWallMarkerAsync("My Marker", ObjectMarker.Circles2, true, 30, 30, 30, 30); //register a 30mm custom marker
                //robot.EventListeningAsync().ThrowFeedException(); //start listening for recognized markers

                //robot.World.OnObjectObserved += (sender, e) =>
                //{
                //    if (e.Object.Name == "My Marker")
                //    {
                //        Log.Information($"Found marker", "fail");
                //    }
                //};

                
                //robot.Camera.OnImageReceived += (sender, e) =>
                //{
                //    Image(e.Image);
                //};
                //await robot.Camera.CameraFeedAsync();

                //robot.Camera.StopCameraFeed();

                //await robot.Screen.SetScreenImage(@"");

                //gain control over the robot by suppressing its personality
                robot.StartSuppressingPersonality();
                await robot.WaitTillPersonalitySuppressedAsync();

                //say something
               
                await robot.Audio.SetMasterVolumeAsync(5);
                await robot.Audio.SayTextAsync(message);

                //await robot.Animation.PlayAsync("anim_vc_laser_lookdown_01");
                
                robot.StopSuppressingPersonality();
                await robot.Audio.SetMasterVolumeAsync(1);
                await robot.DisconnectAsync();
                
                Log.Logger.Information("Vector should of spoke the the text!");
                return true;
            }
            catch (Exception e)
            {
                Log.Information($"Connecting to Vector failed! {e.Message}", "fail");

                return false;
            }
        }

        private void Image(Image image)
        { 
            Bitmap myBitmap;
            ImageCodecInfo myImageCodecInfo;
            Encoder myEncoder;
            EncoderParameter myEncoderParameter;
            EncoderParameters myEncoderParameters;

            // Create a Bitmap object based on a BMP file.
            myBitmap = new Bitmap(image);

            // Get an ImageCodecInfo object that represents the JPEG codec.
            myImageCodecInfo = GetEncoderInfo("image/jpeg");

            // Create an Encoder object based on the GUID

            // for the Quality parameter category.
            myEncoder = Encoder.Quality;
                     
            // Create an EncoderParameters object.            
            // An EncoderParameters object has an array of EncoderParameter
            // objects. In this case, there is only one                     
            // EncoderParameter object in the array.
            myEncoderParameters = new EncoderParameters(1);

            // Save the bitmap as a JPEG file with quality level 75.
            myEncoderParameter = new EncoderParameter(myEncoder, 75L);
            myEncoderParameters.Param[0] = myEncoderParameter;
            try
            {
                myBitmap.Save("image.jpg", myImageCodecInfo, myEncoderParameters);
            }
            catch (Exception e)
            {
                Log.Information($"Failed to create image {e.Message}", "fail");
            }
            finally
            {
                Log.Information($"Image created. ({Directory.GetCurrentDirectory()}\\image.jpg)", "Sucess");
            }

        }
    private static ImageCodecInfo GetEncoderInfo(String mimeType)
    {
        int j;
        ImageCodecInfo[] encoders;
        encoders = ImageCodecInfo.GetImageEncoders();
        for (j = 0; j < encoders.Length; ++j)
        {
            if (encoders[j].MimeType == mimeType)
                return encoders[j];
        }
        return null;
    }

}
}
