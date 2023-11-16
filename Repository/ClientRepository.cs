using Microsoft.EntityFrameworkCore;
using ProjectAPI.Data;
using ProjectAPI.Interfaces;
using ProjectAPI.Models;

namespace ProjectAPI.Repository {
    public class ClientRepository : IClientRepository {
        private readonly DataContext _dataContext;

        public ClientRepository(DataContext dataContext) {
            _dataContext = dataContext;
        }

        public ICollection<Client> GetAllClients() {
            return _dataContext.Clients.Include(c => c.Projects).ToList();
        }

        public Client GetClientById(int id) {
            return _dataContext
                .Clients
                .Include(c => c.Projects)
                .FirstOrDefault(c => c.Id == id);
        }

        public bool ClientExists(int id) {
            return _dataContext.Clients.Any(c => c.Id == id);
        }

        public bool CreateClient(Client client) {
            _dataContext.Add(client);
            return Save();
        }

        public bool Save() {
            return _dataContext.SaveChanges() > 0;
        }

        public bool UpdateClient(Client client) {
            _dataContext.Update(client);
            return Save();
        }

        public bool DeleteClient(Client client) {
            _dataContext.Remove(client);
            return Save();
        }
    }
}