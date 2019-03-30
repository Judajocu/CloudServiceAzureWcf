using AzuredbConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WCFServiceWebRole1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IServicelab
    {

        [OperationContract]
        //string GetData(int value);
        bool RegistrarMatricula(string matricula, string password);

        [OperationContract]
        List<Estudiante> ObtenerEstudiantes(string idEstudiantes);

        [OperationContract]
        int ObtenerPassID(string idEstudiantes);

        [OperationContract]
        int ObtenerPassIDProf(string idprof);

        [OperationContract]
        string ObtenerGroupName(int idGroup);

        [OperationContract]
        //string GetData(int value);
        string GenerarReporteEstudiante(string estudianteID);

        [OperationContract]
        bool RegistrarEsudiante(string id, string name, string lastname, string matricula, int idMoodle);

        [OperationContract]
        bool RegistrarProfesor(string id, string name, string lastname, int idMoodle);

        [OperationContract]
        bool RegistrarCurso(string name, int id, string idprofesor);

        [OperationContract]
        bool ActualizarCurso(string name, int id, string idprofesor);

        [OperationContract]
        bool CambiosCursoEstudiante(string name, int id);

        [OperationContract]
        bool RegistrarCursoestudiante(int idCurso, string idEstudiante);

        [OperationContract]
        bool RegistrarSimulacion(string name, int fallos, int duracion);

        [OperationContract]
        bool RegistrarSimulacionEst(string NombreSim, string idEstudiante, string nota);

        [OperationContract]
        bool verificarEstudiante(string idEstudiantes, string password);

        [OperationContract]
        bool verificarProfesor(string idprofesor, string password);

        [OperationContract]
        bool GuardarCambioDinamicos(string simulacion, string[] dato, int dato2, string nivel, string[] nombreData);

        [OperationContract]
        string[] BuscarDatosD(string simulacion, string nivel, string[] nombreData);

        [OperationContract]
        string[] devolverNotasEst(string idEstudiantes);

        [OperationContract]
        string[] devolverNotasEstPorFecha(string idEstudiantes, DateTime date);

        [OperationContract]
        bool updateNota(string NombreSim, string idEstudiante, string nota, int id);

        [OperationContract]
        void datosUser(string idEstudiante);

        [OperationContract]
        void datosUserProf(string idProf);

        [OperationContract]
        List<string> DarLinkDocumentos(string data);

        [OperationContract]
        bool ActualizarSimulacion(string name, int fallos, int duracion);

        [OperationContract]
        long[] DatosSimulacion(string name);

        [OperationContract]
        List<string> DarListagruposProfesor(string idProfesor);

        [OperationContract]
        List<string> DarListaEstudentGrupo(string EstID);

        [OperationContract]
        List<int> DarListaEstudentGrupoInt(string EstID);

        [OperationContract]
        string GenerarReporteProfesor(int grupoId, string profesorID, DateTime date);

        [OperationContract]
        List<Estudiante> DarListaEstudiantesGrupo(int grupoID);

        [OperationContract]
        bool RegistrarProfesorGrupo(string ProfID);

        [OperationContract]
        bool RegistrarEstudentGrupo(string EstID);



        // TODO: Add your service operations here

        [OperationContract]
        bool aleluya(string name);
    }
}
