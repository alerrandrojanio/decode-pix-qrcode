using System.Net;

namespace DecodeQrCode.Domain.Exceptions;

public class DecodeException : Exception
{
    public HttpStatusCode StatusCode { get; }

    public DecodeException(string message, Exception innerException, HttpStatusCode statusCode)
        : base(message, innerException)
    {
        StatusCode = statusCode;
    }
}
