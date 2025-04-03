using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using PastebinApp.Services;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PastebinApp.ViewModel
{
	public class LoginViewModel : ViewModelBase
	{
		private readonly ApiService _apiService;
		private readonly IDialogService _dialogService;
		private readonly INavigationService _navigationService;
		private readonly ShareService _shareService;

		private string _username = "";

		public string Username
		{
			get => _username;
			set => Set(ref _username, value);
		}

		private string _password = "";

		public string Password
		{
			get => _password;
			set => Set(ref _password, value);
		}

		public LoginViewModel(ApiService apiService, IDialogService dialogService, INavigationService navigationService, ShareService shareService)
		{
			_apiService = apiService;
			_dialogService = dialogService;
			_navigationService = navigationService;
			_shareService = shareService;

			SignInCommand = new RelayCommand(OnSignInCommandExecuted);
		}

		public RelayCommand SignInCommand { get; }
		private async void OnSignInCommandExecuted()
		{
			var result = await _apiService.LoginAsync(_username, _password);
			if (result.IsSuccessful)
			{
				if (App.ShareMode)
				{
					_navigationService.NavigateTo(ViewModelLocator.PastePageKey);
				}
				else
				{
					_navigationService.NavigateTo(ViewModelLocator.PastesListPageKey);
				}
                Username = "";
                Password = "";
            }
			else
			{
				await _dialogService.ShowMessage(result.Response.ToString(), "Error");
			}


		}
	}
}
