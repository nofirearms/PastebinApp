using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using PastebinApp.Model.enums;
using PastebinApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastebinApp.ViewModel
{
	public class PasteViewModel : ViewModelBase
	{
		private readonly ApiService _apiService;
		private readonly INavigationService _navigationService;
		private readonly IDialogService _dialogService;
		private readonly ShareService _shareService;

		private string _title;
		public string Title
		{
			get => _title;
			set => Set(ref _title, value);
		}

		private string _text;
		public string Text
		{
			get => _text;
			set
			{
				Set(ref _text, value);
				CreatePasteCommand?.RaiseCanExecuteChanged();
			}
		}

		public IEnumerable<Privacy> PrivacyList => Enum.GetValues(typeof(Privacy)).Cast<Privacy>();

		private Privacy _selectedPrivacy = Privacy.Public;
		public Privacy SelectedPrivacy
		{
			get => _selectedPrivacy;
			set => Set(ref _selectedPrivacy, value);
		}


		private string _folder = "";
		public string Folder
		{
			get => _folder;
			set => Set(ref _folder, value);
		}

		public PasteViewModel(ApiService apiService, INavigationService navigationService, IDialogService dialogService, ShareService shareService)
		{
			_apiService = apiService;
			_navigationService = navigationService;
			_dialogService = dialogService;
			_shareService = shareService;

			CreatePasteCommand = new RelayCommand(OnCreatePasteCommandExecuted, CanCreatePasteCommandExecute);
			CancelCommand = new RelayCommand(OnCancelCommandExecuted);

			LoadData();
		}

		private void LoadData()
		{
			if (App.ShareMode)
			{
				Text = _shareService.PasteText;
				Title = _shareService.PasteTitle;
			}
		}

		public RelayCommand CreatePasteCommand { get; }
		private async void OnCreatePasteCommandExecuted()
		{
			var result = await _apiService.CreatePasteAsync(_title, _text, _folder, _selectedPrivacy);
			if (result.IsSuccessful)
			{
				if (App.ShareMode)
				{
					_shareService.Complete();
				}
				else
				{
					_navigationService.NavigateTo(ViewModelLocator.PastesListPageKey);
				}
			}
			else
			{
				await _dialogService.ShowMessage(result.Response.ToString(), "Error");
			}
		}

		private bool CanCreatePasteCommandExecute() => !string.IsNullOrEmpty(_text);

		public RelayCommand CancelCommand { get; }
		private void OnCancelCommandExecuted()
		{
			if (App.ShareMode)
			{
				_shareService.Complete();
			}
			else
			{
				_navigationService.NavigateTo(ViewModelLocator.PastesListPageKey);
			}
			

		}
	}
}
