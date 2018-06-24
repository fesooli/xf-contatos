using System;

using Xamarin.Forms;

namespace XF.Contatos.Views
{
    public class DetalhesView : ContentPage
    {
        public DetalhesView()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Hello ContentPage" }
                }
            };
        }
    }
}

