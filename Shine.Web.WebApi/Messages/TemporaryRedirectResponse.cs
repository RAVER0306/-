using System;
using System.Net;

namespace Shine.Web.WebApi.Messages
{
    public class TemporaryRedirectResponse : ResourceIdentifierBase
    {
        public TemporaryRedirectResponse()
            : base(HttpStatusCode.TemporaryRedirect)
        { }

        public TemporaryRedirectResponse(Uri resource)
            : base(HttpStatusCode.TemporaryRedirect, resource)
        { }
    }
}