using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.Telephony;
using Xamarin.Contacts;
using Xamarin.Forms;
using XF.Contatos.Droid.AppResources;
using XF.Contatos.Global;
using XF.Contatos.Models;

[assembly: Dependency(typeof(ContatoHelper))]
namespace XF.Contatos.Droid.AppResources
{
    public class ContatoHelper : IContatoHelper
    {
        public async Task<bool> GetContatoListAsync()
        {
            var context = MainApplication.CurrentContext as Activity;
            if (context == null) return false;

            var idRequestCode = 0;

            var contactsPermission = Manifest.Permission.ReadContacts;
			var phonePermission = Manifest.Permission.CallPhone;
			var mapsPermission = Manifest.Permission.AccessFineLocation;
			var coarsePermission = Manifest.Permission.AccessCoarseLocation;
			var cameraPermission = Manifest.Permission.Camera;
			var storagePermission = Manifest.Permission.WriteExternalStorage;
			string[] permissions = { contactsPermission, phonePermission, mapsPermission, coarsePermission, cameraPermission, storagePermission };
            
			if ((context.CheckSelfPermission(contactsPermission) != (int)Permission.Granted) ||
			    (context.CheckSelfPermission(phonePermission) != (int)Permission.Granted) ||
			    (context.CheckSelfPermission(mapsPermission) != (int)Permission.Granted) ||
			    (context.CheckSelfPermission(coarsePermission) != (int)Permission.Granted) ||
			    (context.CheckSelfPermission(cameraPermission) != (int)Permission.Granted) ||
			    (context.CheckSelfPermission(storagePermission) != (int)Permission.Granted))
            {
                context.RequestPermissions(permissions, idRequestCode);
				Task.Delay(10000).Wait();
            }
   
            var book = new AddressBook(context);
            if (!await book.RequestPermission())
            {
                Console.WriteLine("Permissão negada pelo usuário!");
                return false;
            }

            publishList(book.ToList());
            return true;
        }

        public bool LigarParaContato(Contato contato)
        {
            var context = MainApplication.CurrentContext as Activity;
            if (context == null) return false;

            var intent = new Intent(Intent.ActionCall);
            intent.SetData(Android.Net.Uri.Parse("tel:" + contato.Numero));

            if (IsIntentAvailable(context, intent))
            {
                context.StartActivity(intent);
                return true;
            }

            return false;
        }

        public static bool IsIntentAvailable(Context context, Intent intent)
        {
            var packageManager = context.PackageManager;

            var list = packageManager.QueryIntentServices(intent, 0)
                .Union(packageManager.QueryIntentActivities(intent, 0));

            if (list.Any()) return true;

            var manager = TelephonyManager.FromContext(context);
            return manager.PhoneType != Android.Telephony.PhoneType.None;
        }

        private void publishList(List<Contact> contactList)
        {
            var contatos = new List<Contato>();

            foreach (var cont in contactList)
            {
				Contato contato = new Contato
				{
					Nome = cont.DisplayName,
					Numero = cont.Phones.FirstOrDefault()?.Number
				};

				var image = cont.GetThumbnail();

				if(image != null) {
					var stream = new MemoryStream();
                    image.Compress(Bitmap.CompressFormat.Png, 80, stream);
					contato.Thumbnail = stream.ToArray();
				}
                           
				contatos.Add(contato);
            }

            MessagingCenter.Send<IContatoHelper, List<Contato>>(this, "obtercontatos", contatos);
        }
    }
}