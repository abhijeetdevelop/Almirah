using System;
using SQLite;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Almirah.Models ;

public partial class Note : ObservableObject
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [ObservableProperty]
    private string _title;

    [ObservableProperty]
    private string _content;

    [ObservableProperty]
    private DateTime _createdAt = DateTime.UtcNow;

    [ObservableProperty]
    private DateTime _updatedAt = DateTime.UtcNow;
}