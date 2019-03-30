using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using CrystalDecisions.Shared;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Net;
using System.IO;
using AzuredbConnection;

namespace WCFServiceWebRole1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Servicelab : IServicelab
    {
        DBCHEMICALLABEntities conexionDB = new DBCHEMICALLABEntities();

        public List<Estudiante> ObtenerEstudiantes(string idEstudiantes)
        {
            List<Estudiante> vacio = new List<Estudiante>();
            //return conexionDB.Estudiantes.Where(Estudiantes => Estudiantes.EstudianteID == idEstudiantes || idEstudiantes == -1).ToList();
            return vacio;
        }

        public int ObtenerPassID(string idEstudiantes)
        {

            return conexionDB.Estudiantes.Find(idEstudiantes).EstIDMoodle ?? 0;
        }

        public int ObtenerPassIDProf(string idprof)
        {
            return conexionDB.Profesores.Find(idprof).ProfIDMoodle ?? 0;
        }

        public string ObtenerGroupName(int idGroup)
        {
            return conexionDB.Grupos.Find(idGroup).GrupoNombre;
        }

        public bool RegistrarMatricula(string matricula, string password)
        {
            try
            {
                var nuevoEstudiante = conexionDB.Estudiantes.Create();
                //if para verificar aqui
                nuevoEstudiante.EstudianteID = matricula;
                nuevoEstudiante.EstMatricula = password;


                conexionDB.Estudiantes.Add(nuevoEstudiante);
                //guardar en la base de datos
                conexionDB.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public string GenerarReporteEstudiante(string estudianteID)
        {

            string data = "LLega a la plataforma";
            CrystalReport1 crpt = new CrystalReport1();
            crpt.Load(@"C:\temporal\CrystalReport2.rpt");
            Estudiante estudiante = conexionDB.Estudiantes.Find(estudianteID);
            string[] notas = devolverNotasEst(estudianteID);



            //crpt.SetDataSource(datatablesource);
            crpt.SetParameterValue("nombre", estudiante.EstNombre);
            crpt.SetParameterValue("apellido", estudiante.EstApellido);
            crpt.SetParameterValue("matricula", estudiante.EstMatricula);
            crpt.SetParameterValue("nota1", notas[1]);
            crpt.SetParameterValue("nota2", notas[0]);
            crpt.SetParameterValue("nota3", notas[2]);
            crpt.SetParameterValue("nota4", notas[3]);
            crpt.SetParameterValue("nota5", notas[4]);

            try
            {
                data = "entra al try";
                CrystalDecisions.Shared.ExportOptions rptExportOption = crpt.ExportOptions;
                DiskFileDestinationOptions rptFileDestOption = new DiskFileDestinationOptions();
                string reportFileName = @"C:\temporal\SampleReport.pdf";
                rptFileDestOption.DiskFileName = reportFileName;

                {
                    rptExportOption.ExportDestinationType = ExportDestinationType.DiskFile;
                    //if we want to generate the report as PDF, change the ExportFormatType as "ExportFormatType.PortableDocFormat"
                    //if we want to generate the report as Excel, change the ExportFormatType as "ExportFormatType.Excel"
                    rptExportOption.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rptExportOption.ExportDestinationOptions = rptFileDestOption;
                    PdfRtfWordFormatOptions rptFormatOption = new PdfRtfWordFormatOptions();
                    rptExportOption.ExportFormatOptions = rptFormatOption;
                }

                crpt.Export();

            }
            catch (Exception ex)
            {
                data = ex.Message;
                return data;
            }


            return data;
        }

        //registro de datos
        public bool RegistrarEsudiante(string id, string name, string lastname, string matricula, int idMoodle)
        {
            try
            {
                var nuevoEstudiante = conexionDB.Estudiantes.Create();
                //if para verificar aqui
                nuevoEstudiante.EstudianteID = id;
                nuevoEstudiante.EstNombre = name;
                nuevoEstudiante.EstApellido = lastname;
                nuevoEstudiante.EstMatricula = matricula;
                nuevoEstudiante.EstIDMoodle = idMoodle;


                conexionDB.Estudiantes.Add(nuevoEstudiante);
                //guardar en la base de datos
                conexionDB.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool RegistrarProfesor(string id, string name, string lastname, int idMoodle)
        {
            try
            {
                var nuevoProf = conexionDB.Profesores.Create();
                //if para verificar aqui
                nuevoProf.ProfesorId = id;
                nuevoProf.ProfNombre = name;
                nuevoProf.ProfApellido = lastname;
                nuevoProf.ProfIDMoodle = idMoodle;


                conexionDB.Profesores.Add(nuevoProf);
                //guardar en la base de datos
                conexionDB.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool RegistrarCurso(string name, int id, string idprofesor)
        {
            try
            {
                //conexionDB.Profesores.Find(idEstudiantes).EstPassword;

                var nuevoCurso = conexionDB.Grupos.Create();
                //if para verificar aqui
                nuevoCurso.GrupoNombre = name;
                nuevoCurso.GrupoProfesor = idprofesor;
                nuevoCurso.GrupoID = id;


                conexionDB.Grupos.Add(nuevoCurso);
                //guardar en la base de datos
                conexionDB.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool ActualizarCurso(string name, int id, string idprofesor)
        {
            try
            {
                if (conexionDB.Grupos.Any(data => data.GrupoID == id))
                {

                    var nuevoCurso = conexionDB.Grupos.Single(esto => esto.GrupoID == id);
                    nuevoCurso.GrupoNombre = name;
                    nuevoCurso.GrupoProfesor = idprofesor;

                    //guardar en la base de datos
                    conexionDB.SaveChanges();
                }
                else
                {
                    var nuevoCurso = conexionDB.Grupos.Create();
                    //if para verificar aqui
                    nuevoCurso.GrupoNombre = name;
                    nuevoCurso.GrupoProfesor = idprofesor;
                    nuevoCurso.GrupoID = id;


                    conexionDB.Grupos.Add(nuevoCurso);
                    //guardar en la base de datos
                    conexionDB.SaveChanges();
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool CambiosCursoEstudiante(string name, int id)
        {
            try
            {
                if (conexionDB.Grupos.Any(data => data.GrupoID == id))
                {

                    var nuevoCurso = conexionDB.Grupos.Single(esto => esto.GrupoID == id);
                    nuevoCurso.GrupoNombre = name;

                    //guardar en la base de datos
                    conexionDB.SaveChanges();
                }
                else
                {
                    var nuevoCurso = conexionDB.Grupos.Create();
                    //if para verificar aqui
                    nuevoCurso.GrupoNombre = name;
                    nuevoCurso.GrupoID = id;


                    conexionDB.Grupos.Add(nuevoCurso);
                    //guardar en la base de datos
                    conexionDB.SaveChanges();
                }

            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool RegistrarCursoestudiante(int idCurso, string idEstudiante)
        {
            try
            {

                var nuevoCursoEst = conexionDB.EstudiantesGrupos.Create();
                //if para verificar aqui
                nuevoCursoEst.GrupoID = idCurso;
                nuevoCursoEst.EstudianteID = idEstudiante;


                conexionDB.EstudiantesGrupos.Add(nuevoCursoEst);
                //guardar en la base de datos
                conexionDB.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool RegistrarSimulacion(string name, int fallos, int duracion)
        {
            try
            {

                var nuevoaSimulacion = conexionDB.Simulaciones.Create();
                //if para verificar aqui
                nuevoaSimulacion.SimCantFallos = fallos;
                nuevoaSimulacion.SimDuracion = duracion;
                nuevoaSimulacion.SimNombre = name;


                conexionDB.Simulaciones.Add(nuevoaSimulacion);
                //guardar en la base de datos
                conexionDB.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool RegistrarSimulacionEst(string NombreSim, string idEstudiante, string nota)
        {
            try
            {

                var nuevoaSimulacionEst = conexionDB.SimmulacionEstudiantes.Create();
                //if para verificar aqui
                nuevoaSimulacionEst.EstudianteId = idEstudiante;
                nuevoaSimulacionEst.SimulacionId = conexionDB.Simulaciones.Single(s => s.SimNombre == NombreSim).SimID;
                nuevoaSimulacionEst.Nota = nota;
                System.DateTime dateTimeVariable = System.DateTime.Now;
                nuevoaSimulacionEst.fecha = dateTimeVariable;

                conexionDB.SimmulacionEstudiantes.Add(nuevoaSimulacionEst);
                //guardar en la base de datos
                conexionDB.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool verificarEstudiante(string idEstudiantes, string password)
        {
            bool passa = false;
            /*Ese es un placeholder de prueba*/
            String pass = password;
            /*Ese es un placeholder de prueba*/
            String user = idEstudiantes;
            /* Este string es constante, no se puede cambiar porque es el
             * que da acceso a la pva a ver si valida o no el usuario que
             * se manda*/
            String service = "moodle_mobile_app";

            string createRequest = string.Format("http://www.deltasoft.com.do/moodle/login/token.php?username=" + user + "&password=" + pass + "&service=" + service);

            //Console.WriteLine(createRequest);
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(createRequest);
            req.Method = "GET";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = 0;
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream resStream = resp.GetResponseStream();
            StreamReader reader = new StreamReader(resStream);
            string contents = reader.ReadToEnd();

            //Console.WriteLine(contents);



            // Deserialize
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            if (contents.Contains("exception"))
            {
                // Error
                MoodleException moodleError = serializer.Deserialize<MoodleException>(contents);
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                passa = false;
            }
            else
            {

                Token root = JsonConvert.DeserializeObject<Token>(contents);

                String tok = root.token;

                if (tok != null)
                {
                    //Login
                    passa = true;
                    //if true, guarda datos
                    if (!conexionDB.Estudiantes.Any(Estudiantes => Estudiantes.EstudianteID == idEstudiantes))
                    {
                        datosUser(user);
                    }

                }
                else
                    //no dejar pasar
                    passa = false;

                //Puesto para breakpoint y parar la consola para chequear
                //Console.WriteLine(passa + " lal");

            }

            return passa;
            /*Estudiantes estudiante = conexionDB.Estudiantes.Find(idEstudiantes);
            if (estudiante == null)
            {
                return false;
            }
            else { return true; }*/
        }

        public bool verificarProfesor(string idprofesor, string password)
        {
            bool passa = false;
            /*Ese es un placeholder de prueba*/
            String pass = password;
            /*Ese es un placeholder de prueba*/
            String user = idprofesor;
            /* Este string es constante, no se puede cambiar porque es el
             * que da acceso a la pva a ver si valida o no el usuario que
             * se manda*/
            String service = "moodle_mobile_app";

            string createRequest = string.Format("http://www.deltasoft.com.do/moodle/login/token.php?username=" + user + "&password=" + pass + "&service=" + service);

            //Console.WriteLine(createRequest);
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(createRequest);
            req.Method = "GET";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = 0;
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream resStream = resp.GetResponseStream();
            StreamReader reader = new StreamReader(resStream);
            string contents = reader.ReadToEnd();

            //Console.WriteLine(contents);



            // Deserialize
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            if (contents.Contains("exception"))
            {
                // Error
                MoodleException moodleError = serializer.Deserialize<MoodleException>(contents);
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                passa = false;
            }
            else
            {

                Token root = JsonConvert.DeserializeObject<Token>(contents);

                String tok = root.token;

                if (tok != null)
                {
                    //Login
                    passa = true;
                    //if true, guarda datos
                    if (!conexionDB.Profesores.Any(prof => prof.ProfesorId == idprofesor))
                    {
                        datosUserProf(user);
                    }

                }
                else
                    //no dejar pasar
                    passa = false;

                //Puesto para breakpoint y parar la consola para chequear
                //Console.WriteLine(passa + " lal");

            }

            return passa;
        }

        public bool GuardarCambioDinamicos(string simulacion, string[] dato, int dato2, string nivel, string[] nombreData)
        {
            try
            {
                for (int i = 0; i < nombreData.Length; i++)
                {
                    string IdDinamico = simulacion + "," + nivel + "," + nombreData[i];

                    if (conexionDB.Datosdinamicos.Any(data => data.NombreID == IdDinamico))
                    {
                        var nuevoaDatoDinamico = conexionDB.Datosdinamicos.Single(esto => esto.NombreID == IdDinamico);
                        nuevoaDatoDinamico.Datastring = dato[i];

                        //guardar en la base de datos
                        conexionDB.SaveChanges();
                    }
                    else
                    {
                        var nuevoaDatoDinamico = conexionDB.Datosdinamicos.Create();
                        //if para verificar aqui
                        nuevoaDatoDinamico.NombreID = IdDinamico;
                        nuevoaDatoDinamico.simulacionID = conexionDB.Simulaciones.Single(s => s.SimNombre == simulacion).SimID;
                        nuevoaDatoDinamico.Datastring = dato[i];

                        conexionDB.Datosdinamicos.Add(nuevoaDatoDinamico);
                        //guardar en la base de datos
                        conexionDB.SaveChanges();
                    }

                }

            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public string[] BuscarDatosD(string simulacion, string nivel, string[] nombreData)
        {

            string[] result = new string[nombreData.Length];

            for (int i = 0; i < result.Length; i++)
            {
                string IdDinamico = simulacion + "," + nivel + "," + nombreData[i];
                try
                {
                    if (conexionDB.Datosdinamicos.Any(data => data.NombreID == IdDinamico))
                    {
                        var DatoDinamico = conexionDB.Datosdinamicos.Single(d => d.NombreID == IdDinamico);
                        result[i] = DatoDinamico.Datastring;


                    }
                    else
                    {
                        //no esta
                        result[i] = "0";
                    }

                }
                catch (Exception)
                {
                    result[0] = "null";
                    result[1] = "null";
                    result[2] = "null";
                    return result;
                }
            }

            return result;
        }

        public string[] devolverNotasEst(string idEstudiantes)
        {

            string[] simulaciones = new string[5] { "Nomenclatura", "Balanceo", "Estequiometria", "Tabla Periodica", "Conversion" };
            string[] esto = new string[5];
            for (int i = 0; i < esto.Length; i++)
            {
                esto[i] = "Null";
            }

            try
            {
                for (int i = 0; i < esto.Length; i++)
                {
                    List<SimmulacionEstudiante> notas = new List<SimmulacionEstudiante>();
                    string temp = simulaciones[i];
                    Simulacione sim = conexionDB.Simulaciones.Single(s => s.SimNombre == temp);

                    if (conexionDB.SimmulacionEstudiantes.Any(Estudiantes => Estudiantes.EstudianteId == idEstudiantes && Estudiantes.SimulacionId == sim.SimID))
                    {
                        notas = conexionDB.SimmulacionEstudiantes.Where(Estudiantes => Estudiantes.EstudianteId == idEstudiantes && Estudiantes.SimulacionId == sim.SimID).ToList();
                        esto[i] = notas.OrderByDescending(n => n.fecha).First().Nota;
                    }

                }
            }
            catch (Exception)
            {
                return esto;
            }

            return esto;
        }

        public string[] devolverNotasEstPorFecha(string idEstudiantes, DateTime date)
        {
            string[] simulaciones = new string[5] { "Nomenclatura", "Balanceo", "Estequiometria", "Tabla Periodica", "Conversion" };
            string[] esto = new string[5];
            for (int i = 0; i < esto.Length; i++)
            {
                esto[i] = "Null";
            }

            try
            {
                for (int i = 0; i < esto.Length; i++)
                {
                    List<SimmulacionEstudiante> notas = new List<SimmulacionEstudiante>();
                    string temp = simulaciones[i];
                    Simulacione sim = conexionDB.Simulaciones.Single(s => s.SimNombre == temp);

                    if (conexionDB.SimmulacionEstudiantes.Any(Estudiantes => Estudiantes.EstudianteId == idEstudiantes && Estudiantes.SimulacionId == sim.SimID))
                    {
                        notas = conexionDB.SimmulacionEstudiantes.Where(Estudiantes => Estudiantes.EstudianteId == idEstudiantes && Estudiantes.SimulacionId == sim.SimID && Estudiantes.fecha < date).ToList();
                        esto[i] = notas.OrderByDescending(n => n.fecha).First().Nota;
                    }

                }
            }
            catch (Exception)
            {
                return esto;
            }

            return esto;
        }

        public bool updateNota(string NombreSim, string idEstudiante, string nota, int id)
        {
            try
            {

                var nuevoaSimulacionEst = conexionDB.SimmulacionEstudiantes.Single(esto => esto.SimEstId == id);
                //if para verificar aqui
                //nuevoaSimulacionEst.EstudianteId = idEstudiante;
                //nuevoaSimulacionEst.SimulacionId = conexionDB.Simulaciones.Single(s => s.SimNombre == NombreSim).SimID;
                //nuevoaSimulacionEst.Nota = nota;
                System.DateTime dateTimeVariable = System.DateTime.Now;
                nuevoaSimulacionEst.fecha = dateTimeVariable;

                //guardar en la base de datos
                conexionDB.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public void datosUser(string idEstudiante)
        {
            String token = "5708e1cb28191d8d50401a15e56bea81";

            /*placeholder de prueba para conseguir datos de usuario
             Si se quieren todos los usuario de moodle se pone: %%*/
            string email = idEstudiante;
            string createRequest = string.Format("http://www.deltasoft.com.do/moodle/webservice/rest/server.php?wstoken={0}&wsfunction={1}&moodlewsrestformat=json&&criteria[0][key]=email&criteria[0][value]=" + email, token, "core_user_get_users");

            //Console.WriteLine(createRequest);
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(createRequest);
            req.Method = "GET";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = 0;
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream resStream = resp.GetResponseStream();
            StreamReader reader = new StreamReader(resStream);
            string contents = reader.ReadToEnd();


            // Deserialize
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            if (contents.Contains("exception"))
            {
                // Error
                MoodleException moodleError = serializer.Deserialize<MoodleException>(contents);
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                // RootObject root = JsonConvert.DeserializeObject<List<User>>(contents);

                RootObject root = JsonConvert.DeserializeObject<RootObject>(contents);

                //La lista de todos los usuarios desearalizada de Json osea una lista normal
                List<User> users = root.users;

                //Probando que sirve
                if (users[0].email != null)
                {
                    //Console.WriteLine(users[0].fullname);
                    RegistrarEsudiante(idEstudiante, users[0].firstname, users[0].lastname, "no tiene", users[0].id);
                    //registra o actualiza los cursos en los que esta el estudiante
                    RegistrarEstudentGrupo(idEstudiante);
                    //devolver un string[] de todos los cursos en los que esta
                    List<int> grupos = DarListaEstudentGrupoInt(idEstudiante);
                    foreach (int g in grupos)
                    {
                        RegistrarCursoestudiante(g, idEstudiante);
                    }
                    //RegistrarCursoestudiante(1, idEstudiante);
                }


                //Puesto para breakpoint y parar la consola para chequear
                //Console.WriteLine("lal");

            }
        }

        public void datosUserProf(string idProf)
        {
            String token = "5708e1cb28191d8d50401a15e56bea81";

            /*placeholder de prueba para conseguir datos de usuario
             Si se quieren todos los usuario de moodle se pone: %%*/
            string email = idProf;
            string createRequest = string.Format("http://www.deltasoft.com.do/moodle/webservice/rest/server.php?wstoken={0}&wsfunction={1}&moodlewsrestformat=json&&criteria[0][key]=email&criteria[0][value]=" + email, token, "core_user_get_users");

            //Console.WriteLine(createRequest);
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(createRequest);
            req.Method = "GET";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = 0;
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream resStream = resp.GetResponseStream();
            StreamReader reader = new StreamReader(resStream);
            string contents = reader.ReadToEnd();


            // Deserialize
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            if (contents.Contains("exception"))
            {
                // Error
                MoodleException moodleError = serializer.Deserialize<MoodleException>(contents);
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                // RootObject root = JsonConvert.DeserializeObject<List<User>>(contents);

                RootObject root = JsonConvert.DeserializeObject<RootObject>(contents);

                //La lista de todos los usuarios desearalizada de Json osea una lista normal
                List<User> users = root.users;

                //Probando que sirve
                if (users[0].id != null)
                {
                    //Console.WriteLine(users[0].fullname);
                    RegistrarProfesor(idProf, users[0].firstname, users[0].lastname, users[0].id);
                    RegistrarProfesorGrupo(idProf);
                    //ActualizarCurso("Quimica101", idProf);

                    //RegistrarCursoestudiante(1, idEstudiante);
                }


                //Puesto para breakpoint y parar la consola para chequear
                //Console.WriteLine("lal");

            }
        }

        public List<string> DarLinkDocumentos(string data)
        {
            List<string> resultado = new List<string>();
            //string[] esto = null;
            //data es el codigo del curso
            try
            {
                String token = "5708e1cb28191d8d50401a15e56bea81";
                string descarga;
                string nameF;

                string createRequest = string.Format("http://www.deltasoft.com.do/moodle/webservice/rest/server.php?wstoken={0}&wsfunction={1}&courseid={2}&moodlewsrestformat=json", token, "core_course_get_contents", data);

                //Console.WriteLine(createRequest);
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(createRequest);
                req.Method = "GET";
                req.ContentType = "application/x-www-form-urlencoded";
                req.ContentLength = 0;
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                Stream resStream = resp.GetResponseStream();
                StreamReader reader = new StreamReader(resStream);
                string contents = reader.ReadToEnd();

                // Deserialize
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (contents.Contains("exception"))
                {
                    // Error
                    MoodleException moodleError = serializer.Deserialize<MoodleException>(contents);
                    //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                else
                {
                    /*LA lista de cursos, porque el JSON dice que un solo curso es una lista
                     * de cursos de un elemento (MALDITO JSON DEL DIABLO CONIO) y luego me 
                     di cuenta que los topicos lo tiene asi y mierda entonces para poder
                     entrar a todos los topicos*/
                    List<Course> courses = JsonConvert.DeserializeObject<List<Course>>(contents);
                    foreach (Course root in courses)
                    {
                        //Buscando dentro del topico lo que hay
                        foreach (Module mod in root.Modules)
                        {
                            //no todos tienen archivos
                            if (mod.Contents != null)
                            {
                                //Buscando
                                //esto=new string[mod.Contents.Length];
                                int i = 0;
                                foreach (Content cont in mod.Contents)
                                {
                                    //Tomar el nombre del archivo, que se pasa el formato que esta(pdf o word, etc)
                                    nameF = cont.Filename;
                                    //Console.WriteLine(nameF);
                                    //Link de descarga se completa aquí
                                    descarga = cont.Fileurl + "&token=" + token;
                                    //Console.WriteLine(nameF + "link" + descarga);
                                    //esto[i] = nameF + "|" + descarga;
                                    resultado.Add(nameF + "|" + descarga);
                                    i++;
                                    //Bajando todos los archivos ya
                                    using (var client = new WebClient())
                                    {
                                        //Esto es una vaina para poder bajar porque entonces no valida el SSL y da error
                                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                                        //Lee la funcion de client, tal vez da una idea de lo que hace (?)
                                        //client.DownloadFile(descarga, nameF);
                                    }
                                }
                            }

                        }
                    }

                }
            }
            catch (Exception)
            {
                return resultado;
            }

            return resultado;
        }

        public bool ActualizarSimulacion(string name, int fallos, int duracion)
        {
            try
            {
                if (conexionDB.Simulaciones.Any(data => data.SimNombre == name))
                {
                    var sim = conexionDB.Simulaciones.Single(esto => esto.SimNombre == name);
                    sim.SimDuracion = duracion;
                    sim.SimCantFallos = fallos;

                    conexionDB.SaveChanges();
                }
                else
                {
                    var sim = conexionDB.Simulaciones.Create();
                    sim.SimNombre = name;
                    sim.SimDuracion = duracion;
                    sim.SimCantFallos = fallos;

                    conexionDB.Simulaciones.Add(sim);
                    conexionDB.SaveChanges();
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public long[] DatosSimulacion(string name)
        {
            long[] esto = new long[2] { 0, 0 };

            try
            {
                if (conexionDB.Simulaciones.Any(data => data.SimNombre == name))
                {
                    var sim = conexionDB.Simulaciones.Single(es => es.SimNombre == name);
                    esto[0] = sim.SimDuracion ?? 0;
                    esto[1] = sim.SimCantFallos ?? 0;

                }
            }
            catch (Exception)
            {
                return esto;
            }

            return esto;
        }

        public List<string> DarListagruposProfesor(string idProfesor)
        {
            List<string> results = new List<string>();
            List<Grupos> groups = new List<Grupos>();
            string[] esto;

            try
            {
                if (conexionDB.Grupos.Any(data => data.GrupoProfesor == idProfesor))
                {
                    groups = conexionDB.Grupos.Where(g => g.GrupoProfesor == idProfesor).ToList();
                    foreach (Grupos a in groups)
                    {
                        results.Add(a.GrupoNombre + "@" + a.GrupoID);
                    }

                }
            }
            catch (Exception)
            {
                return results;
            }

            return results;
        }

        public List<string> DarListaEstudentGrupo(string EstID)
        {
            List<string> results = new List<string>();
            List<EstudiantesGrupos> groups = new List<EstudiantesGrupos>();
            string[] esto;

            try
            {
                if (conexionDB.EstudiantesGrupos.Any(data => data.EstudianteID == EstID))
                {
                    groups = conexionDB.EstudiantesGrupos.Where(g => g.EstudianteID == EstID).ToList();
                    foreach (EstudiantesGrupos a in groups)
                    {
                        Grupos ayu = conexionDB.Grupos.Single(c => c.GrupoID == a.GrupoID);
                        results.Add(ayu.GrupoNombre + "@" + ayu.GrupoID);
                    }

                }
            }
            catch (Exception)
            {
                return results;
            }

            return results;
        }

        public List<int> DarListaEstudentGrupoInt(string EstID)
        {
            List<int> results = new List<int>();
            List<EstudiantesGrupos> groups = new List<EstudiantesGrupos>();
            string[] esto;

            try
            {
                String token = "5708e1cb28191d8d50401a15e56bea81";
                //El id del estudiante
                int id = ObtenerPassID(EstID);
                //Haciendo la llamada para conseguir el curso
                string createRequest = string.Format("http://www.deltasoft.com.do/moodle/webservice/rest/server.php?wstoken={0}&wsfunction={1}&userid={2}&moodlewsrestformat=json", token, "core_enrol_get_users_courses", id.ToString());
                //Console.WriteLine(createRequest);
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(createRequest);
                req.Method = "GET";
                req.ContentType = "application/x-www-form-urlencoded";
                req.ContentLength = 0;
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                Stream resStream = resp.GetResponseStream();
                StreamReader reader = new StreamReader(resStream);
                string contents = reader.ReadToEnd();

                // Deserialize
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (contents.Contains("exception"))
                {
                    // Error
                    MoodleException moodleError = serializer.Deserialize<MoodleException>(contents);
                    //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                else
                {
                    /*LA lista de cursos en lo que el ususario esta inscrito*/
                    List<UserCourses> courses = JsonConvert.DeserializeObject<List<UserCourses>>(contents);
                    //Leyendo la lista
                    foreach (UserCourses item in courses)
                    {
                        results.Add(item.id);
                    }


                }
            }
            catch (Exception)
            {
                return results;
            }

            return results;
        }

        public string GenerarReporteProfesor(int grupoId, string profesorID, DateTime date)
        {
            string data = "LLega a la plataforma";
            CrystalReport2 crpt = new CrystalReport2();
            crpt.Load(@"C:\temporal\CrystalReport2.rpt");
            Profesore prof = conexionDB.Profesores.Find(profesorID);
            Grupos group = conexionDB.Grupos.Single(g => g.GrupoID == grupoId);
            //conseguir todos los estudiantes de un grupo de profesores
            List<Estudiante> alumnos = DarListaEstudiantesGrupo(grupoId);
            //string[] notas = devolverNotasEst(estudianteID);



            //crpt.SetDataSource(datatablesource);
            crpt.SetParameterValue("dato1", prof.ProfesorId);
            crpt.SetParameterValue("dato2", prof.ProfNombre);
            crpt.SetParameterValue("dato3", prof.ProfApellido);
            crpt.SetParameterValue("dato4", group.GrupoNombre);
            crpt.SetParameterValue("dato5", group.GrupoNombre);

            string ayu = "";
            string ayu2 = "";
            string ayu3 = "";
            string ayu4 = "";
            string ayu5 = "";
            string ayu6 = "";
            string ayu7 = "";
            string ayu8 = "";
            foreach (Estudiante a in alumnos)
            {
                ayu += a.EstNombre + Environment.NewLine;
                ayu2 += a.EstudianteID + Environment.NewLine;
                ayu3 += a.EstApellido + Environment.NewLine;
                string[] notas = devolverNotasEstPorFecha(a.EstudianteID, date);
                ayu4 += notas[1] + Environment.NewLine;
                ayu5 += notas[0] + Environment.NewLine;
                ayu6 += notas[2] + Environment.NewLine;
                ayu7 += notas[3] + Environment.NewLine;
                ayu8 += notas[4] + Environment.NewLine;
            }
            crpt.SetParameterValue("name", ayu);
            crpt.SetParameterValue("mat", ayu2);
            crpt.SetParameterValue("last", ayu3);
            crpt.SetParameterValue("nota1", ayu4);
            crpt.SetParameterValue("nota2", ayu5);
            crpt.SetParameterValue("nota3", ayu6);
            crpt.SetParameterValue("nota4", ayu7);
            crpt.SetParameterValue("nota5", ayu8);

            try
            {
                data = "entra al try";
                CrystalDecisions.Shared.ExportOptions rptExportOption = crpt.ExportOptions;
                DiskFileDestinationOptions rptFileDestOption = new DiskFileDestinationOptions();
                string reportFileName = @"C:\temporal\SampleReport2.pdf";
                rptFileDestOption.DiskFileName = reportFileName;

                {
                    rptExportOption.ExportDestinationType = ExportDestinationType.DiskFile;
                    //if we want to generate the report as PDF, change the ExportFormatType as "ExportFormatType.PortableDocFormat"
                    //if we want to generate the report as Excel, change the ExportFormatType as "ExportFormatType.Excel"
                    rptExportOption.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rptExportOption.ExportDestinationOptions = rptFileDestOption;
                    PdfRtfWordFormatOptions rptFormatOption = new PdfRtfWordFormatOptions();
                    rptExportOption.ExportFormatOptions = rptFormatOption;
                }

                crpt.Export();

            }
            catch (Exception ex)
            {
                data = ex.Message;
                return data;
            }


            return data;
        }

        public List<Estudiante> DarListaEstudiantesGrupo(int grupoID)
        {
            List<Estudiante> results = new List<Estudiante>();
            List<EstudiantesGrupos> aux = new List<EstudiantesGrupos>();

            try
            {
                Grupos group = conexionDB.Grupos.Single(s => s.GrupoID == grupoID);
                if (conexionDB.EstudiantesGrupos.Any(data => data.GrupoID == group.GrupoID))
                {
                    aux = conexionDB.EstudiantesGrupos.Where(g => g.GrupoID == group.GrupoID).ToList();
                    foreach (EstudiantesGrupos a in aux)
                    {
                        Estudiante ayuda = conexionDB.Estudiantes.Single(s => s.EstudianteID == a.EstudianteID);
                        results.Add(ayuda);
                    }

                }
            }
            catch (Exception)
            {
                return results;
            }

            return results;
        }

        public bool RegistrarProfesorGrupo(string ProfID)
        {
            try
            {
                String token = "5708e1cb28191d8d50401a15e56bea81";
                //El id del estudiante
                int id = ObtenerPassIDProf(ProfID);
                //Haciendo la llamada para conseguir el curso
                string createRequest = string.Format("http://www.deltasoft.com.do/moodle/webservice/rest/server.php?wstoken={0}&wsfunction={1}&userid={2}&moodlewsrestformat=json", token, "core_enrol_get_users_courses", id.ToString());
                //Console.WriteLine(createRequest);
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(createRequest);
                req.Method = "GET";
                req.ContentType = "application/x-www-form-urlencoded";
                req.ContentLength = 0;
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                Stream resStream = resp.GetResponseStream();
                StreamReader reader = new StreamReader(resStream);
                string contents = reader.ReadToEnd();

                // Deserialize
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (contents.Contains("exception"))
                {
                    // Error
                    MoodleException moodleError = serializer.Deserialize<MoodleException>(contents);
                    //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                else
                {
                    /*LA lista de cursos en lo que el ususario esta inscrito*/
                    List<UserCourses> courses = JsonConvert.DeserializeObject<List<UserCourses>>(contents);
                    //Leyendo la lista
                    foreach (UserCourses item in courses)
                    {
                        ActualizarCurso(item.shortname, item.id, ProfID);
                        //id del curso
                        //Console.WriteLine(item.id);
                        //Nombre corto
                        //Console.WriteLine(item.shortname);
                        //Nombre abreviado
                        //Console.WriteLine(item.fullname);
                    }

                    Console.WriteLine("lala");

                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool RegistrarEstudentGrupo(string EstID)
        {
            try
            {
                String token = "5708e1cb28191d8d50401a15e56bea81";
                //El id del estudiante
                int id = ObtenerPassID(EstID);
                //Haciendo la llamada para conseguir el curso
                string createRequest = string.Format("http://www.deltasoft.com.do/moodle/webservice/rest/server.php?wstoken={0}&wsfunction={1}&userid={2}&moodlewsrestformat=json", token, "core_enrol_get_users_courses", id.ToString());
                //Console.WriteLine(createRequest);
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(createRequest);
                req.Method = "GET";
                req.ContentType = "application/x-www-form-urlencoded";
                req.ContentLength = 0;
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                Stream resStream = resp.GetResponseStream();
                StreamReader reader = new StreamReader(resStream);
                string contents = reader.ReadToEnd();

                // Deserialize
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (contents.Contains("exception"))
                {
                    // Error
                    MoodleException moodleError = serializer.Deserialize<MoodleException>(contents);
                    //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                else
                {
                    /*LA lista de cursos en lo que el ususario esta inscrito*/
                    List<UserCourses> courses = JsonConvert.DeserializeObject<List<UserCourses>>(contents);
                    //Leyendo la lista
                    foreach (UserCourses item in courses)
                    {
                        CambiosCursoEstudiante(item.shortname, item.id);
                        //id del curso
                        //Console.WriteLine(item.id);
                        //Nombre corto
                        //Console.WriteLine(item.shortname);
                        //Nombre abreviado
                        //Console.WriteLine(item.fullname);
                    }


                }

            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool aleluya(string name)
        {
            return true;
        }
    }
}
