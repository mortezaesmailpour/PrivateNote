﻿namespace PrivateNote.Api.Services.Contract;

public interface ILoggerAdapter<TType>
{
    void LogInformation(string? message, params object?[] args);
    void LogError(string? message, params object?[] args);
    void LogError(Exception? exception, string? message, params object?[] args);
}