﻿using LoginRegistroAPI.Servicios.Interfaces;
using System.Security.Cryptography;
using System.Text;
using LoginRegistroAPI.Models;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace LoginRegistroAPI.Servicios.Logica
{
    public class LogicaUsuario : IUsuario
    {
        Conexion cn = new(); //VARIABLE PARA LLAMAR A LA CADENA DE CONEXION (SERVICIOS/CONEXION.CS)


        //----------(1)VARIABLE Y CONSTRUCTOR PARA USAR EL JWT (VIENE DEL BUILDER DE PROGRAM.CS)--------------//
        //---------(2)VARIABLE Y CONSTRUCTOR PARA CARGAR ARCHIVOS (VIENE DE APPSETTINGS.JSON)---------------//
        private readonly string _secretKey;//(1)
        private readonly string _rutaArchivos;//(2)
        public LogicaUsuario(IConfiguration config)
        {
            _secretKey = config.GetSection("settings").GetSection("secretkey").ToString(); //(1)
            _rutaArchivos = config.GetSection("Settings").GetSection("RutaArchivos").Value; //(2)
        }
        //---------------------------------------------(1)-(2)---------------------------------------------//

        public async Task<string> PostRegistrar(Usuario Ob)
        {
            using (SqlConnection conexion = new(cn.GetCadenaSQL()))
            {
                conexion.Open();
                var cmd = new SqlCommand("SP_RegistrarUsuario", conexion);
                cmd.Parameters.AddWithValue("Nombre", Ob.Nombre);
                cmd.Parameters.AddWithValue("UserName", Ob.UserName);
                cmd.Parameters.AddWithValue("Email", Ob.Email);
                cmd.Parameters.AddWithValue("Contraseña", Ob.Contraseña);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
            }
            var response = "Se ha creado el usuairo con exito";
            return await Task.FromResult(response);
        }

        public async Task<string> PostLogin(Usuario Ob)
        {
            try
            {
                using (SqlConnection conexion = new(cn.GetCadenaSQL()))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("SP_Login", conexion);
                    cmd.Parameters.AddWithValue("Email", Ob.Email);
                    cmd.Parameters.AddWithValue("Contraseña", Ob.Contraseña);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                //LLAMAMOS AL METODO QUE GENERO EL JWT("Autorizacion(Ob)") Y LO ALMACENAMOS EN "response"
                string response = Autorizacion(Ob);
                return await Task.FromResult(response.ToString());
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<string> PostArchivos(Documentos Ob) //METODO PARA CARGAR ARCHIVOS
        {
            string rutaDocumento = Path.Combine(_rutaArchivos, Ob.Archivo.FileName); //CREAMOS EL NOMBRE/RUTA ARCHIVO

            try
            {
                using FileStream newFile = File.Create(rutaDocumento);//CREAMOS UN NUEVO ARCHIVO CON EL NOMBRE/RUTA CREADOS ANTERIORMENTE; 
                                                                      //EN ESTE PUNTO EL ARCHIVO ESTA EN BLANCO, SOLO CONTIENE UN NOMBRE/RUTA
                Ob.Archivo.CopyTo(newFile);//COPIA EL ARCHIVO AL NOMBRE/RUTA CREADOS
                newFile.Flush();//LIMPIAMOS EL "FileStream" PARA FUTUROS USOS

                using (SqlConnection conexion = new(cn.GetCadenaSQL()))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("SP_GuardarDocumentos", conexion);
                    cmd.Parameters.AddWithValue("Descripcion", Ob.Descripcion);
                    cmd.Parameters.AddWithValue("Ruta", rutaDocumento);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                var response = $"Se ha guardado el archivo con exito. Ruta de la imagen: {rutaDocumento}";
                return await Task.FromResult(response);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message.ToString());
            }
        }

        public async Task<List<Usuario>> GetAllUsuarios()
        {
            List<Usuario> lista = new();
            using (SqlConnection conexion = new(cn.GetCadenaSQL()))
            {

                conexion.Open();
                var cmd = new SqlCommand("SP_ListarUsuarios", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using var reader = cmd.ExecuteReader();//El "SqlDataReader" se puede reemplazar por un "var"
                while (reader.Read())
                {
                    lista.Add(new Usuario()
                    {
                        IdUsuario = Convert.ToInt32(reader["Id"]),
                        Nombre = reader["Nombre"].ToString(),
                        UserName = reader["UserName"].ToString(),
                        Email = reader["Email"].ToString(),
                        Contraseña = reader["Contraseña"].ToString()
                    });
                }
            }
            return await Task.FromResult(lista);
        }


        //------------------------------METODO PARA CREAR Y LEER EL JWT START------------------------------//
        private string Autorizacion(Usuario request)
        {
            var keyBytes = Encoding.ASCII.GetBytes(_secretKey);

            var claims = new ClaimsIdentity();

            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, request.Email));

            //---------------------------------CREACION DEL TOKEN START----------------------------------//
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
            };
            //------------------------------------CREACION DEL TOKEN END--------------------------------//

            //--------------------------------LECTURA DEL TOKEN START-----------------------------------//
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenCOnfig = tokenHandler.CreateToken(tokenDescriptor);

            string tokenCreado = tokenHandler.WriteToken(tokenCOnfig);

            return tokenCreado;
            //--------------------------------LECTURA DEL TOKEN END-----------------------------------//
        }
        //----------------------------METODO PARA CREAR Y LEER EL JWT END-------------------------------------//
    }
}








