using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DigiBank.NuGet.API.Interfaces;

public interface IModelCustom<TResponse>
    where TResponse : IHttpResponse
{
    IReadOnlyList<TResponse> Items { get; set; }
    TResponse? Item { get; set; }
    bool IsReturnMessage { get; set; }
    String Message { get; set; }
    bool IsPagination { get; set; }
    int Offset { get; set; }
    int Limit { get; set; }
    Int64 Total { get; set; }
    Exception Exception { get; set; }
    IList<Exception> Exceptions { get; set; }
    HttpStatusCode StatusCode { get; set; }
}
