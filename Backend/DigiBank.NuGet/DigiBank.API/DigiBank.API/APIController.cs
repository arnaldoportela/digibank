using DigiBank.NuGet.API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Primitives;
using System.Net;

namespace DigiBank.NuGet.API;

public class ApiController<TResponse> : Controller
    where TResponse : IHttpResponse
{
    protected string? _apiName { get; set; }

    public ApiController() { }

    protected virtual IActionResult CustomResponse(IModelCustom<TResponse> response)
    {
        if (response == null)
        {
            return StatusCode((int)HttpStatusCode.NoContent);
        }

        if (response.IsReturnMessage)
        {
            return StatusCode((int)response.StatusCode, response?.Message);
        }

        if ((response?.Items?.Count ?? 0) < 1 && (response?.Total ?? 0) == 0)
        {
            return StatusCode((int)HttpStatusCode.NoContent);
        }

        bool totalpages = (response?.Limit + response?.Offset) >= response?.Total;

        if (response?.Total > response?.Items?.Count && !totalpages)
        {
            SetPagedHeader<IModelCustom<TResponse>>(response);

            if (response.IsPagination)
            {
                return StatusCode((int)HttpStatusCode.PartialContent, new
                {
                    _offset = response.Offset,
                    _limit = response.Limit,
                    total = response.Total,
                    items = response.Items
                });
            }

            return StatusCode((int)HttpStatusCode.PartialContent, response?.Items);
        }

        response.StatusCode = (int)response.StatusCode != 0 ? response.StatusCode : HttpStatusCode.OK;

        if (response.StatusCode == HttpStatusCode.BadRequest ||
            response.StatusCode == HttpStatusCode.NotFound ||
            response.StatusCode == HttpStatusCode.NoContent)
        {
            return StatusCode((int)response.StatusCode);
        }

        if (response?.Items is not null)
        {
            return StatusCode((int)response.StatusCode, response.Items);
        }

        return StatusCode((int)response.StatusCode, response.Item);
    }

    protected virtual IActionResult CustomResponse(ModelStateDictionary modelState)
    {
        var retornoError = GetErrors(modelState);

        return StatusCode((int)HttpStatusCode.PreconditionFailed, retornoError);
    }

    private List<string> GetErrors(ModelStateDictionary modelState)
    {
        var query = from state in modelState.Values
                    from error in state.Errors
                    select error.ErrorMessage;

        var errorList = query.ToList();
        return errorList;
    }

    protected virtual void SetPagedHeader<T>(IModelCustom<TResponse> model)
    {
        Request.HttpContext.Response.Headers.Append(new KeyValuePair<string, StringValues>("_offset", model.Offset.ToString()));
        Request.HttpContext.Response.Headers.Append(new KeyValuePair<string, StringValues>("_limit", model.Limit.ToString()));
        Request.HttpContext.Response.Headers.Append(new KeyValuePair<string, StringValues>("_total", model.Total.ToString()));
    }


    private bool IsPropertyExist(dynamic settings)
    {
        return settings.GetType().GetProperty("StatusCode") != null;
    }

    private bool IsPropertyMessage(dynamic settings)
    {
        return settings.GetType().GetProperty("Message") != null;
    }
}