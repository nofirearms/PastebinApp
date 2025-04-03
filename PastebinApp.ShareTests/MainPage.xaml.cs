using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Data.Html;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace PastebinApp.ShareTests
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

		private string htmlText = "Version:1.0 StartHTML:00000097 EndHTML:00000541 StartFragment:00000153 EndFragment:00000508 <!DOCTYPE><HTML><HEAD></HEAD><BODY><!--StartFragment --><div style = \"padding-left: 10px; margin-bottom: 10px; border-left: 5px solid;border-color: rgb(255,137,119);\"><p style=\"margin:0;\">It’s very small and weak, and it will never amount to anything</p></div><div style = \"padding-left: 10px; margin-bottom: 10px; border-left: 5px solid;border-color: rgb(255,137,119);\"><p style=\"margin:0;\">Control</p></div><!--EndFragment --></BODY></HTML>";

        public MainPage()
        {
            this.InitializeComponent();
        }

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			//DataTransferManager.ShowShareUI();
			var text = HtmlUtilities.ConvertToText(htmlText);
		}

	}
}
