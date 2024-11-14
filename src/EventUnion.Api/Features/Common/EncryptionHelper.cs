using System.Security.Cryptography;

namespace EventUnion.Api.Features.Common;

// TODO empurrar lógica para um objeto de valor: Password e EncryptedPassword(receberá Password como param)
public static class EncryptionHelper
{
    public static (string EncryptedPassword, string EncryptionKey, string IV) EncryptPassword(string password)
    {
        using var aes = Aes.Create();
        aes.GenerateKey();
        aes.GenerateIV();

        var key = aes.Key;
        var iv = aes.IV;

        using var encryptor = aes.CreateEncryptor(key, iv);
        using var ms = new MemoryStream();
        using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
        using (var sw = new StreamWriter(cs))
        {
            sw.Write(password);
        }
        var encrypted = ms.ToArray();
        return (Convert.ToBase64String(encrypted), Convert.ToBase64String(key), Convert.ToBase64String(iv));
    }
    
    public static string DecryptPassword(string encryptedPassword, string encryptionKey, string iv)
    {
        var key = Convert.FromBase64String(encryptionKey);
        var cipherText = Convert.FromBase64String(encryptedPassword);
        var ivBytes = Convert.FromBase64String(iv);

        using var aes = Aes.Create();
        aes.Key = key;
        aes.IV = ivBytes;

        using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
        using var ms = new MemoryStream(cipherText);
        using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
        using var sr = new StreamReader(cs);
        return sr.ReadToEnd();
    }
}