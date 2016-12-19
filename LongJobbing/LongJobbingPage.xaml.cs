using System.Text.RegularExpressions;
using Xamarin.Forms;
using System.Net.Http;
using System;

namespace LongJobbing
{
	public partial class LongJobbingPage : ContentPage
	{
		public LongJobbingPage()
		{
			InitializeComponent();
		}

		void SendSignal(object sender, System.EventArgs e)
		{
			var userID = int.Parse(txtUserID.Text);
			var jobID = int.Parse(txtJobID.Text);

			var btn = (Button)(sender);
			var time = Regex.Match(btn.Text, @"\d+").Value;
			//DisplayAlert("Confirmation Message", "Sending a communication back to the office...", "OK");
			PostResponse(userID, jobID, int.Parse(time));
		}

		async void PostResponse(int userID, int jobID, int time)
		{
			var query = string.Format("userID={0}&jobID={1}&time={2}", userID, jobID, time);
			var uri = new Uri(string.Format("http://localhost:9004/notifyDispatch?" + query, string.Empty));

			var client = new HttpClient();

			var response = await client.PostAsync(uri, new StringContent(string.Empty));

			var message = response.Content.ReadAsStringAsync().Result;

			await DisplayAlert("Testing", message, "OK");
		}
	}
}
