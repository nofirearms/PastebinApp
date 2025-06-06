﻿using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
using PastebinApp.ViewModel;
using Microsoft.Practices.ServiceLocation;
using PastebinApp.Services;
using PastebinApp.Views;
using Windows.ApplicationModel.DataTransfer;

namespace PastebinApp
{
	sealed partial class App
	{
		public App()
		{
			InitializeComponent();
			Suspending += OnSuspending;
		}


		public static bool ShareMode => (Window.Current.Content as Frame).Name == "ShareFrame";

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
					if (ServiceLocator.Current.GetInstance<ApiService>().IsLoggedIn)
						rootFrame.Navigate(typeof(PastesListPage), e.Arguments);
					else
						rootFrame.Navigate(typeof(LoginPage), e.Arguments);
				}
				// Ensure the current window is active
				Window.Current.Activate();
			}
			DispatcherHelper.Initialize();

			Messenger.Default.Register<NotificationMessageAction<string>>(
				this,
				HandleNotificationMessage);
		}

		private void HandleNotificationMessage(NotificationMessageAction<string> message)
		{
			message.Execute("Success (from App.xaml.cs)!");
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

		protected override void OnShareTargetActivated(ShareTargetActivatedEventArgs args)
		{
			var share_service = ServiceLocator.Current.GetInstance<ShareService>();

			share_service.ReceiveData(args.ShareOperation);

			var rootFrame = new Frame() { Name = "ShareFrame" };

            Window.Current.Content = rootFrame;

            if (ServiceLocator.Current.GetInstance<ApiService>().IsLoggedIn)
				rootFrame.Navigate(typeof(PastePage), null);
			else
				rootFrame.Navigate(typeof(LoginPage), null);

			
			Window.Current.Activate();
		}
	}
}
