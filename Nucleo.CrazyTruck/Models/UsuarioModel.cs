﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;

namespace Nucleo.CrazyTruck.Models
{
    class UsuarioModel
    {
        CrazyTruckDBEntitiesCn context = new CrazyTruckDBEntitiesCn();

        public void activateEmail(string correo)
        {
            //Inicializan las variables del cliente, las credenciales y el mensaje
            NetworkCredential login;
            SmtpClient client;
            MailMessage msg = new MailMessage();

            //Se obtiene un codigo aleatorio como token
            string token = codigoAleatorio();
            //Se inserta el token que se le va a dar a dicha activacion respecto al correo que lo solicito
            modificarTokenActivacion(correo, token);

            //Se agrega la informacion del mensaje
            msg.From = new MailAddress("crazytruckcc@gmail.com");
            msg.Subject = "Activar correo";
            msg.To.Add(new MailAddress(correo));
            msg.Body = "Se ha creado una cuenta con este correo en la pagina crazy truck, para activar la cuenta, favor" +
                "de hacer click en el siguiente ne lace o copiar y pegar en su navegador: http://activacion.cshtml?uid= " + token;

            //Se declaran las variables con la siguiente informacion
            
            //Las credenciales almacena la informacion del correo del cual se envia 
            login = new NetworkCredential("crazytruckcc@gmail.com", "Cece2018");

            //Se declara el host y el puerto por el cual se va a enviar el correo
            client = new SmtpClient("smtp.gmail.com", Convert.ToInt32(587));
            client.EnableSsl = true;
            client.Credentials = login;
            client.Send(msg);
        }

        //Metodo el cual modifica el token del correo al cual se va a enviar el email de ativacion respecto al correo
        public void modificarTokenActivacion(string correo, string token)
        {
            int idUsu= context.Usuario.Single(d => d.email == correo).id;
            Usuario usu = context.Usuario.Where(e => e.id == idUsu).First();
            usu.tokenActivacion = token;
            context.SaveChanges();

        }

        //Metodo el cual envia un correo de recuperar contrasenia al correo asignado
        public void RecoverPass(string correo)
        {
            NetworkCredential login;
            SmtpClient client;
            MailMessage msg = new MailMessage();

            string token = codigoAleatorio();
            editaRecuperar(correo, token);

            msg.From = new MailAddress("crazytruckcc@gmail.com");
            msg.Subject = "Activacion";
            msg.To.Add(new MailAddress (correo));
            msg.Body = "Se ha creado solicitado una recuperacion de contrasenia para su cuenta de crazy truck, " +
                "para recuperar dicha contrasenia favor de hacer click en el siguiente enlace o copiar y pegar "+
                "en su navegador: http://crazytruck/activar.cshtml?uid= " + token;

            login = new NetworkCredential("crazytruckcc@gmail.com", "Cece2018");
            client = new SmtpClient("smtp.gmail.com", Convert.ToInt32(587));
            client.EnableSsl = true;
            client.Credentials = login;
            client.Send(msg);
        }

        //Metodo el cual genera un codigo aleatorio con la clase Guid la cual te da un total de 16 digitos
        public string codigoAleatorio()
        {
            //Declaramos una variable int la cual se va a utilizar para indicar cuantos caracteres de Guid va a tener el token
            int longitud = 7;
            //Aqui la variable miGuid ya tiene un valor de 16 digitos, por ejemplo "1254-4828-8448-8549"
            Guid miGuid = Guid.NewGuid();
            //Aqui lo pasamos a string para poder retornarlo
            //Luego le decimos que las partes donde esten guiones (los cuales los pone solo la clase Guid al generar un string)
            //sean eliminados y el string se recorra a la misma vez con la campo "string.Empty"
            //Ejemplo "Guid solo" = 1254-4828-8448-8549 "ConReplace"= 1254482884488549
            //Por ultimo, con el campo ".Substring" le indicamos que desde el primer valor hasta el ultimo valor indicado en
            //la variable "longitud" es lo que se va a regresar en la variable token
            string token = miGuid.ToString().Replace("-", string.Empty).Substring(0, longitud);
            return token;
        }

        //Metodo el cual edita los siguientes datos del correo al cual se le esta modificando la contrasenia 
        public void editaRecuperar(string correo, string token)
        {
            int idT = context.Usuario.Single(d => d.email == correo).id;
            Usuario usu = context.Usuario.Where(a => a.id == idT).First();

            usu.passRequest = true;
            usu.tokenPassword = token;

            context.SaveChanges();
        }
    }
}
