using System;

namespace Coelsa.Common
{
    public static class Constants
    {
        #region .:Messages:.
        public const string MSG_NOT_RESULTS             = "No se encontraron resultados.";
        public const string MSG_CONTACT_NOT_FOUND       = "El contacto no fue encontrado.";
        public const string MSG_CONTACT_INSERTED        = "El contacto fue guardado exitosamente.";
        public const string MSG_CONTACT_NOT_INSERTED    = "El contacto no pudo ser insertado.";
        public const string MSG_PARAM_REQUIRED          = "El parametro {0} es requerido.";
        public const string MSG_CONTACT_UPDATED         = "El contacto fue actualizado exitosamente.";
        public const string MSG_CONTACT_NOT_UPDATED     = "El contacto no fue actualizado.";
        public const string MSG_CONTACT_DELETED         = "El contacto fue eliminado exitosamente.";
        public const string MSG_CONTACT_NOT_DELETED     = "El contacto no fue eliminado.";
        public const string MSG_CONTACT_NOT_EXISTS      = "El contacto no existe.";
        public const string MSG_CONTACT_NOT_MATCH       = "El ID de contacto del objeto no concuerda con el ID del parametro.";

        #endregion

        #region .:Enumerators:.
        public enum ResponseCode
        {
            SUCCESS,
            BUSINESS_ERROR,
            APPLICATION_ERROR
        }

        public enum Crud
        {
            CREATE,
            READ,
            UPDATE,
            DELETE
        }
        #endregion
    }
}
