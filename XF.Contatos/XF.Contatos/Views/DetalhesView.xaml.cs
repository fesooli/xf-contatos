using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;
using XF.Contatos.Global;
using XF.Contatos.Models;

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

			//if (contato.Thumbnail != null)
			//{
			//	Stream stream = new MemoryStream(contato.Thumbnail);
			//	imgContato.Source = ImageSource.FromStream(() => { return stream; });
			//}
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
