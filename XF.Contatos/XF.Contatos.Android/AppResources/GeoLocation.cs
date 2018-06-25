using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Xamarin.Forms;
using Xamarin.Geolocation;
using XF.Contatos.Droid;
using XF.Contatos.Global;

[assembly: Dependency(typeof(GeoLocation))]
namespace XF.Contatos.Droid
{
	public class GeoLocation : ILocalizacao
    {
		public void GetCoordenada()
        {
            var context = MainApplication.CurrentContext as Activity;
            var locator = new Geolocator(context) { DesiredAccuracy = 50 };
            locator.GetPositionAsync(timeout: 10000).ContinueWith(t => {
                SetCoordenada(t.Result.Latitude, t.Result.Longitude);
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

		public void startMap(Coordenada coordenada)
		{
			var geoUri = Android.Net.Uri.Parse("geo:" + coordenada.Latitude + "," + coordenada.Longitude);
            var mapIntent = new Intent(Intent.ActionView, geoUri);
			var context = MainApplication.CurrentContext as Activity;
			context.StartActivity(mapIntent);
		}

		void SetCoordenada(double paramLatitude, double paramLongitude)
        {
            var coordenada = new Coordenada()
            {
                Latitude = paramLatitude.ToString(),
                Longitude = paramLongitude.ToString()
            };

            MessagingCenter.Send<ILocalizacao, Coordenada>(this, "coordenada", coordenada);
        }
    }
}
