using System.Security.Cryptography;
using PrivateNote.Api.Services.Contract;

namespace PrivateNote.Api.Services;
public class RsaService : IRsaService
{
    private readonly RSAEncryptionPadding _encryptionPadding = RSAEncryptionPadding.Pkcs1;
    private readonly RSASignaturePadding _signaturePadding = RSASignaturePadding.Pkcs1;
    private readonly HashAlgorithmName _hashAlgorithm = HashAlgorithmName.SHA256;
    private const int MaxDataSize = 245;
    private const int EncryptedBlockSize = 256;

    public (string publicKey , string privateKey ) CreateKeys()
    {
        var rsa = RSA.Create();
        return (rsa.ToXmlString(false), rsa.ToXmlString(true));
    }
    private static RSA CreateFromKey(string key)
    {
        var rsa = new RSACryptoServiceProvider();
        rsa.FromXmlString(key);
        return rsa;
    }
    private static byte[] GetBytes(string text) => Encoding.Unicode.GetBytes(text);
    private static string GetString(byte[] textBytes) => Encoding.Unicode.GetString(textBytes);
    private static IList<byte[]> Split(byte[] input, int blockSize)
    {
        var result = new List<byte[]>();
        var numberOfBlock = (input.Count() / blockSize) + (input.Count() % blockSize == 0 ? 0 : 1);
        for (var i = 0; i < numberOfBlock; i++)
        {
            result.Add(input.Skip(i * blockSize).Take(blockSize).ToArray());
        }
        return result;
    }
    private static byte[] Merge(IList<byte[]> input)
    {
        var totalSize = input.Sum(item => item.Length);
        var result = new byte[totalSize];
        var currentSize = 0;
        for (var i = 0; i < input.Count; i++)
        {
            Array.Copy(input[i], 0, result, currentSize, input[i].Length);
            currentSize += input[i].Length;
        }
        return result;
    }
    public string? Encrypt(string text, string publicKey)
    {
        var rsa = CreateFromKey(publicKey);
        var textBytes = GetBytes(text);
        byte[] encryptedBytes;
        if (textBytes.Length > MaxDataSize)
        {
            var encryptedBytesList = new List<byte[]>();
            var listOfTextBytes = Split(textBytes, MaxDataSize);
            for (var i = 0; i < listOfTextBytes.Count; i++)
            {
                var item = listOfTextBytes[i];
                var encryptedItem = rsa.Encrypt(item, _encryptionPadding);
                encryptedBytesList.Add(encryptedItem);
            }
            encryptedBytes = Merge(encryptedBytesList);
        }
        else
        {
            encryptedBytes = rsa.Encrypt(textBytes, _encryptionPadding);
        }
        var encryptedText = Convert.ToBase64String(encryptedBytes);
        return encryptedText;
    }
    public string? Decrypt(string encryptedText, string privateKey)
    {
        var rsa = CreateFromKey(privateKey);
        var encryptedBytes = Convert.FromBase64String(encryptedText);
        byte[] bytes;
        if (encryptedBytes.Length > EncryptedBlockSize)
        {
            var decryptedBytesList = new List<byte[]>();
            var listOfEncryptedBytes = Split(encryptedBytes, EncryptedBlockSize);
            for (var i = 0; i < listOfEncryptedBytes.Count; i++)
            {
                var item = listOfEncryptedBytes[i];
                var decryptedBytes = rsa.Decrypt(item, _encryptionPadding);
                decryptedBytesList.Add(decryptedBytes);
            }
            bytes = Merge(decryptedBytesList);
        }
        else
        {
            bytes = rsa.Decrypt(encryptedBytes, _encryptionPadding);
        }
        var decryptedText = GetString(bytes);
        return decryptedText;
    }
    public string? Sign(string text, string privateKey)
    {
        var rsa = CreateFromKey(privateKey);
        var textBytes = GetBytes(text);
        var signatureBytes = rsa.SignData(textBytes, _hashAlgorithm, _signaturePadding);
        var signature = Convert.ToBase64String(signatureBytes);
        return signature;
    }
    public bool Verify(string text, string signature, string publicKey)
    {
        var rsa = CreateFromKey(publicKey);
        var textBytes = GetBytes(text);
        var signatureBytes = Convert.FromBase64String(signature);
        var result = rsa.VerifyData(textBytes, signatureBytes, _hashAlgorithm, _signaturePadding);
        return result;
    }
}