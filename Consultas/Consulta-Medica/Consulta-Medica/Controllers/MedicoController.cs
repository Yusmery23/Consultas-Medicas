using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Consulta_Medica.Models;

using System.Data;
using System.Data.SqlClient;
namespace Consultas_Medicas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicoController : ControllerBase

        //creo mi variable de lectura
    {
        private readonly string cadenaSQL;

        // Creo el constructor

        public MedicoController(IConfiguration config)
        {
            cadenaSQL = config.GetConnectionString("CadenaSQL");

        }

        // Procedo a listar los Medicos

        [HttpGet]
        [Route("Lisa")]

        // Creo el Metodo

        public IActionResult Lista()
        {
            List<Medico> Lista = new List<Medico>();

            //Utilizo el Try para capturar los errores que existan

            try
            {
                //Realizo la conexion a SQL

                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();

                    //Creo comando de ejecución

                    var cmd = new SqlCommand("medico", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Realizo la lectura de la ejecución

                    using (var rd = cmd.ExecuteReader())
                    {

                        while (rd.Read())
                        {
                            Lista.Add(new Medico()
                            {

                                idMedico = Convert.ToInt32(rd["idMedico"]),
                                NombreMed = rd["NombreMed"].ToString(),
                                ApellidoMed = rd["ApellidoMed"].ToString(),
                                RunMed = rd["RunMed"].ToString(),
                                Eunacom = rd["Eunacom"].ToString(),
                                NacionalidadMed = rd["NacionalidadMed"].ToString(),
                                Especialidad = rd["Especialidad"].ToString(),
                                Horarios = rd["Horarios"].ToString(),
                                TarifaHr = Convert.ToInt32(rd["TarifaHr"]),


                            });
                        }
                    }
                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = Lista });

            }
            catch (Exception error)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message, response = Lista });

            }
        }


        //Creo el metodo para optener un nombre específico

        [HttpGet]
        [Route("Obtener/{idMedico:int}")]

        public IActionResult Obtener(int idMedico)
        {

            List<Medico> Lista = new List<Medico>();
            Medico medico = new Medico();

            //Utilizo el Try para capturar los errores que existan

            try
            {

                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();

                    //Creo comando de ejecución

                    var cmd = new SqlCommand("medico", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Realizo la lectura de la ejecución

                    using (var rd = cmd.ExecuteReader())
                    {

                        while (rd.Read())
                        {
                            Lista.Add(new Medico()
                            {

                                idMedico = Convert.ToInt32(rd["idMedico"]),
                                NombreMed = rd["NombreMed"].ToString(),
                                ApellidoMed = rd["ApellidoMed"].ToString(),
                                RunMed = rd["RunMed"].ToString(),
                                Eunacom = rd["Eunacom"].ToString(),
                                NacionalidadMed = rd["NacionalidadMed"].ToString(),
                                Especialidad = rd["Especialidad"].ToString(),
                                Horarios = rd["Horarios"].ToString(),
                                TarifaHr = Convert.ToInt32(rd["TarifaHr"]),


                            });
                        }
                    }


                }

                medico = Lista.Where(item => item.idMedico == idMedico).FirstOrDefault();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = medico });

            }
            catch (Exception error)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message, response = medico });

            }
        }

        //creo el metodo para guardar 

        [HttpPost]
        [Route("Guardar")]

        public IActionResult Guardar([FromBody] Medico objeto)
        {


            try
            {

                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();

                    //Creo comando de ejecución

                    var cmd = new SqlCommand("medico", conexion);

                    //Defino los parametros de entrada

                    cmd.Parameters.AddWithValue("IdMedico", objeto.idMedico);
                    cmd.Parameters.AddWithValue("NombreMed", objeto.NombreMed);
                    cmd.Parameters.AddWithValue("ApellidoMed", objeto.ApellidoMed);
                    cmd.Parameters.AddWithValue("RunMed", objeto.RunMed);
                    cmd.Parameters.AddWithValue("Eunacom", objeto.Eunacom);
                    cmd.Parameters.AddWithValue("Nacionalidad", objeto.NacionalidadMed);
                    cmd.Parameters.AddWithValue("Especialidad", objeto.Especialidad);
                    cmd.Parameters.AddWithValue("Horario", objeto.Horarios);
                    cmd.Parameters.AddWithValue("TarifaHr", objeto.TarifaHr);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();

                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });

            }
            catch (Exception error)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });

            }
        }


        [HttpPut]
        [Route("Editar")]

        public IActionResult Editar([FromBody] Medico objeto)
        {


            try
            {

                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();

                    //Creo comando de ejecución

                    var cmd = new SqlCommand("medico", conexion);

                    //Defino los parametros de entrada

                    cmd.Parameters.AddWithValue("IdMedico", objeto.idMedico);
                    cmd.Parameters.AddWithValue("NombreMed", objeto.NombreMed);
                    cmd.Parameters.AddWithValue("ApellidoMed", objeto.ApellidoMed);
                    cmd.Parameters.AddWithValue("RunMed", objeto.RunMed);
                    cmd.Parameters.AddWithValue("Eunacom", objeto.Eunacom);
                    cmd.Parameters.AddWithValue("Nacionalidad", objeto.NacionalidadMed);
                    cmd.Parameters.AddWithValue("Especialidad", objeto.Especialidad);
                    cmd.Parameters.AddWithValue("Horario", objeto.Horarios);
                    cmd.Parameters.AddWithValue("TarifaHr", objeto.TarifaHr);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();

                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Editado" });

            }
            catch (Exception error)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });

            }
        }
        [HttpDelete]
        [Route("Eliminar/{idMedico:int}")]

        public IActionResult Eliminar(int idMedico)
        {


            try
            {

                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();

                    var cmd = new SqlCommand("medico", conexion);
                    cmd.Parameters.AddWithValue("IdMedico", idMedico);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();

                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Eliminado" });

            }
            catch (Exception error)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });

            }
        }
    }
}