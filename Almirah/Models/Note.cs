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

    [ObservableProperty] private DateTime _createdAt = DateTime.UtcNow;
    [ObservableProperty] private DateTime _updatedAt = DateTime.UtcNow;

    public string Content
    {
        get => Decrypt(_encryptedContent);
        set
        {
            _encryptedContent = Encrypt(value);
            OnPropertyChanged();
        }
    }

    private static readonly byte[]
        Key = Encoding.UTF8.GetBytes("YourFixedSecretKey12345678901234"); // Check if exactly 32 bytes

    private static readonly byte[] IV = Encoding.UTF8.GetBytes("YourIV1234567890"); // 16 bytes for AES IV.

    private string Encrypt(string plainText)
    {
        if (string.IsNullOrEmpty(plainText)) return string.Empty;

        using (var aesAlg = Aes.Create())
        {
            aesAlg.Key = Key;
            aesAlg.IV = IV;

            var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using (var msEncrypt = new MemoryStream())
            {
                using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (var swEncrypt = new StreamWriter(csEncrypt))
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

        using (var aesAlg = Aes.Create())
        {
            aesAlg.Key = Key;
            aesAlg.IV = IV;

            var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using (var msDecrypt = new MemoryStream(Convert.FromBase64String(encryptedText)))
            {
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (var srDecrypt = new StreamReader(csDecrypt))
                    {
                        return srDecrypt.ReadToEnd();
                    }
                }
            }
        }
    }
}