using System;
using System.Security.Cryptography;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;

namespace Almirah.Models;

public partial class Note : ObservableObject
{
    [PrimaryKey] [AutoIncrement] public int Id { get; set; }

    [ObservableProperty] private string _title;
    [ObservableProperty] private string _encryptedContent;

    public string Content
    {
        get => Decrypt(_encryptedContent);
        set => _encryptedContent = Encrypt(value);
    }

    private static readonly byte[] Key = Encoding.UTF8.GetBytes("YourFixedSecretKey12345678901234"); // Check if exactly 32 bytes
    private static readonly byte[] IV = Encoding.UTF8.GetBytes("YourIV1234567890"); // 16 bytes for AES IV.
    
    private string Encrypt(string plainText)
    {
        if (string.IsNullOrEmpty(plainText)) return string.Empty; 
        
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Key;
            aesAlg.IV = IV;

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using (var msEncrypt = new System.IO.MemoryStream())
            {
                using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (var swEncrypt = new System.IO.StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }
    }

    private string Decrypt(string encryptedText)
    {
        if (string.IsNullOrEmpty(encryptedText)) return string.Empty; 

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Key;
            aesAlg.IV = IV;

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using (var msDecrypt = new System.IO.MemoryStream(Convert.FromBase64String(encryptedText)))
            {
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (var srDecrypt = new System.IO.StreamReader(csDecrypt))
                    {
                        return srDecrypt.ReadToEnd();
                    }
                }
            }
        }
    }

    [ObservableProperty] private DateTime _createdAt = DateTime.UtcNow;
    [ObservableProperty] private DateTime _updatedAt = DateTime.UtcNow;
}