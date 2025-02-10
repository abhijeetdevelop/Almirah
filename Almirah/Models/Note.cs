using Almirah.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;

namespace Almirah.Models;

public partial class Note : ObservableObject
{
    private IEncryptionService _encryptionService;

    [PrimaryKey] [AutoIncrement] public int Id { get; set; }

    [ObservableProperty] private string _title;
    [ObservableProperty] private string _encryptedContent;

    [ObservableProperty] private DateTime _createdAt = DateTime.UtcNow;
    [ObservableProperty] private DateTime _updatedAt = DateTime.UtcNow;

    public Note()
    {
    }

    public void InitializeEncryptionService(IEncryptionService encryptionService)
    {
        _encryptionService = encryptionService;
    }

    public string Content
    {
        get => _encryptionService?.Decrypt(_encryptedContent) ?? string.Empty;
        set
        {
            if (_encryptionService != null)
            {
                _encryptedContent = _encryptionService.Encrypt(value);
                OnPropertyChanged();
            }
        }
    }
}
