using ProjectAPI.Models;

namespace ProjectAPI.Interfaces {
    public interface IClientRepository {
        ICollection<Client> GetAllClients();
        Client GetClientById(int id);
        bool ClientExists(int id);
        bool CreateClient(Client client);
        bool UpdateClient(Client client);
        bool DeleteClient(Client client);
        bool Save();
    }
}