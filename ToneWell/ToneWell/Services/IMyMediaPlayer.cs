using System;

namespace ToneWell.Services
{
    public interface IMyMediaPlayer
    {
        bool IsPlaying { get; }

        void StartPlayer(string filePath);

        void Pause();

        void Resume();

        void Stop();

        void Release();

        event EventHandler Completion;

    }
}
