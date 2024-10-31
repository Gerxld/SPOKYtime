using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace SpookyTime
{
    public class ReproductorMusica
    {
        private IWavePlayer waveOutDevice;
        private AudioFileReader audioFileReader;

        public void Play(string filePath)
        {
            waveOutDevice = new WaveOut();
            audioFileReader = new AudioFileReader(filePath);
            waveOutDevice.Init(audioFileReader);
            waveOutDevice.Play();
            waveOutDevice.PlaybackStopped += OnPlaybackStopped;
        }

        public void Stop()
        {
            waveOutDevice?.Stop();
            Dispose();
        }

        private void OnPlaybackStopped(object sender, EventArgs e)
        {
            // Reproducir de nuevo la música cuando termine
            audioFileReader.Position = 0; // Reinicia el audio
            waveOutDevice.Play(); // Reproduce nuevamente
        }

        public void Dispose()
        {
            waveOutDevice?.Dispose();
            audioFileReader?.Dispose();
        }
    }
}
