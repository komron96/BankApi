using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace BankApp.WebApi
{

    [ApiController]
    [Route("client")]
    public sealed class ClientContoller : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public ClientContoller(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpPost]
        public async Task<Client> CreateClientAsync([FromBody] Client client, CancellationToken cancellationToken)
        {
            await _appDbContext.AddAsync(client, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return client;
        }


        [HttpGet]
        public async Task<IEnumerable<Client>> GetClientsAsync(CancellationToken cancellationToken)
        {
            IEnumerable<Client> clients = await _appDbContext.Clients.ToListAsync(cancellationToken);
            return clients;
        }


        [HttpGet("phone/{phoneNumber}")]
        public async Task<Client> GetClientByPhoneAsync([FromRoute] string phoneNumber, CancellationToken cancellationToken)
        {
            if (phoneNumber.Contains('+'))
            {
                Client client = await _appDbContext.Clients.FirstOrDefaultAsync(t => t.PhoneNumber == phoneNumber, cancellationToken: cancellationToken) ?? throw new NotFoundEntityException("Not found siski");
                return client;
            }
            return null;
        }

        [HttpPost("changePhone/{phoneNumber}/{newPhoneNumber}")]
        public async Task<bool> ChangePhoneNumberAsync([FromRoute] string phoneNumber, [FromRoute] string newPhoneNumber, CancellationToken cancellationToken)
        {
            if (!Regex.IsMatch(phoneNumber, @"^\d{9}$|^\d{12}$"))
            {
                return false;
            }

            Client? client = await _appDbContext.Clients.FirstOrDefaultAsync(t => t.PhoneNumber == phoneNumber, cancellationToken);

            if (client is not null)
            {
                client.PhoneNumber = newPhoneNumber;
                await _appDbContext.SaveChangesAsync(cancellationToken);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
