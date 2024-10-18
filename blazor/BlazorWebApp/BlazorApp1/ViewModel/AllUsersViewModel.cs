using BlazorApp1.Models;
using BlazorApp1.Services;

namespace BlazorApp1.ViewModel;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

internal sealed partial class AllUsersViewModel : ObservableObject
{
    private readonly IService<Utilisateur> _service;
    
    public AllUsersViewModel(IService<Utilisateur> service)
    {
        _service = service;
    }
    
    [ObservableProperty] private List<Utilisateur> _utilisateurs = new();
    
    [RelayCommand]
    private async Task LoadUsers()
    {
        _utilisateurs = await _service.GetAllAsync("Utilisateurs");
    }
}

