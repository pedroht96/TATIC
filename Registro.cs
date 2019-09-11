using System;

namespace BaseDados.Objetos
{
    public class Registro
    {
        private long Data { get; set; }

        private UInt64 Usuario { get; set; }

        public readonly long RData;

        public readonly UInt64 RUsuario;

        public Registro(long Data, UInt64 Usuario)
        {
            RData = this.Data = Data;
            RUsuario = this.Usuario = Usuario;
        }

        public DateTime GetDateTime() => new DateTime(Data);

        // Retorna a data no formato que esta no pdf
        public string GetDateTimeStringFormat() => new DateTime(Data).ToString("yyyyMMddHHmmssfff");
    }
}