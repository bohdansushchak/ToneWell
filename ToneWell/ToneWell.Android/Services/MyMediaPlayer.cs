﻿using Android.Media;
using System;
using ToneWell.Services;

namespace ToneWell.Droid.Services
{
    public class MyMediaPlayer : IMyMediaPlayer
    {
        private MediaPlayer player;

        public event EventHandler Completion;

        public bool IsPlaying => player.IsPlaying;

        public int Duration => player.Duration;

        public int CurrentPosition => player.CurrentPosition;

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
            {
                player.Start();
                player.Completion += Completion;
            }

        }

        public void StartPlayer(string filePath)
        {
            if (player == null)
            {
                player = new MediaPlayer();
            }
            else
            {
                if (!string.IsNullOrEmpty(filePath))
                {
                    player.Reset();
                    player.SetDataSource(filePath);
                    player.Prepare();
                }
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