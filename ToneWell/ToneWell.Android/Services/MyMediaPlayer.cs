using Android.Media;
using ToneWell.Services;

namespace ToneWell.Droid.Services
{
    public class MyMediaPlayer : IMyMediaPlayer
    {
        private MediaPlayer player;

        public MyMediaPlayer()
        {
            player = new MediaPlayer(); 
        }

        public void Pause()
        {
            if (player == null)
                return;

            if (player.IsPlaying)
                player.Pause();
        }

        public void Resume()
        {
            throw new System.NotImplementedException();
        }

        public void StartPlayer(string filePath)
        {
            if (player == null)
            {
                player = new MediaPlayer();
            }
            else
            {
                player.Reset();
                player.SetDataSource(filePath);
                player.Prepare();
                player.Start();
            }
        }

        public void Stop()
        {
            throw new System.NotImplementedException();
        }

        public void Release()
        {
            if (player != null)
                player.Release();
        }
    }
}