using System.ComponentModel.DataAnnotations;
using BlazorApp1.Models;
using BlazorApp1.Services;
using BlazorBootstrap;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BlazorApp1.ViewModel;

internal sealed class UserSearchViewModel : ObservableObject
{
    private readonly IService<Utilisateur> _utilisateurService;
    private readonly ToastService _toastService;

    public Utilisateur? Model { get; private set; }
    public SearchQuery _SearchQuery { get; set; } = new SearchQuery();

    public UserSearchViewModel(IService<Utilisateur> utilisateurService, ToastService toastService)
    {
        _utilisateurService = utilisateurService;
        _toastService = toastService;
    }

    public async Task Search()
    {
        Model = await _utilisateurService.GetByStringAsync("Utilisateurs/GetByEmail", _SearchQuery.Email);

        if (Model != null)
        {
            _toastService.Notify(new(ToastType.Success, "Succès !", "Utilisateur trouvé"));
        }
        else
        {
            _toastService.Notify(new(ToastType.Warning, "Aucun résultat", "Utilisateur non trouvé"));
        }
    }

    public async Task<bool> Submit()
    {
        if (Model == null)
            return false;

        bool isSuccess = await _utilisateurService.PutAsync("Utilisateurs", Model.UtilisateurId, Model);

        if (isSuccess)
        {
            _toastService.Notify(new(ToastType.Success, "Succès !", "Utilisateur modifié"));
        }
        else
        {
            _toastService.Notify(new(ToastType.Danger, "Erreur", "Erreur lors de la modification de l'utilisateur"));
        }

        return isSuccess;
    }

    public class SearchQuery
    {
        [Required(ErrorMessage = "Le champ est obligatoire")]
        [EmailAddress(ErrorMessage = "Le champ doit être un mail valide")]
        public string? Email { get; set; }
    }
}