using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Quick.Build;

public static class QbNet
{
    public struct TransferProgress
    {
        public long Current { get; set; }
        public long Total { get; set; }
        public long Speed { get; set; }
        public TimeSpan RemainingTime { get; set; }
    }

    public static HttpClient GetHttpClient()
    {
        var handler = new HttpClientHandler()
        {
            ServerCertificateCustomValidationCallback = delegate { return true; }
        };
        HttpClient httpClient = new HttpClient(handler);
        httpClient.DefaultRequestHeaders.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("text/html"));
        httpClient.DefaultRequestHeaders.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/xhtml+xml"));
        httpClient.DefaultRequestHeaders.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/xml;q=0.9"));
        httpClient.DefaultRequestHeaders.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("image/avif"));
        httpClient.DefaultRequestHeaders.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("image/webp"));
        httpClient.DefaultRequestHeaders.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("image/apng"));
        httpClient.DefaultRequestHeaders.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("*/*;q=0.8"));
        httpClient.DefaultRequestHeaders.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/signed-exchange;v=b3;q=0.7"));
        httpClient.DefaultRequestHeaders.AcceptEncoding.Add(StringWithQualityHeaderValue.Parse("*"));
        httpClient.DefaultRequestHeaders.AcceptLanguage.Add(StringWithQualityHeaderValue.Parse("en-US"));
        httpClient.DefaultRequestHeaders.AcceptLanguage.Add(StringWithQualityHeaderValue.Parse("en;q=0.9"));
        httpClient.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests", "1");
        httpClient.DefaultRequestHeaders.UserAgent.Add(ProductInfoHeaderValue.Parse("Mozilla/5.0"));
        httpClient.DefaultRequestHeaders.UserAgent.Add(ProductInfoHeaderValue.Parse("(Windows NT 10.0; Win64; x64)"));
        httpClient.DefaultRequestHeaders.UserAgent.Add(ProductInfoHeaderValue.Parse("AppleWebKit/537.36"));
        httpClient.DefaultRequestHeaders.UserAgent.Add(ProductInfoHeaderValue.Parse("(KHTML, like Gecko)"));
        httpClient.DefaultRequestHeaders.UserAgent.Add(ProductInfoHeaderValue.Parse("Chrome/126.0.6478.126"));
        httpClient.DefaultRequestHeaders.UserAgent.Add(ProductInfoHeaderValue.Parse("Safari/537.36"));
        httpClient.DefaultRequestHeaders.ExpectContinue = false;
        return httpClient;
    }

    public static async Task DownloadFile(string url, string file, CancellationToken cancellationToken, Action<TransferProgress> transferProgressAction = null)
    {
        var httpClient = GetHttpClient();
        using (var fs = File.OpenWrite(file))
        {
            var rep = await httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
            var totalFileSize = rep.Content.Headers.ContentLength.Value;
            var lastDisplayTime = DateTime.MinValue;
            var readTotalCount = 0;
            var buffer = new byte[10 * 1024];
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            using (var ns = rep.Content.ReadAsStream())
            {
                while (true)
                {
                    if ((DateTime.Now - lastDisplayTime).TotalSeconds > 0.5 && stopwatch.ElapsedMilliseconds > 0)
                    {
                        StringBuilder sb = new StringBuilder();
                        var speed = readTotalCount * 1D / stopwatch.ElapsedMilliseconds;
                        var speed_long = Convert.ToInt64(speed * 1000);
                        var remainingTime = TimeSpan.FromMilliseconds((totalFileSize - readTotalCount) / speed);
                        transferProgressAction?.Invoke(new TransferProgress()
                        {
                            Current = readTotalCount,
                            Total = totalFileSize,
                            Speed = speed_long,
                            RemainingTime = remainingTime
                        });
                        lastDisplayTime = DateTime.Now;
                    }
                    var ret = await ns.ReadAsync(buffer, 0, buffer.Length, cancellationToken);
                    readTotalCount += ret;
                    fs.Write(buffer, 0, ret);
                    if (readTotalCount >= totalFileSize)
                        break;
                }
            }
            stopwatch.Stop();
        }
    }
}
