using System;
using System.Threading.Tasks;
using ClientConvertisseurV1.Views;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace ClientConvertisseurV1.Services;

public class DialogService
{
    public ContentDialog Dialog;
    
    public DialogService()
    {
        Dialog = new ContentDialog
        {
            Title = "Erreur",
            Content = "Erreur inconnue",
            CloseButtonText = "Ok",
        };
    }
    
    public async Task ShowAsyncErrorDialog(string message)
    {
        Dialog.Content = message;
        ContentDialogResult result = await Dialog.ShowAsync();
    }
}
