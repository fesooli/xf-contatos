using System;
namespace XF.Contatos.Global
{
	public interface ILocalizacao
    {
        void GetCoordenada();

		void startMap(Coordenada coordenada);
    }

    public class Coordenada
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
