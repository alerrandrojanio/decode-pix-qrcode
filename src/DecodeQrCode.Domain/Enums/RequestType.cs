using System.ComponentModel;

namespace DecodeQrCode.Domain.Enums;

public enum RequestType
{
    [Description("application/json")]
    APPLICATION_JSON,
    [Description("application/jose")]
    APPLICATION_JOSE
}
