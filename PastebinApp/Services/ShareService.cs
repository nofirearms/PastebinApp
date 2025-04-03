using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.ApplicationModel.DataTransfer.ShareTarget;
using Windows.Data.Html;

namespace PastebinApp.Services
{
	public class ShareService
	{
		private ShareOperation _shareOperation;

		public string PasteText { get; set; }

		public string PasteTitle { get; set; }
		
		public async void ReceiveData(ShareOperation shareOperation)
		{
			_shareOperation = shareOperation;

			_shareOperation.ReportStarted();

			string output = string.Empty;

			if (_shareOperation.Data.Contains(StandardDataFormats.Text))
			{
				output = await _shareOperation.Data.GetTextAsync();
			}
			else if (_shareOperation.Data.Contains(StandardDataFormats.Html))
			{
				var html = await _shareOperation.Data.GetHtmlFormatAsync();
				output = HtmlUtilities.ConvertToText(html);
			}

			PasteText = output;
			PasteTitle = $"{_shareOperation.Data.Properties.Title} - {_shareOperation.Data.Properties.ApplicationName}";
		}

		public async void Complete()
		{
			if (_shareOperation is null) return;

			_shareOperation.ReportCompleted();
		}
	}
}
//		ShareOperation shareOperation = args.ShareOperation;

//		shareOperation.ReportStarted();
//			shareOperation.ReportSubmittedBackgroundTask();
//			string output = string.Empty;
//			if (shareOperation.Data.Contains(StandardDataFormats.Text))
//			{
//				string text = await shareOperation.Data.GetTextAsync();


//		output = "text " + text;
//			}else if (shareOperation.Data.Contains(StandardDataFormats.Html)){

//				string text = await shareOperation.Data.GetHtmlFormatAsync();

//	output = "html " + text;
//			}
//			else if (shareOperation.Data.Contains(StandardDataFormats.Rtf)){

//				string text = await shareOperation.Data.GetRtfAsync();

//output = "rtf " + text;
//			}
//			else
//			{
//				output = "error";
//			}
//			var page = new SharePage();
//page.SharedText = output;
//			Window.Current.Content = page;
//			Window.Current.Activate();
//			//shareOperation.ReportCompleted();