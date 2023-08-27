using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TiendaVirtual.Entidades.Dtos.Cliente;
using TiendaVirtual.Entidades.Entidades;

namespace TiendaVirtual.Servicios.Interfaces
{
    public interface IServiciosClientes
    {
        void Borrar(int id);
        bool EstaRelacionado(Cliente cliente);
        bool Existe(Cliente cliente);
        Cliente GetClientePorId(int id);
        List<ClienteListDto> GetClientes();
        List<ClienteListDto> GetClientes(int paisId, int ciudadId);
        void Guardar(Cliente cliente);
        List<ClienteListDto> Filtrar(Func<Cliente, bool> predicado);
        int GetCantidad();
        Cliente GetClientePorEmail(string name);
    }
}
