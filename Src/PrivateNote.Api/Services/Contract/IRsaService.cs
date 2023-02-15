﻿namespace PrivateNote.Api.Services.Contract;

public interface IRsaService
{
    (string publicKey , string privateKey) CreateKeys();
    string? Encrypt(string text, string publicKey);
    string? Decrypt(string encryptedText, string privateKey);
    string? Sign(string text, string privateKey);
    bool Verify(string text, string signature, string publicKey);
}