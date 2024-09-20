using System;
using System.Collections.ObjectModel;
using ClientConvertisseurV2_2.Models;
using ClientConvertisseurV2_2.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;

namespace ClientConvertisseurV2_2.ViewModels;

/// <summary>
/// Classe permettant de gérer la conversion d'un montant en euros en une devise sélectionnée
/// </summary>
public class ConvertisseurEuroViewModel : BaseConvertisseurViewModel
{
    protected override async void ActionSetConversion()
    {
        var baseAmount = MontantDevise;
        Double intAmount = await ProcessAmount(baseAmount);
        var devise = SelectedDevise;
        Devise checkedDevise = await ProcessDevise(devise);

        double totalAmount = intAmount * checkedDevise.Taux;
        MontantConverti = totalAmount.ToString();
    }

}