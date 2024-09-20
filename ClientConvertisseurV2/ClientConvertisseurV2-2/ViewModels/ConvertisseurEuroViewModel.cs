using System.Collections.ObjectModel;
using ClientConvertisseurV2_2.Models;
using ClientConvertisseurV2_2.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ClientConvertisseurV2_2.ViewModels;

public class ConvertisseurEuroViewModel: ObservableObject
{
    private ObservableCollection<Devise> devises;
    private WSServices service;

    public ConvertisseurEuroViewModel()
    {
        service = WSServices.GetInstance("http://localhost:5179/api/");
        ActionGetAsyncData();
    }

    private async void ActionGetAsyncData()
    {
        var result = await service.GetDevisesAsync("devises");
        Devises = new ObservableCollection<Devise>(result);
    }

    public ObservableCollection<Devise> Devises
   {
      get { return devises; }
      set
      {
         devises = value;
         OnPropertyChanged(); // Notify the UI that the property has changed
      }
   }
}