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
public class ConvertisseurDeviseViewModel : BaseConvertisseurViewModel
{
    /// <summary>
    /// Méthode permettant de convertir un montant en devise en euros
    /// </summary>
    /// <exception cref="Exception"></exception>
    protected override async void ActionSetConversion()
    {
        var baseAmount = MontantDevise;
        Double intAmount = await ProcessAmount(baseAmount);
        var devise = SelectedDevise;
        Devise checkedDevise = await ProcessDevise(devise);

        var totalAmount = intAmount / checkedDevise.Taux;
        MontantConverti = totalAmount.ToString();
    }
}