using Newtonsoft.Json;
using SmartNoteService.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace TestApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void btnSend_Click(object sender, RoutedEventArgs e)
        {
            TestMethodRequest req = new TestMethodRequest();
            int t;
            Int32.TryParse(textBox.Text, out t);
            req.Count = t;


            TestMethodResponse resp = await SendMessage<TestMethodRequest, TestMethodResponse>(req, "http://localhost/SmartNoteService/SmartNoteService.svc/TestMethod/");

            string res = "";
            foreach (var item in resp.List)
            {
                res += item.ToString() + " ";
            }
            tbRes.Text = res;
        }

        public async Task<K> SendMessage<T, K>(T request, string uriString)
        {
            using (var client = new Windows.Web.Http.HttpClient())
            {
                TimeSpan ts = new TimeSpan(1, 0, 0);
                var uri = new Uri(uriString);
                Windows.Web.Http.HttpStringContent input = new Windows.Web.Http.HttpStringContent(JsonConvert.SerializeObject(request), Windows.Storage.Streams.UnicodeEncoding.Utf8 /*System.Text.Encoding.UTF8*/, "application/json");

                var cts = new System.Threading.CancellationTokenSource();
                cts.CancelAfter(ts);
                var res = await client.PostAsync(uri, input);
                res.EnsureSuccessStatusCode();

                string responseBody = await res.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<K>(responseBody);
            }

        }
    }
}
