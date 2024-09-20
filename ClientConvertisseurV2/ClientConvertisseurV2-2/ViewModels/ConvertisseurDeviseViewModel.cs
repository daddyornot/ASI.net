using System;
using System.Collections.ObjectModel;
using ClientConvertisseurV2_2.Models;
using ClientConvertisseurV2_2.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;

namespace ClientConvertisseurV2_2.ViewModels;

/// <summary>
/// Classe permettant de gérer la conversion d'un montant en devise en euros
/// </summary>
public class ConvertisseurDeviseViewModel : ObservableObject
{
    public ConvertisseurDeviseViewModel()
    {
        BtnSetConversion = new RelayCommand(ActionSetConversion);
        _wsServices = WSServices.GetInstance("http://localhost:5179/api/");
        _dialogService = new DialogService();
        ActionGetAsyncData();
    }

    private ObservableCollection<Devise> _devises;
    private WSServices _wsServices;
    private DialogService _dialogService;
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
            _dialogService.Dialog.XamlRoot = App.MainRoot.XamlRoot;
            await _dialogService.ShowAsyncErrorDialog("API - Erreur lors de la récupération des devises");
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
    /// Méthode permettant de convertir un montant en devise en euros
    /// </summary>
    /// <exception cref="Exception"></exception>
    private async void ActionSetConversion()
    {
        var baseAmount = MontantDevise;
        if (!Double.TryParse(baseAmount, out Double intAmount))
        {
            _dialogService.Dialog.XamlRoot = App.MainRoot.XamlRoot;
            await _dialogService.ShowAsyncErrorDialog("Veuillez saisir un montant valide");
            throw new Exception("Montant non valide");
        }

        var devise = SelectedDevise;
        if (devise == null)
        {
            _dialogService.Dialog.XamlRoot = App.MainRoot.XamlRoot;
            await _dialogService.ShowAsyncErrorDialog("Veuillez sélectionner une devise");
            throw new Exception("Devise non sélectionnée");
        }

        double totalAmount = intAmount * devise.Taux;
        MontantConverti = totalAmount.ToString();
    }
}