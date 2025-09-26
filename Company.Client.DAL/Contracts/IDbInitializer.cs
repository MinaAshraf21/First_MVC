
namespace Company.Client.DAL.Contracts
{
    // a code contract that contains all required methods to apply pending migrations & seeding data 
    public interface IDbInitializer
    {
        void Initialize();
        void Seed();
    }
}
