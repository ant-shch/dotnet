using System;
using System.Buffers;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using Xunit;
using Xunit.Abstractions;

namespace ReplaceDomainNameWithSpan.Test
{
    public class ReplaceHostNameTest
    {
        private const int Count = 1000000;
        private const string FileUrl = "https://enzofilesuattr.blob.core.windows.net/inspection/EKSPERT%C4%B0Z%20RAPORU20190618070025233.pdf";

        private const string PrivateStorageUrl = "https://prvtrstagingopsvava.blob.core.windows.net/";

        private readonly Uri Endpoint = new Uri(PrivateStorageUrl);
        private readonly ITestOutputHelper testOutputHelper;

        public ReplaceHostNameTest(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void UrlClassPerformenceTest()
        {
            var prefix = Endpoint.Scheme + "://" + Endpoint.Host;
            var miliseconds = GetExucutionTimeMiliseconds(() =>
             {
                 var fileUri = new Uri(FileUrl);
                 var storageFileUri = prefix + fileUri.AbsolutePath;
             });

            testOutputHelper.WriteLine($"Run Count: {Count}, time in miliseconds: {miliseconds}");
        }

        [Fact]
        public void StringBuilderPerformenceTest()
        {
            var miliseconds = GetExucutionTimeMiliseconds(() =>
            {
                var b = new StringBuilder();
                b.Append(Endpoint.Scheme);
                b.Append("://");
                b.Append(Endpoint.Host);
                b.Append(FileUrl);
                var storageFileUri = b.ToString();
            });

            testOutputHelper.WriteLine($"Run Count: {Count}, time in miliseconds: {miliseconds}");
        }

        [Fact]
        public void StringConcatPerformenceTest()
        {

            var miliseconds = GetExucutionTimeMiliseconds(() =>
            {
                var storageFileUri = String.Concat(Endpoint.Scheme, "://", Endpoint.Host, FileUrl);
            });

            testOutputHelper.WriteLine($"Run Count: {Count}, time in miliseconds: {miliseconds}");
        }


        [Fact]
        public void ConcatTest()
        {
            var prefix = Endpoint.Scheme + "://" + Endpoint.Host;
            var miliseconds = GetExucutionTimeMiliseconds(() =>
            {
                var storageFileUri = prefix + FileUrl;
            });

            testOutputHelper.WriteLine($"Run Count: {Count}, time in miliseconds: {miliseconds}");
        }

        [Theory]
        [InlineData("https://enzofilesuattr.blob.core.windows.net/inspection/EKSPERT%C4%B0Z%20RAPORU20190618070025233.pdf", PrivateStorageUrl + "inspection/EKSPERT%C4%B0Z%20RAPORU20190618070025233.pdf")]
        [InlineData("enzofilesuattr.blob.core.windows.net/inspection/EKSPERT%C4%B0Z%20RAPORU20190618070025233.pdf", PrivateStorageUrl + "inspection/EKSPERT%C4%B0Z%20RAPORU20190618070025233.pdf")]
        [InlineData("/inspection/EKSPERT%C4%B0Z%20RAPORU20190618070025233.pdf", PrivateStorageUrl + "inspection/EKSPERT%C4%B0Z%20RAPORU20190618070025233.pdf")]
        public void SplitDomainTest(string fileUrl, string expected)
        {
            string fullPath = GetFileAbsolutePath(fileUrl);
            Assert.Equal(expected, fullPath);
        }


        [Fact]
        public void SplitDomainPerformenceTest()
        {
            var miliseconds = GetExucutionTimeMiliseconds(() =>
            {
                string fullPath = GetFileAbsolutePath(FileUrl);
            });

            testOutputHelper.WriteLine($"Run Count: {Count}, time in miliseconds: {miliseconds}");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string GetFileAbsolutePath(string fileUrl)
        {
            string r = string.Empty;
            var schemaDelimeter = fileUrl.IndexOf("://");
            var schemaDelimeterLength = 3;
            var slashLength = 1;
            if (schemaDelimeter < 0)
            {
                schemaDelimeter = fileUrl.IndexOf('/');
                r = fileUrl.Substring(schemaDelimeter + slashLength);
            }
            else
            {
                schemaDelimeter = fileUrl.IndexOf('/', schemaDelimeter + schemaDelimeterLength);
                r = fileUrl.Substring(schemaDelimeter + slashLength);
            }
            var fullPath = PrivateStorageUrl + r;
            return fullPath;
        }

        [Fact]
        public void SplitDomainSpanPerformenceTest()
        {
            var miliseconds = GetExucutionTimeMiliseconds(() =>
            {
                GetAbsolutePathUsingSpan(FileUrl);
            });

            testOutputHelper.WriteLine($"Run Count: {Count}, time in miliseconds: {miliseconds}");
        }

        [Theory]
        [InlineData("https://enzofilesuattr.blob.core.windows.net/inspection/EKSPERT%C4%B0Z%20RAPORU20190618070025233.pdf", PrivateStorageUrl + "inspection/EKSPERT%C4%B0Z%20RAPORU20190618070025233.pdf")]
        [InlineData("enzofilesuattr.blob.core.windows.net/inspection/EKSPERT%C4%B0Z%20RAPORU20190618070025233.pdf", PrivateStorageUrl + "inspection/EKSPERT%C4%B0Z%20RAPORU20190618070025233.pdf")]
        [InlineData("/inspection/EKSPERT%C4%B0Z%20RAPORU20190618070025233.pdf", PrivateStorageUrl + "inspection/EKSPERT%C4%B0Z%20RAPORU20190618070025233.pdf")]
        public void SplitDomainSpanTest(string fileUrl, string expected)
        {
            string fullPath = GetAbsolutePathUsingSpan(fileUrl);
            Assert.Equal(expected, fullPath);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string GetAbsolutePathUsingSpan(string fileUrl)
        {
            var fileUrlSpan = fileUrl.AsSpan();
            var schemaDelimeter = fileUrlSpan.IndexOf("://");
            var schemaDelimeterLength = 3;
            var slashLength = 1;
            if (schemaDelimeter < 0)
            {
                schemaDelimeter = fileUrlSpan.IndexOf('/');
                return Concat(PrivateStorageUrl.AsSpan(), fileUrlSpan.Slice(schemaDelimeter + slashLength));
            }
            else
            {
                var urlWithoutScheme = fileUrlSpan.Slice(schemaDelimeter + schemaDelimeterLength);
                var nextAfterScheme = urlWithoutScheme.IndexOf('/');
                return Concat(PrivateStorageUrl.AsSpan(), urlWithoutScheme.Slice(nextAfterScheme + slashLength));
            }
        }

        public static string Concat(ReadOnlySpan<char> span0, ReadOnlySpan<char> span1)
        {
            var result = new char[span0.Length + span1.Length];
            var resultSpan = result.AsSpan();
            span0.CopyTo(result);
            var from = span0.Length;
            span1.CopyTo(resultSpan.Slice(from));
            return new string(result);
        }

        [Fact]
        public void SplitPerformenceTest()
        {
            var miliseconds = GetExucutionTimeMiliseconds(() =>
            {
                var fileUriParts = FileUrl.Split(new[] { "blob.core.windows.net" }, StringSplitOptions.RemoveEmptyEntries);
                if (fileUriParts.Length > 1)
                {
                    var storageFileUri = Endpoint.Scheme + "://" + Endpoint.Host + fileUriParts[1];
                }
                else
                {
                    var storageFileUri = Endpoint.Scheme + "://" + Endpoint.Host + fileUriParts[0];
                }
            });

            testOutputHelper.WriteLine($"Run Count: {Count}, time in miliseconds: {miliseconds}");
        }

        [Fact]
        public void RegexPerformenceTest()
        {
            var reg = new Regex("^(?:\\S+\\.\\S+?\\/|\\/)?(\\S+)$", RegexOptions.Compiled);
            var miliseconds = GetExucutionTimeMiliseconds(() =>
            {
                var value = reg.Replace(FileUrl, String.Empty);
            });

            testOutputHelper.WriteLine($"Run Count: {Count}, time in miliseconds: {miliseconds}");
        }

        private double GetExucutionTimeMiliseconds(Action method, int count = 1000000, int runs = 20)
        {
            double sum = 0;
            for (int it = 0; it < runs; it++)
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                for (int i = 0; i < count; i++)
                {
                    method.Invoke();
                }
                stopWatch.Stop();
                sum += stopWatch.Elapsed.TotalMilliseconds;
            }
            return sum / runs;
        }
    }
}
