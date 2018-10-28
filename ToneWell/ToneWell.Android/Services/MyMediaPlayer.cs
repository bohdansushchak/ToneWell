using System;
using Android.Media;
using ToneWell.Services;

namespace ToneWell.Droid.Services
{
    public class MyMediaPlayer : IMyMediaPlayer
    {
        private MediaPlayer player;

        public event EventHandler Completion;

        public bool IsPlaying => player.IsPlaying;

        public MyMediaPlayer()
        {
            player = new MediaPlayer();
            player.Completion += Completion;
        }

        public void Pause()
        {
            if (player.IsPlaying)
                player.Pause();
        }

        public void Resume()
        {
            if (!player.IsPlaying)
                player.Start();
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
                player.Completion += Completion;

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