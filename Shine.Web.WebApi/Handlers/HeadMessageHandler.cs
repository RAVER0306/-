﻿using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Shine.Web.WebApi.Handlers
{
    public class HeadMessageHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Method == HttpMethod.Head)
            {
                request.Method = HttpMethod.Get;
                return base.SendAsync(request, cancellationToken)
                    .ContinueWith(task =>
                    {
                        HttpResponseMessage response = task.Result;
                        response.RequestMessage.Method = HttpMethod.Head;
                        response.Content = new HeadContent(response.Content);
                        return task.Result;
                    });
            }

            return base.SendAsync(request, cancellationToken);
        }
    }


    internal class HeadContent : HttpContent
    {
        public HeadContent(HttpContent content)
        {
            CopyHeaders(content.Headers, Headers);
        }

        protected override Task SerializeToStreamAsync(
            Stream stream,
            TransportContext context)
        {
            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
            tcs.SetResult(null);
            return tcs.Task;
        }

        protected override bool TryComputeLength(out long length)
        {
            length = -1;
            return false;
        }

        private static void CopyHeaders(IEnumerable<KeyValuePair<string, IEnumerable<string>>> fromHeaders,
            HttpContentHeaders toHeaders)
        {
            foreach (KeyValuePair<string, IEnumerable<string>> header in fromHeaders)
            {
                toHeaders.Add(header.Key, header.Value);
            }
        }
    }
}