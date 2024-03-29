﻿namespace PrivateNote.Api.Dto.Responses;

public class RsaSignInResponse
{
    public string EncryptedToken { get; init; } = String.Empty;

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
