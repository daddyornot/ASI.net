using System.Collections.ObjectModel;
using ClientConvertisseurV2.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ClientConvertisseurV2.ViewModels;

public class ConvertisseurEuroViewModel: ObservableObject
{

   private ObservableCollection<Devise> devises;
   
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