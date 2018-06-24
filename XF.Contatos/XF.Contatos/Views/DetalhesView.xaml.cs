using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XF.Contatos.Global;

namespace XF.Contatos.Views
{
    public partial class DetalhesView : ContentPage
    {
        public DetalhesView()
        {
            InitializeComponent();

			ILocalizacao geolocation = DependencyService.Get<ILocalizacao>();
            geolocation.GetCoordenada();

            MessagingCenter.Subscribe<ILocalizacao, Coordenada>
                (this, "coordenada", (objeto, geo) =>
                {
                    lblLongitude.Text = geo.Longitude;
                    lblLatitude.Text = geo.Latitude;
                });
        }
  
		private async void btnVerNoMapa_Clicked(object sender, EventArgs e)
        {
			ILocalizacao geolocation = DependencyService.Get<ILocalizacao>();
			geolocation.startMap(new Coordenada()
			{
				Latitude = lblLatitude.Text,
				Longitude = lblLongitude.Text
			});
        }
    }
}
