using System;
using System.Collections.Generic;
using ClientConvertisseurV1.Models;
using ClientConvertisseurV1.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ClientConvertisseurV1.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ConvertisseurEuroPage : Page
    {
        private readonly WSServices _wsServices;
        private readonly DialogService _dialogService;

        public ConvertisseurEuroPage()
        {
            InitializeComponent();
            _dialogService = new DialogService();
            _wsServices = WSServices.GetInstance("http://localhost:5179/api/"); // le "/" a la fin est important !
            ActionGetAsyncData();
        }

        private async void ActionGetAsyncData()
        {
            var result = await _wsServices.GetDevisesAsync("devises");
            if (result == null)
            {
                _dialogService.Dialog.XamlRoot = Content.XamlRoot;
                await _dialogService.ShowAsyncErrorDialog("API - Erreur lors de la récupération des devises");
                return;
                throw new Exception("Erreur lors de la récupération des devises");
            }

            AllDevises.DataContext = new List<Devise>(result);
        }

        private async void ComputeAmount(object sender, RoutedEventArgs e)
        {
            var baseAmount = TxtAmount.Text;
            if (!Double.TryParse(baseAmount, out Double amount))
            {
                _dialogService.Dialog.XamlRoot = Content.XamlRoot;
                await _dialogService.ShowAsyncErrorDialog("Veuillez saisir un montant valide");
                throw new Exception("Montant non valide");
            }
            // amount = Double.Parse(baseAmount);

            var devise = AllDevises.SelectedItem as Devise;
            if (devise == null)
            {
                _dialogService.Dialog.XamlRoot = Content.XamlRoot;
                await _dialogService.ShowAsyncErrorDialog("Veuillez sélectionner une devise");
                throw new Exception("Devise non sélectionnée");
            }

            double totalAmount = amount * devise.Taux;
            TotalAmount.Text = totalAmount.ToString();
        }
    }
}