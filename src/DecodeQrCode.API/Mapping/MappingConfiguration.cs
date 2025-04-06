using DecodeQrCode.API.Models.Decode;
using DecodeQrCode.Domain.DTOs.Decode;
using DecodeQrCode.Domain.DTOs.Decode.Response;
using DecodeQrCode.Domain.DTOs.Decode.Response.Objects;
using DecodeQrCode.Domain.DTOs.JWS;
using DecodeQrCode.Domain.DTOs.JWS.Objects;
using DecodeQrCode.Domain.DTOs.QrCode;
using DecodeQrCode.Domain.Extensions;
using Mapster;
using System.Globalization;

namespace DecodeQrCode.API.Mapping;

public static class MappingConfiguration
{
    public static void RegisterMappings()
    {
        #region DecodeQrCode
        TypeAdapterConfig<DecodeQrCodeModel, DecodeQrCodeDTO>
            .NewConfig()
            .Map(dest => dest.QrCode, src => src.Body!.QrCode);

        #region StaticQrCode
        TypeAdapterConfig<QrCodeDTO, StaticQrCodeResponseDTO>
            .NewConfig()
            .Map(dest => dest.MerchantCategoryCode, src => src.MerchantCategoryCode ?? "0000")
            .Map(dest => dest.PixKey, src => src.MerchantAccountInformation!.Key)
            .Map(dest => dest.KeyType, src => src.MerchantAccountInformation!.Key!.GetPixKeyType().ToString())
            .Map(dest => dest.FSS, src => src.MerchantAccountInformation!.FSS)
            .Map(dest => dest.GUI, src => src.MerchantAccountInformation!.GUI)
            .Map(dest => dest.AdditionalData, src => src.MerchantAccountInformation!.AdditionalInformation)
            .Map(dest => dest.TxId, src => src.AdditionalDataField!.TxId)
            .Map(dest => dest.TransactionAmount, src => Convert.ToDecimal(src.TransactionAmount, CultureInfo.InvariantCulture));
        #endregion StaticQrCode

        #region DynamicQrcode
        TypeAdapterConfig<JWSValueDTO, ValueDTO>
            .NewConfig()
            .Map(dest => dest.OriginalValue, src => src.OriginalValue)
            .Map(dest => dest.ModalityChange, src => src.ModalityChange)
            .Map(dest => dest.Rebate, src => src.Rebate)
            .Map(dest => dest.Discount, src => src.Discount)
            .Map(dest => dest.Interest, src => src.Interest)
            .Map(dest => dest.Fine, src => src.Fine)
            .Map(dest => dest.Final, src => src.Final)
            .Map(dest => dest.Withdrawal, src => src.Removal.Withdrawal)
            .Map(dest => dest.Change, src => src.Removal.Change);

        TypeAdapterConfig<JWSWithdrawalDTO, WithdrawalDTO>
            .NewConfig()
            .Map(dest => dest.Value, src => src.Value)
            .Map(dest => dest.AlterModality, src => src.AlterModality)
            .Map(dest => dest.WithdrawalServiceProvider, src => src.WithdrawalServiceProvider)
            .Map(dest => dest.AgentModality, src => src.AgentModality);

        TypeAdapterConfig<JWSChangeDTO, ChangeDTO>
            .NewConfig()
            .Map(dest => dest.Value, src => src.Value)
            .Map(dest => dest.AlterModality, src => src.AlterModality)
            .Map(dest => dest.WithdrawalServiceProvider, src => src.WithdrawalServiceProvider)
            .Map(dest => dest.AgentModality, src => src.AgentModality);

        TypeAdapterConfig<JWSDebtorDTO, DebtorDTO>
            .NewConfig()
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.CPF, src => src.CPF)
            .Map(dest => dest.CNPJ, src => src.CNPJ);

        TypeAdapterConfig<JWSRecipientDTO, RecipientDTO>
            .NewConfig()
            .Map(dest => dest.CPF, src => src.CPF)
            .Map(dest => dest.CNPJ, src => src.CNPJ)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.FantasyName, src => src.FantasyName)
            .Map(dest => dest.Street, src => src.Street)
            .Map(dest => dest.City, src => src.City)
            .Map(dest => dest.State, src => src.State)
            .Map(dest => dest.ZipCode, src => src.ZipCode);

        TypeAdapterConfig<JWSAdditionalInformationDTO, AdditionalInformationResponseDTO>
            .NewConfig()
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Value, src => src.Value);

        #region ImmediateQrCode
        TypeAdapterConfig<(QrCodeDTO qrCodeDTO, JWSDTO jwsDTO), ImmediateQrCodeResponseDTO>
            .NewConfig()
            .Map(dest => dest.CreationDate, src => src.jwsDTO.Payload.Calendar.CreationDate)
            .Map(dest => dest.PresentationDate, src => src.jwsDTO.Payload.Calendar.PresentationDate)
            .Map(dest => dest.Expiration, src => src.jwsDTO.Payload.Calendar.Expiration)
            .Map(dest => dest.Value, src => src.jwsDTO.Payload.Value)
            .Map(dest => dest.Debtor, src => src.jwsDTO.Payload.Debtor)
            .Map(dest => dest.Recipient, src => src.jwsDTO.Payload.Recipient)
            .Map(dest => dest.TxId, src => src.jwsDTO.Payload.TxId)
            .Map(dest => dest.Key, src => src.jwsDTO.Payload.Key)
            .Map(dest => dest.KeyType, src => src.jwsDTO.Payload.Key.GetPixKeyType().ToString())
            .Map(dest => dest.PayerSolicitation, src => src.jwsDTO.Payload.PayerSolicitation)
            .Map(dest => dest.Status, src => src.jwsDTO.Payload.Status)
            .Map(dest => dest.PayloadFormatIndicator, src => src.qrCodeDTO.PayloadFormatIndicator)
            .Map(dest => dest.TransactionCurrency, src => src.qrCodeDTO.TransactionCurrency)
            .Map(dest => dest.CRC16, src => src.qrCodeDTO.CRC16)
            .Map(dest => dest.GUI, src => src.qrCodeDTO.MerchantAccountInformation.GUI);
        #endregion ImmediateQrCode

        #region DueDateQrCode
        TypeAdapterConfig<(QrCodeDTO qrCodeDTO, JWSDTO jwsDTO), DueDateQrCodeResponseDTO>
            .NewConfig()
            .Map(dest => dest.CreationDate, src => src.jwsDTO.Payload.Calendar.CreationDate)
            .Map(dest => dest.PresentationDate, src => src.jwsDTO.Payload.Calendar.PresentationDate)
            .Map(dest => dest.DueDate, src => src.jwsDTO.Payload.Calendar.DueDate)
            .Map(dest => dest.ValidityAfterExpiration, src => src.jwsDTO.Payload.Calendar.ValidityAfterExpiration)
            .Map(dest => dest.Value, src => new ValueDTO())
            .Map(dest => dest.Debtor, src => src.jwsDTO.Payload.Debtor)
            .Map(dest => dest.Recipient, src => src.jwsDTO.Payload.Recipient)
            .Map(dest => dest.TxId, src => src.jwsDTO.Payload.TxId)
            .Map(dest => dest.Key, src => src.jwsDTO.Payload.Key)
            .Map(dest => dest.KeyType, src => src.jwsDTO.Payload.Key.GetPixKeyType().ToString())
            .Map(dest => dest.PayerSolicitation, src => src.jwsDTO.Payload.PayerSolicitation)
            .Map(dest => dest.Status, src => src.jwsDTO.Payload.Status)
            .Map(dest => dest.PayloadFormatIndicator, src => src.qrCodeDTO.PayloadFormatIndicator)
            .Map(dest => dest.TransactionCurrency, src => src.qrCodeDTO.TransactionCurrency)
            .Map(dest => dest.CRC16, src => src.qrCodeDTO.CRC16)
            .Map(dest => dest.GUI, src => src.qrCodeDTO.MerchantAccountInformation.GUI);
        #endregion DueDateQrCode
        #endregion DynamicQrcode
        #endregion DecodeQrCode
    }
}