using System;
using System.ComponentModel;
using System.Windows.Input;
using AudioPlayer.Services;
using Xamarin.Forms;

namespace AudioPlayer.ViewModels
{
	public class AudioPlayerViewModel: INotifyPropertyChanged
	{
		private IAudioPlayerService _audioPlayer;
		private bool _isStopped;
		private int _indexOfButton;
		public event PropertyChangedEventHandler PropertyChanged;

		public AudioPlayerViewModel(IAudioPlayerService audioPlayer)
		{
			_audioPlayer = audioPlayer;
			_audioPlayer.OnFinishedPlaying = () => {
				_isStopped = true;
				CommandText = "Play";
				_indexOfButton = 0;
			};
			CommandText = "Play";
			_isStopped = true;
			_indexOfButton = 0;
		}

		private string _commandText;
		public string CommandText
		{
			get { return _commandText;}
			set
			{
				_commandText = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CommandText"));
			}
		}

		private ICommand _playPauseCommand;
		public ICommand PlayPauseCommand
		{
			get
			{
				return _playPauseCommand ?? (_playPauseCommand = new Command(
					(obj) => 
				{
					if (CommandText == "Play")
					{
						if (_isStopped)
						{
							_isStopped = false;
							_audioPlayer.Play("Galway.mp3");
						}
						else
						{

							_audioPlayer.Play();
						}
						CommandText = "Pause";
					}
					else
					{
						_audioPlayer.Pause();
						CommandText = "Play";
					}
				}));
			}
		}
	}
}
