using BlazorApp1.Models;
using BlazorApp1.Services;
using BlazorBootstrap;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BlazorApp1.ViewModel;

internal sealed partial class AddUserViewModel : ObservableObject
{
    private readonly IService<Utilisateur> _utilisateurService;
    private readonly ToastService _toastService;

    public Utilisateur Model { get; set; } = new Utilisateur();

    public AddUserViewModel(IService<Utilisateur> utilisateurService, ToastService toastService)
    {
        _utilisateurService = utilisateurService;
        _toastService = toastService;
    }

    public async Task<bool> Submit()
    {
        bool isSuccess = await _utilisateurService.PostAsync("Utilisateurs", Model);

        if (isSuccess)
        {
            _toastService.Notify(new(ToastType.Success, "Succès !", "Utilisateur ajouté"));
        }
        else
        {
            _toastService.Notify(new(ToastType.Danger, "Erreur ajout", "Erreur lors de l'ajout de l'utilisateur"));
        }

        return isSuccess;
    }
}