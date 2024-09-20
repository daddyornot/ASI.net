using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ClientConvertisseurV2_2.Models;
using ClientConvertisseurV2_2.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ClientConvertisseurV2_2.ViewModels;

public abstract class BaseConvertisseurViewModel : ObservableObject
{
    public BaseConvertisseurViewModel()
    {
        BtnSetConversion = new RelayCommand(ActionSetConversion);
        _wsServices = WSServices.GetInstance("http://localhost:5179/api/");
        DialogService = new DialogService();
        ActionGetAsyncData();
    }

    private ObservableCollection<Devise> _devises;
    private WSServices _wsServices;
    protected DialogService DialogService;
    public IRelayCommand BtnSetConversion { get; }
    private string _montantDevise;

    public string MontantDevise
    {
        get { return _montantDevise; }
        set
        {
            _montantDevise = value;
            OnPropertyChanged();
        }
    }

    private Devise _selectedDevise;

    public Devise SelectedDevise
    {
        get { return _selectedDevise; }
        set
        {
            _selectedDevise = value;
            OnPropertyChanged();
        }
    }

    private string _montantConverti;

    public string MontantConverti
    {
        get { return _montantConverti; }
        set
        {
            _montantConverti = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Méthode permettant de récupérer les devises depuis l'API
    /// </summary>
    /// <exception cref="Exception"></exception>
    private async void ActionGetAsyncData()
    {
        var result = await _wsServices.GetDevisesAsync("devises");

        if (result == null)
        {
            DialogService.Dialog.XamlRoot = App.MainRoot.XamlRoot;
            await DialogService.ShowAsyncErrorDialog("API - Erreur lors de la récupération des devises");
            return;
            throw new Exception("Erreur lors de la récupération des devises");
        }

        Devises = new ObservableCollection<Devise>(result);
    }

    public ObservableCollection<Devise> Devises
    {
        get { return _devises; }
        set
        {
            _devises = value;
            OnPropertyChanged(); // Notify the UI that the property has changed
        }
    }

    /// <summary>
    /// Vérifie si le montant est valide et ouvre une boîte de dialogue si ce n'est pas le cas
    /// </summary>
    /// <param name="baseAmount">Le montant récupéré depuis la view</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    protected async Task<Double> ProcessAmount(string baseAmount)
    {
        if (!Double.TryParse(baseAmount, out Double intAmount))
        {
            DialogService.Dialog.XamlRoot = App.MainRoot.XamlRoot;
            await DialogService.ShowAsyncErrorDialog("Veuillez saisir un montant valide");
            throw new Exception("Montant non valide");
        }

        return intAmount;
    }

    /// <summary>
    /// Gère la devise sélectionnée et ouvre une boîte de dialogue si aucune devise n'est sélectionnée
    /// </summary>
    /// <param name="devise">La devise récupérée depuis la view</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    protected async Task<Devise> ProcessDevise(Devise devise)
    {
        if (devise == null)
        {
            DialogService.Dialog.XamlRoot = App.MainRoot.XamlRoot;
            await DialogService.ShowAsyncErrorDialog("Veuillez sélectionner une devise");
            throw new Exception("Devise non sélectionnée");
        }
        return devise;
    }

    /// <summary>
    /// Méthode permettant de convertir un montant
    /// </summary>
    /// <exception cref="Exception"></exception>
    protected abstract void ActionSetConversion();
}