using LabMVC15_04_2021.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace LabMVC15_04_2021.Models.Data
{
    public class StudentDAO
    {
        private readonly IConfiguration _configuration;
            string connectionString;

        public StudentDAO(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("DefaultConnection");
        }
        // esta es la llamada al string de conexion con la base de datos definido en el punto 3
        public StudentDAO()
        {

        }


        //metodo que simula la insercion de estudiante en BD     
        public int Insert(Student student) // ya no es void sino int
        {
            int resultToReturn;// declaramos variable que guardará un 1 o un 0 de
                               //acuerdo a si se insertó ono el estudiante

            //usaremos using para englobar todo lo que tiene que ver con una misma cosa
            //u objeto. En este caso, todo lo envueto aca tiene que ver con connection, la
            //cual sacamos con la clase SqlConnection y con el string de conexion que tenemos
            //en nuestro appsetting.json
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                connection.Open(); //abrimos conexión
                SqlCommand command = new SqlCommand("InsertStudent", connection);//llamamos a un procedimiento almacenado (SP) que crearemos en el
                                                                                 //punto siguiente. La idea es no tener acá en el código una sentencia INSERT INTO
                                                                                 //directa, pues es una mala práctica y además insostenible e inmantenible en el tiempo.


                command.CommandType = System.Data.CommandType.StoredProcedure; //acá decimos que lo que se ejecutará es un SP

                //acá abajo le pasamos los parámetros al SP. En @ van los nombres de
                //los parámetros en SP y a la par los valores. No pasamos el Id porque es
                //autoincremental en la tabla, entonces no lo necesitamos:

                command.Parameters.AddWithValue("@Name", student.Name);
                command.Parameters.AddWithValue("@Email", student.Email);
                command.Parameters.AddWithValue("@Password", student.Password);

                resultToReturn = command.ExecuteNonQuery(); //esta es la sentencia que
                                                            //ejecuta la inserción en BD y saca un 1 o un 0 dependiendo de si se modificó la
                                                            //tupla o no.Es decir, si se insertó en BD o no. 

                connection.Close(); //cerramos conexión.

            }
            return resultToReturn; //retornamos resultado al Controller.

        }// end insert


        public Boolean VerifyEmail(string studentEmail)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("VerifyEmail", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("Email", studentEmail);

                var returnParameter = command.Parameters.Add("@Exists", System.Data.SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;
                command.ExecuteNonQuery();

                int result = (int)returnParameter.Value;
                connection.Close();

                return result == 1 ? true : false;
            }
        }
    }

    
}
