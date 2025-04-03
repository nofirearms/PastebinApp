using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using PastebinApp.Model;
using PastebinApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace PastebinApp.ViewModel
{
	public class PastesListViewModel : ViewModelBase
	{
		private readonly ApiService _apiService;
		private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;

        public ObservableCollection<PastePreview> PastesList { get; set; } = new ObservableCollection<PastePreview>();

		private User _user;
		public User User
		{
			get => _user;
			set => Set(ref _user, value);
		}

		private PastePreview _selectedPaste;
		public PastePreview SelectedPaste
		{
			get => _selectedPaste;
			set
			{
				Set(ref _selectedPaste, value);
				OpenPasteCommand?.RaiseCanExecuteChanged();
			}
		}

		private bool _isPasteOpen = false;
		public bool IsPasteOpen
		{
			get => _isPasteOpen;
			set => Set(ref _isPasteOpen, value);
		}

		private string _pasteText;
		public string  PasteText
		{
			get => _pasteText;
			set => Set(ref _pasteText, value);
		}

		public PastesListViewModel(ApiService apiService, INavigationService navigationService, IDialogService dialogService)
		{
			_apiService = apiService;
			_navigationService = navigationService;
			_dialogService = dialogService;

			CreatePasteCommand = new RelayCommand(OnCreatePasteCommandExecuted);

			OpenPasteCommand = new RelayCommand(OnOpenPasteCommandExecuted, CanOpenPasteCommandExecute);
			ClosePasteCommand = new RelayCommand(OnClosePasteCommandExecuted);
			LogoutCommand = new RelayCommand(OnLogoutCommandExecuted);
			RefreshCommand = new RelayCommand(OnRefreshCommandExecuted);

            LoadData();
		}

		public async void LoadData()
		{
			//Username = _apiService.Username;
			var user_result = await _apiService.GetUserInfoAsync();
			if(user_result.IsSuccessful)
			{
				User = (User)user_result.Response;
			}
			else
			{
                await _dialogService.ShowMessage(user_result.Response.ToString(), "Error");
                return;
			}
			var result = await _apiService.GetPastesAsync();
			if (result.IsSuccessful)
			{
				var list = result.Response as PastePreview[];
				PastesList = new ObservableCollection<PastePreview>(list);
				RaisePropertyChanged("PastesList");
			}
		}

		public RelayCommand CreatePasteCommand { get; }
		private void OnCreatePasteCommandExecuted()
		{
			_navigationService.NavigateTo(ViewModelLocator.PastePageKey);
		}

		public RelayCommand OpenPasteCommand { get; }
		private async void OnOpenPasteCommandExecuted()
		{
			var result = await _apiService.GetPasteTextAsync(_selectedPaste.Key);
			if(result.IsSuccessful)
			{
				var text = result.Response as string;
				PasteText = text;

				IsPasteOpen = true;
			}
		}
		private bool CanOpenPasteCommandExecute() => _selectedPaste != null;

		public RelayCommand ClosePasteCommand { get; }
		private void OnClosePasteCommandExecuted()
		{
			IsPasteOpen = false;
		}


        public RelayCommand RefreshCommand { get; }
        private void OnRefreshCommandExecuted()
        {
			LoadData();
        }

        public RelayCommand LogoutCommand { get; }
		private void OnLogoutCommandExecuted()
		{
			_apiService.Logout();
			_navigationService.NavigateTo(ViewModelLocator.LoginPageKey);
		}

    }


}
