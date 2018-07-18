using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _C_ProgRes
{
    //////////////////////////////////////////////////////////////////
    // Clase Para manejo de las constantes de las aplicaciones
    //////////////////////////////////////////////////////////////////
    public class ClasX_Constans
    {
        public enum inDB_DateFormats
        {
            BD_DATE_FORMAT_DD_MM_AAAA = 0
            ,
            BD_DATE_FORMAT_AAAA_MM_DD = 1
                , BD_DATE_FORMAT_MM_DD_AAAA = 2
        }
        // Los esquema de colores
        public enum inEsquema_Colores
        {
            ESQUEMA__COLOR_ROJO = 0 
            , ESQUEMA_COLOR_AZUL = 1
        }
        // Las diferentes Acciones
        public const String ACCION_ADICION = "ADICION";
        public const String ACCION_MODIFICACION = "MODIFICACION";
        public const String ACCION_CONSULTA = "CONSULTA";
        public const String ACCION_ELIMINAR = "ELIMINAR";
        public const String ACCION_INGRESAR = "INGRESAR";
        public const String ACCION_PROCEDURE = "PROCEDURE";
        //
        public const String ESTADO_VIGENTE = "V";
        public const String ESTADO_NO_VIGENTE = "N";
        //
        public const String ESTADO_ACTIVO = "-1";
        public const String ESTADO_NO_ACTIVO = "0";
        //
        public const String CLASX_TIPO_DATO_STRING = "S";
        public const String CLASX_TIPO_DATO_NUMERIC = "N";
        public const String CLASX_TIPO_DATO_DATE = "D";
        public const String CLASX_TIPO_DATO_BOOLEAN = "B";
        public const String CLASX_TIPO_DATO_DOUBLE = "U";
        //
        public const String SYSTEM_DATETIME = "SYSTEM_DATE_TIME";
        //
        public const String SECCION_BD_0 = "SYS_BD_ZERO"; // La seccion para leer los parametros de la base de datos.
        //public const String SECCION_BD_0_DEMO = "SYS_BD_ZERO_DEMO"; // La seccion para leer los parametros de conexion de la base de datos.
        public const String SECCION_BD_CONNECT_INFO = "BD_CONNECT_INFO"; // La seccion para leer los parametros de conexion con la base de datos, tipo de conexion Fenix.
        //
        public const String SECCION_BD_1 = "SYS_BD_ONE"; // La seccion para leer los parametros de la base de datos. Segunda Base de Datos.
        //
        public const String SECCION_ID_APP = "APP"; // Seccion donde esta el Id de la Aplicacion
        public const String SECCION_OBJETOS_APP = "OBJETOS"; // Seccion donde estan los objetos a registrar
        //
        public const String NEW_LINE = "\r\n"; // Caracteres para nueva linea
        // Los permossos
        public const String PERMISO_READ = "R";
        public const String PERMISO_WRITE = "W";
        public const String PERMISO_DELETE = "D";
        public const String PERMISO_DISPLAY = "Y";
        //
        //////////////////////////////////////////////////////////////////////////////////////
        // Los Mensajes utilizados, en la parte del login.
        //////////////////////////////////////////////////////////////////////////////////////
        public const String MENSAJE_1 = "La información del la Base de Datos, no está definida en el archivo de configuración de la aplicación";
        public const String MENSAJE_2 = "El tipo de Conexión para la Base de Datos, está mal configurada o no está definida en el archivo de configuración de la aplicación";
        public const String MENSAJE_3 = "Acceso Negado";
        public const String MENSAJE_4 = "A continuación se debe definir la información de conexión de la Base de Datos, para poder trabajar con la aplicación";
        public const String MENSAJE_5 = "Atención";
        //
        public const String MENSAJE_6 = "¿ Está seguro de grabar esta información, para la conexión con la base de datos ?";
        public const String MENSAJE_7 = "Error al establecer la conexión";
        public const String MENSAJE_8 = "La conexión se estableció correctamente";
        public const String MENSAJE_9 = "Ingreso al Sistema";
        public const String MENSAJE_10 = "Ingreso Información de Conexión a la Base de Datos";
        //
        public const String MENSAJE_11 = "El código de Usuario:";
        public const String MENSAJE_12 = "No está registrado, como usuario válido en el sistema.";
        public const String MENSAJE_13 = "No está ACTIVO en el sistema.";
        public const String MENSAJE_14 = "No tiene contraseña asignada. Se presentará una ventana, para definir la contraseña del usuario.";
        public const String MENSAJE_15 = "Contraseña Inválida.";
        public const String MENSAJE_16 = "Los días de validez de la contraseña, han caducado. Se presentará una ventana, para definir la contraseña del usuario.";
        public const String MENSAJE_17 = "Cambio de Contraseña";
        public const String MENSAJE_18 = "La Contraseña actual no coincide, con la digitada.";
        public const String MENSAJE_19 = "Las nuevas Contraseñas, no son iguales.";
        public const String MENSAJE_20 = "La nueva Contraseña, debe ser diferente de la Contraseña actual.";
        public const String MENSAJE_21 = "Se ha cambiado la Contraseña del usuario.";
        public const String MENSAJE_22 = "Favor entrar en contacto con el Administrador del Sistema.";
        public const String MENSAJE_23 = "Hallando información de los servidores de Bases de Datos disponibles.";
        public const String MENSAJE_24 = "Por favor esperar unos segundos..............";
        public const String MENSAJE_25 = "Intentando establecer conexión con el servidor de Base de Datos.";
        //       
        public const String MENSAJE_26 = "Se ha detectado que la base de datos:";
        public const String MENSAJE_27 = "Instalada en el servidor:";
        public const String MENSAJE_28 = "No está actualizada";
        public const String MENSAJE_29 = "Se deben realizar los siguientes cambios en la Base de Datos, para poder ejecutar la aplicación";
        public const String MENSAJE_30 = "Creación de Tablas:";
        public const String MENSAJE_31 = "Creación de Campos:";
        public const String MENSAJE_32 = "Creación de Procedimientos Almacenados:";
        public const String MENSAJE_33 = "Creación de Vistas ( Views ) :";
        public const String MENSAJE_34 = "Creación de Índices:";
        //
        public const String MENSAJE_35 = "Tabla :";
        public const String MENSAJE_36 = "Campo :";
        public const String MENSAJE_37 = "Índice :";
        //
        public const String MENSAJE_38 = "Error en la autenticación en el Dominio de Windows";
        //
    }
}
