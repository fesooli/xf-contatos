﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Contatos.Global;
using XF.Contatos.Models;
using XF.Contatos.ViewModels;

namespace XF.Contatos.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ContatosView : ContentPage
	{
		public ContatosView ()
		{
			InitializeComponent ();
			//var contatoHelper = DependencyService.Get<IContatoHelper>();

			//contatoHelper.GetContatoListAsync();
            loadContatos();
        }

        private async void loadContatos()
        {
            var contatoHelper = DependencyService.Get<IContatoHelper>();
            var result = await contatoHelper.GetContatoListAsync();

            if (!result)
                await DisplayAlert("", "Permissões não carregadas, reabra o APP!", "Ok");                
        }

        void OnContatoTapped(object sender, ItemTappedEventArgs e) => 
            ((ContatoViewModel)BindingContext).Discar((Contato)e.Item);

		private async void detalhes_Clicked(object sender, ItemTappedEventArgs e)
        {
			await Navigation.PushAsync(new DetalhesView());
        }
    }
}