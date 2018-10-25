using System;

namespace ToneWell.Services
{
    public interface IMyMediaPlayer
    {
        void StartPlayer(string filePath);

        void Pause();

        void Resume();

        void Stop();

        void Release();
    }
}
