using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Utilities.Static
{
    public class ReplyMessage
    {
        public const string MESSAGE_QUERY = "Consulta Exitosa.";
        public const string MESSAGE_QUERY_EMPTY = "No se encontraron registros.";
        public const string MESSAGE_SAVE = "Se registró correctamente.";
        public const string MESSAGE_UPDATE = "Se actualizó correctamente.";
        public const string MESSAGE_DELETE = "Se eliminó correctamente.";
        public const string MESSAGE_EXISTS = "El registro ya existe.";
        public const string MESSAGE_ACTIVETE = "El registro ha sido activado.";
        public const string MESSAGE_TOKEN = "Token generado correctamente.";
        public const string MESSAGE_VALIDATE = "Errores de validacicón.";
        public const string MESSAGE_FAILED = "Operación fallida.";
    }
}