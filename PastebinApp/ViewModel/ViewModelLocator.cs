using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using PastebinApp.Model;
using PastebinApp.Services;
using PastebinApp.Views;

namespace PastebinApp.ViewModel
{
	/// <summary>
	/// This class contains static references to all the view models in the
	/// application and provides an entry point for the bindings.
	/// <para>
	/// See http://www.mvvmlight.net
	/// </para>
	/// </summary>
	public class ViewModelLocator
	{
		public const string LoginPageKey = "LoginPage";
		public const string PastesListPageKey = "PastesListPage";
		public const string PastePageKey = "PastePage";

		/// <summary>
		/// This property can be used to force the application to run with design time data.
		/// </summary>
		public static bool UseDesignTimeData
		{
			get
			{
				return false;
			}
		}

		static ViewModelLocator()
		{
			ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

			var nav = new NavigationService(); 
			nav.Configure(LoginPageKey, typeof(LoginPage));
			nav.Configure(PastesListPageKey, typeof(PastesListPage));
			nav.Configure(PastePageKey, typeof(PastePage));

			SimpleIoc.Default.Register<INavigationService>(() => nav);
			SimpleIoc.Default.Register<IDialogService, DialogService>();
			SimpleIoc.Default.Register<ApiService>();
			SimpleIoc.Default.Register<ShareService>();

			if (ViewModelBase.IsInDesignModeStatic
					|| UseDesignTimeData)
			{
				SimpleIoc.Default.Register<IDataService, Design.DesignDataService>();
			}
			else
			{
				SimpleIoc.Default.Register<IDataService, DataService>();
			}

			SimpleIoc.Default.Register<LoginViewModel>();
			SimpleIoc.Default.Register<PastesListViewModel>();
			SimpleIoc.Default.Register<PasteViewModel>();
		}

		/// <summary>
		/// Gets the Main property.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
			"CA1822:MarkMembersAsStatic",
			Justification = "This non-static member is needed for data binding purposes.")]
		public LoginViewModel Login => ServiceLocator.Current.GetInstance<LoginViewModel>();
		//public PastesListViewModel PastesList => ServiceLocator.Current.GetInstance<PastesListViewModel>();
		//public PasteViewModel Paste => ServiceLocator.Current.GetInstance<PasteViewModel>();
		public PastesListViewModel PastesList => SimpleIoc.Default.GetInstanceWithoutCaching<PastesListViewModel>();
		public PasteViewModel Paste => SimpleIoc.Default.GetInstanceWithoutCaching<PasteViewModel>();
	}
}
