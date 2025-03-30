using System.ComponentModel;

namespace DecodeQrCode.Domain.Enum;

public enum RequestType
{
    [Description("application/json")]
    APPLICATION_JSON,
    [Description("application/jose")]
    APPLICATION_JOSE
}
