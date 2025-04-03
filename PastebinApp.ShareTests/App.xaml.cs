using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.DataTransfer;
using Windows.ApplicationModel.DataTransfer.ShareTarget;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ApplicationSettings;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace PastebinApp.ShareTests
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;

			this.LeavingBackground += App_LeavingBackground;
			this.Resuming += App_Resuming;
		}

		private void App_Resuming(object sender, object e)
		{
			
		}

		private void App_LeavingBackground(object sender, LeavingBackgroundEventArgs e)
		{

		}

		private void ShareSourceLoad()
		{
			DataTransferManager dataTransferManager = DataTransferManager.GetForCurrentView();
			dataTransferManager.DataRequested += new TypedEventHandler<DataTransferManager, DataRequestedEventArgs>(this.DataRequested);
		}

		private void DataRequested(DataTransferManager sender, DataRequestedEventArgs e)
		{
			DataRequest request = e.Request;
			request.Data.Properties.Title = "Share Text Example";
			request.Data.Properties.Description = "An example of how to share text.";
			request.Data.SetText("Hello World!");
		}

		protected override void OnWindowCreated(WindowCreatedEventArgs args)
		{
			base.OnWindowCreated(args);
		}
		/// <summary>
		/// Invoked when the application is launched normally by the end user.  Other entry points
		/// will be used such as when the application is launched to open a specific file.
		/// </summary>
		/// <param name="e">Details about the launch request and process.</param>
		protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // When the navigation stack isn't restored navigate to the first page,
                    // configuring the new page by passing required information as a navigation
                    // parameter
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }
                // Ensure the current window is active
                Window.Current.Activate();

				DataTransferManager dataTransferManager = DataTransferManager.GetForCurrentView();
				dataTransferManager.DataRequested += DataTransferManager_DataRequested;
				
				dataTransferManager.TargetApplicationChosen += DataTransferManager_TargetApplicationChosen;
				ShareSourceLoad();
				dataTransferManager.ShareProvidersRequested += DataTransferManager_ShareProvidersRequested;
				//DataTransferManager.ShowShareUI();

			}
        }

		private void DataTransferManager_ShareProvidersRequested(DataTransferManager sender, ShareProvidersRequestedEventArgs args)
		{
			
		}

		private void DataTransferManager_TargetApplicationChosen(DataTransferManager sender, TargetApplicationChosenEventArgs args)
		{
			
		}

		private void DataTransferManager_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
		{
			DataRequest request = args.Request;
			request.Data.Properties.Title = "Share Text Example";
			request.Data.Properties.Description = "An example of how to share text.";
			request.Data.SetText("Hello World!");
			request.Data.Properties.FileTypes.Add("Text");
		}

		protected override async void OnShareTargetActivated(ShareTargetActivatedEventArgs args)
		{
			ShareOperation shareOperation = args.ShareOperation;

			shareOperation.ReportStarted();
			shareOperation.ReportSubmittedBackgroundTask();
			string output = string.Empty;
			if (shareOperation.Data.Contains(StandardDataFormats.Text))
			{
				string text = await shareOperation.Data.GetTextAsync();

				// To output the text from this example, you need a TextBlock control
				// with a name of "sharedContent".
				//await new Windows.UI.Popups.MessageDialog(text).ShowAsync();


				output = "text " + text;
			}else if (shareOperation.Data.Contains(StandardDataFormats.Html)){

				string text = await shareOperation.Data.GetHtmlFormatAsync();

				output = "html " + text;
			}
			else if (shareOperation.Data.Contains(StandardDataFormats.Rtf)){

				string text = await shareOperation.Data.GetRtfAsync();

				output = "rtf " + text;
			}
			else
			{
				output = "error";
			}
			var page = new SharePage();
			page.SharedText = output;
			Window.Current.Content = page;
			Window.Current.Activate();
			//shareOperation.ReportCompleted();
		}

		protected override void OnBackgroundActivated(BackgroundActivatedEventArgs args)
		{
			base.OnBackgroundActivated(args);
		}

		async void ReportCompleted(ShareOperation shareOperation, string quickLinkId, string quickLinkTitle)
		{
			QuickLink quickLinkInfo = new QuickLink
			{
				Id = quickLinkId,
				Title = quickLinkTitle,

				// For quicklinks, the supported FileTypes and DataFormats are set 
				// independently from the manifest
				SupportedFileTypes = { "*" },
				SupportedDataFormats = { StandardDataFormats.Text, StandardDataFormats.Uri,
				StandardDataFormats.Bitmap, StandardDataFormats.StorageItems }
			};

			//StorageFile iconFile = await Windows.ApplicationModel.Package.Current.InstalledLocation.CreateFileAsync(
			//		"assets\\user.png", CreationCollisionOption.OpenIfExists);
			//quickLinkInfo.Thumbnail = RandomAccessStreamReference.CreateFromFile(iconFile);
			//shareOperation.ReportCompleted(quickLinkInfo);
		}
		/// <summary>
		/// Invoked when Navigation to a certain page fails
		/// </summary>
		/// <param name="sender">The Frame which failed navigation</param>
		/// <param name="e">Details about the navigation failure</param>
		void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }

		protected override void OnActivated(IActivatedEventArgs args)
		{
			base.OnActivated(args);
		}
	}
}
