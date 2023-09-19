﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Usuarios
    {
        private CD_Usuarios objCapaDato = new CD_Usuarios();
        public List<Usuario> Listar()
        {
            return objCapaDato.Listar(); //Retorna un método que está en la clase CD_Usuarios
        }

        public int Registrar(Usuario obj, out string Mensaje)
        {
            //toda las reglas del Negocio antes de registrar en la BBDD
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Nombres) || string.IsNullOrWhiteSpace(obj.Nombres))
            {
                Mensaje = "El nombre del usuario no puede estar vacío";
            }
            else if (string.IsNullOrEmpty(obj.Apellidos) || string.IsNullOrWhiteSpace(obj.Apellidos))
            {
                Mensaje = "El apellido del usuario no puede estar vacío";
            }
            else if (string.IsNullOrEmpty(obj.Correo) || string.IsNullOrWhiteSpace(obj.Correo))
            {
                Mensaje = "El correo del usuario no puede estar vacío";
            }

            if (string.IsNullOrEmpty(Mensaje)) //si sigue vacío es porque no ha ocurrido ningún error
            {

                string clave = CN_Recursos.GenerarClave();
                string asunto = "Creacion de Cuenta";
                string mensaje_correo = "<h3>Su cuenta fue creada correctamente</h3></br><p>Su contraseña para acceder es : !clave!</p>";
                mensaje_correo = mensaje_correo.Replace("!clave!", clave);

                bool respuesta = CN_Recursos.EnviarCorreo(obj.Correo,asunto,mensaje_correo);

                if (respuesta)
                {
                    obj.Clave = CN_Recursos.ConvertirSha256(clave); //propiedad donde almacena la contraseña
                    return objCapaDato.Registrar(obj, out Mensaje);
                }
                else
                {
                    Mensaje = "No se puede enviar el correo";
                    return 0;

                }             
                
            }
            else
            {
                return 0;

            }
        }

        public bool Editar(Usuario obj, out string Mensaje)
        {

            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Nombres) || string.IsNullOrWhiteSpace(obj.Nombres))
            {
                Mensaje = "El nombre del usuario no puede ser vacío";
            }
            else if (string.IsNullOrEmpty(obj.Apellidos) || string.IsNullOrWhiteSpace(obj.Apellidos))
            {
                Mensaje = "El apellido del usuario no puede ser vacío";
            }
            else if (string.IsNullOrEmpty(obj.Correo) || string.IsNullOrWhiteSpace(obj.Correo))
            {
                Mensaje = "El correo del usuario no puede ser vacío";
            }

            if (string.IsNullOrWhiteSpace(Mensaje))
            {
                return objCapaDato.Editar(obj, out Mensaje);
            }
            else
            {
                return false;
            }
           
        }

        public bool Eliminar(int id, out string Mensaje)
        {
            return objCapaDato.Eliminar(id, out Mensaje);
        }
    }
}
