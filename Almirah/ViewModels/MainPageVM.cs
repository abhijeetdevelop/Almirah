using System.Collections.ObjectModel;
using System.Windows.Input;
using Almirah.Models;
using Almirah.Services.Interfaces;

namespace Almirah.ViewModels;

public class MainPageVM : BindableObject
{
    private readonly IStorageService _storageService;
    private readonly IFileSystemService _fileSystemService;
    
    private FileItem _selectedFile;
    private ObservableCollection<FileItem> _files;
    
    public ICommand CopyCommand { get; }
    public ICommand PasteCommand { get; }
    public ObservableCollection<FileItem> Files
    {
        get => _files;
        set
        {
            _files = value;
            OnPropertyChanged();
        }
    }
    
    // Holds the currently selected file/folder for copy-paste
    public FileItem SelectedFile
    {
        get => _selectedFile;
        set
        {
            _selectedFile = value;
            OnPropertyChanged();
        }
    }

    public MainPageVM(IStorageService storageService, IFileSystemService fileSystemService)
    {
        _storageService = storageService;
        _fileSystemService = fileSystemService;

        Files = new ObservableCollection<FileItem>();
        
        CopyCommand = new Command<FileItem>(OnCopy);
        PasteCommand = new Command<FileItem>(OnPaste);
    }

    // Load files from storage
    private async Task LoadFiles()
    {
        //var files = await _storageService.GetFilesAsync();
        Files.Clear();
        
        Files.Add(new FileItem { Name = "A" });
        Files.Add(new FileItem { Name = "B" });
        Files.Add(new FileItem { Name = "C" });
        
        // foreach (var file in files) 
        //     Files.Add(file);
    }
    
    public async Task OnAppearing()
    {
        await LoadFiles();
    }

    // Handle copy operation
    private void OnCopy(FileItem file)
    {
        // Cache the selected file for paste operation
        SelectedFile = file;
    }

    // Handle paste operation
    private async void OnPaste(FileItem file)
    {
        if (SelectedFile == new FileItem()) return;
        // Perform copy-paste operation
        var result = await _fileSystemService.CopyFileAsync(SelectedFile, file);
        if (result)
            // Optionally refresh the files
            await LoadFiles();
    }
}