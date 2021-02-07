# Coelsa Challenge

## Contenido:
- Código fuente
- Script de bbdd (Objetos + datos dummy)
- Colección de pruebas en postman (Coelsa.postman_collection.json)

## Prerequisitos:
Antes de ejecutar la aplicación o pruebas unitarias ejecutar el scritp de bbdd(Coelsa_Scritpt_DB.sql).

### Parametros de configuración en appsettings.json:
SqlConnection: string de conexion a BBDD, se debe configurar tanto en el proyecto de testing(Coelsa.UnitTests) como en el de web api(Coelsa.WebApi).

## Servicios disponibles
- Crear Contacto.
- Actualizar Contacto.
- Eliminar Contacto.
- Obtener contactos paginado por empresa
- Obtener todos los contactos.
- Obtener contacto por Identificador.
