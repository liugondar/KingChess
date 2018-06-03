namespace ChessKing
{
    public class SoundManager
    {
        WMPLib.WindowsMediaPlayer Music;
        WMPLib.WindowsMediaPlayer MoveSound;
        WMPLib.WindowsMediaPlayer CheckSound;
        WMPLib.WindowsMediaPlayer CheckMateSound;
        WMPLib.WindowsMediaPlayer AnnouncerWelcome;

        public SoundManager()
        {
            Music = new WMPLib.WindowsMediaPlayer();
            MoveSound = new WMPLib.WindowsMediaPlayer();
            CheckSound= new WMPLib.WindowsMediaPlayer();
            CheckMateSound= new WMPLib.WindowsMediaPlayer();
            AnnouncerWelcome= new WMPLib.WindowsMediaPlayer();
        }
        /// <summary>
        /// Change state to mute if music is playing or opposite
        /// </summary>
        public void SwapMusicState()
        {
            Common.IsMusicMuted = !Common.IsMusicMuted;
            if (Common.IsMusicMuted) Music.controls.pause();
            else Music.controls.play();
        }
      

        public void InitMusic()
        {
            if (!Common.IsMusicMuted)
            {
                Music.URL = @"Sounds\Music.wav";
                Music.settings.setMode("Loop", true);
                CheckMateSound.settings.autoStart = false;
                CheckMateSound.URL = @"Sounds\CheckMate.wav";
            }
        }
        public void StopMusic()
        {
            if (Common.IsMusicMuted) Music.controls.stop();
        }
        public void PauseMusic()
        {
            if (Common.IsMusicMuted) Music.controls.pause();
        }
        public void PlayMusic()
        {
            if (!Common.IsMusicMuted) Music.controls.play();
        }

        public void PlayAnnouncerWelcome()
        {
            if (!Common.IsSoundMuted)
                AnnouncerWelcome.URL = @"Sounds\AnnounceWelcome.wav";
        }
        public void PlayCheckMateSound()
        {
            if (!Common.IsSoundMuted)
            {
                CheckMateSound.controls.play();
            }
        }
        public void StopCheckMateSound()
        {
            CheckMateSound.controls.stop();
        }

        public void PlayCheckSound()
        {
            if (!Common.IsSoundMuted)
                CheckSound.URL = @"Sounds\Check.wav";
        }

        public void PlayMoveSound()
        {
            if (!Common.IsSoundMuted)
                MoveSound.URL = @"Sounds\Move.wav";
        }

        public void SwapSoundState()
        {
            Common.IsSoundMuted = !Common.IsSoundMuted;
        }

    }
}
