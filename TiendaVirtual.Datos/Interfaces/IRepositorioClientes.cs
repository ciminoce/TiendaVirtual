using System;
using System.Collections.Generic;
using TiendaVirtual.Entidades.Dtos.Cliente;
using TiendaVirtual.Entidades.Entidades;

namespace TiendaVirtual.Datos.Interfaces
{
    public interface IRepositorioClientes
    {
        void Agregar(Cliente cliente);
        void Borrar(int id);
        void Editar(Cliente cliente);
        bool EstaRelacionado(Cliente cliente);
        bool Existe(Cliente cliente);
        Cliente GetClientePorId(int id);
        Cliente GetClientePorEmail(string email);
        List<ClienteListDto> GetClientes();
        List<ClienteListDto> GetClientes(int paisId, int ciudadId);
        List<ClienteListDto> Filtrar(Func<Cliente, bool> predicado);
        int GetCantidad();
    }
}
