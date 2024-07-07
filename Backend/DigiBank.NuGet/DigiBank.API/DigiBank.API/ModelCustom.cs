using DigiBank.NuGet.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DigiBank.NuGet.API;

public class ModelCustom<TResponse> : IModelCustom<TResponse>
    where TResponse : IHttpResponse
{
    public IReadOnlyList<TResponse> Items { get; set; }
    public TResponse? Item { get; set; }
    public bool IsReturnMessage { get; set; }
    public String Message { get; set; }
    public bool IsPagination { get; set; }
    public int Offset { get; set; }
    public int Limit { get; set; }
    public Int64 Total { get; set; }
    public Exception Exception { get; set; }
    public IList<Exception> Exceptions { get; set; }
    public HttpStatusCode StatusCode { get; set; }

    public ModelCustom() {
        Offset = 0; 
        Limit = 10; 
        Total = -1;
        StatusCode = HttpStatusCode.OK;
    }

    public ModelCustom(IReadOnlyList<TResponse> items,
                       TResponse? item,
                       bool isReturnMessage,
                       string message,
                       int offset,
                       int limit,
                       long total,
                       Exception exception,
                       IList<Exception> exceptions,
                       HttpStatusCode statusCode)
    {
        Items = items;
        Item = item;
        IsReturnMessage = isReturnMessage;
        Message = message;
        Offset = offset;
        Limit = limit;
        Total = total;
        Exception = exception;
        Exceptions = exceptions;
        StatusCode = statusCode;
    }

    #region Constructors for Exception

    public ModelCustom(IReadOnlyList<TResponse> items, Exception Exception)
        : this(items, default(TResponse), false, null, 0, 10, -1, Exception, null, HttpStatusCode.OK) { }

    public ModelCustom(dynamic item, Exception Exception)
        : this(null, default(TResponse), false, null, 0, 10, -1, Exception, null, HttpStatusCode.OK)
    {
        Item = item;
    }

    #endregion

    #region Constructors for Message

    public ModelCustom(string message, HttpStatusCode statusCode)
        : this(null, default(TResponse), true, message, 0, 10, -1, null, null, statusCode) { }

    #endregion

    #region Constructors for Item

    public ModelCustom(TResponse item, HttpStatusCode statusCode)
        : this(null, default(TResponse), false, null, 0, 10, -1, null, null, statusCode)
    {
        Item = item;
    }

    public ModelCustom(TResponse item)
        : this(null, default(TResponse), false, null, 0, 10, -1, null, null, HttpStatusCode.OK)
    {
        Item = item;
    }

    #endregion

    #region Construtors for List of items, paginated or not

    public ModelCustom(IReadOnlyList<TResponse> items, int offset, int limit, long total, HttpStatusCode statusCode = HttpStatusCode.OK)
        : this(items, default(TResponse), false, null, offset, limit, total, null, null, statusCode) { }

    public ModelCustom(IReadOnlyList<TResponse> items, HttpStatusCode statusCode = HttpStatusCode.OK)
        : this(items, 0, 10, 0, statusCode) { }

    #endregion
}
