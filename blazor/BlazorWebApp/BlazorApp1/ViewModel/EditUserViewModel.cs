using BlazorApp1.Models;
using BlazorApp1.Services;
using BlazorBootstrap;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BlazorApp1.ViewModel;

internal sealed partial class EditUserViewModel : ObservableObject
{
    private readonly IService<Utilisateur> _utilisateurService;
    private readonly ToastService _toastService;

    public Utilisateur Model { get; private set; } = new Utilisateur();

    public EditUserViewModel(IService<Utilisateur> utilisateurService, ToastService toastService)
    {
        _utilisateurService = utilisateurService;
        _toastService = toastService;
    }

    [RelayCommand]
    public async Task LoadUser(int id)
    {
        Model = await _utilisateurService.GetByIdAsync("Utilisateurs/GetById", id);
    }

    public async Task<bool> SubmitChanges(int id)
    {
        bool isSuccess = await _utilisateurService.PutAsync("Utilisateurs", id, Model);
        if (isSuccess)
        {
            _toastService.Notify(new(ToastType.Success, "Succès !", "Utilisateur modifié"));
        }
        else
        {
            _toastService.Notify(new(ToastType.Danger, "Erreur modification", "Erreur lors de la modification de l'utilisateur"));
        }
        return isSuccess;
    }
}